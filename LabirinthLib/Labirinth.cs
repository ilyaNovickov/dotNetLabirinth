﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LabirinthLib
{
    public class Labirinth
    {
        /*
         * 0 - empty
         * 1 - wall
         * 2 - in
         * 3 - exit
         * 4 - in and exit
         */
        #region Vars
        Random random = new Random();
        float percentofEmptySpace = 0.4f;
        Size size;
        Point firstIn;
        Point secIn;
        Point exit;
        int[,] lab;
        List<Point> firstWay = new List<Point>();
        List<Point> secondWay = new List<Point>();
        #endregion
        #region Constr
        public Labirinth() : this(new Size(10, 10))
        {

        }

        public Labirinth(int size) : this(new Size(size, size))
        {

        }

        public Labirinth(int width, int height) : this(new Size(width, height))
        {

        }

        public Labirinth(Size size)
        {
            this.Size = size;
        }
        #endregion
        #region Props
        public int this[Point point]
        {
            get => lab[point.X, point.Y];
            private set => lab[point.X, point.Y] = value;
        }

        public int this[int x, int y]
        {
            get => lab[x, y];
            private set => lab[x, y] = value;
        }

        public float MinEmptySpace
        {
            get => (1 / (new Size(Width - 2, Height - 2).Square));
        }

        public float EmptySpace
        {
            get => percentofEmptySpace;
            set
            {
                if (MinEmptySpace <= value && value <= 1f)
                {
                    percentofEmptySpace = value;
                }
                else
                {
                    percentofEmptySpace = 0.4f;
                }
                FillLabirinth();
            }
        }

        public Size Size
        {
            get => size;
            set
            {
                if (value.Width < 4 && value.Height < 4)
                    return;
                lab = new int[value.Width, value.Height];
                size = value;
                FillLabirinth();
            }
        }

        public int Width => size.Width;

        public int Height => size.Height;

        public Point FirstIn
        {
            get => firstIn;
            set
            {
                IEnumerable<Point> avaiblePoints = GetEmptyCellsInLayout(1);
                if (avaiblePoints.Contains(value))
                {
                    //if (secondWay.Contains(value))
                    //{
                    //    List<Point> extra = secondWay;
                    //    secondWay = firstWay;
                    //    firstWay = secondWay;
                    //}
                    firstIn = value;
                }
                else
                    return;
            }
        }

        public Point SecondIn
        {
            get => secIn;
            set
            {
                IEnumerable<Point> avaiblePoints = GetEmptyCellsInLayout(1);
                if (avaiblePoints.Contains(value))
                {
                    //if (secondWay.Contains(value))
                    //{
                    //    List<Point> extra = secondWay;
                    //    secondWay = firstWay;
                    //    firstWay = secondWay;
                    //}
                    secIn = value;
                }
                else
                    return;
            }
        }

        public Point Exit
        {
            get => exit;
            set
            {
                IEnumerable<Point> avaiblePoints = GetEmptyCellsInLayout(1);
                if (avaiblePoints.Contains(value))
                {
                    if (secondWay.Contains(value))
                    {
                        List<Point> extra = secondWay;
                        secondWay = firstWay;
                        firstWay = secondWay;

                        Point extraPoint = secIn;
                        secIn = firstIn;
                        firstIn = extraPoint;
                    }
                    firstIn = value;
                }
                else
                    return;
            }
        }

        public int CountofEmptyCells
        {
            get
            {
                int count = 0;
                for (int x = 1; x < lab.GetLength(0); x++)
                {
                    for (int y = 1; y < lab.GetLength(1); y++)
                    {
                        count += this[x, y] == 1 ? 0 : 1;
                    }
                }
                return count;
            }
        }

        public int CountofLayouts
        {
            get
            {
                return Math.Min(Size.Width - 1, Size.Height - 1);
            }
        }
        #endregion
        #region Methods
        #region ExtraMethods
        private void FillLabirinth()
        {
            exit = Point.Empty;
            firstIn = Point.Empty;
            secIn = Point.Empty;
            firstWay.Clear();
            secondWay.Clear();

            for (int x = 0; x < lab.GetLength(0); x++)
            {
                for (int y = 0; y < lab.GetLength(1); y++)
                {
                    this[x, y] = 1;
                }
            }
        }

        private Point GetRandomLayoutPoint(int numofLayout = 0)
        {

            if (numofLayout < 0 || numofLayout >= CountofLayouts)
                throw new Exception("В прямоугольнике нет столько слоёв");

            int offset = numofLayout + 1;
            int x = random.Next(numofLayout, Size.Width - offset);
            int y = random.Next(numofLayout, Size.Height - offset);

            byte doIntZero = (byte)random.Next(0, 3 + 1);

            if (doIntZero == 0)
                x = numofLayout;
            else if (doIntZero == 1)
                y = numofLayout;
            else if (doIntZero == 2)
                x = Size.Width - offset;
            else
                y = Size.Height - offset;

            return new Point(x, y);
        }

        private Point GetRandomPointFromList(IEnumerable<Point> list)
        {
            if (list.Count() == 0)
                return Point.Empty;
            return list.ElementAt(random.Next(0, list.Count()));
        }

        private Direction GetRandomDirectionFromList(IEnumerable<Direction> avaibleDirs)
        {
            if (avaibleDirs.Count() == 0)
                return Direction.None;
            return avaibleDirs.ElementAt(random.Next(0, avaibleDirs.Count<Direction>()));
        }

        private bool IsBorder(Point point)
        {
            return (point.X == 0 || point.Y == 0 || point.X == Size.Width - 1 || point.Y == Size.Height - 1);
        }

        private IEnumerable<Point> GetEmptyCellsInLayout(int numofLayout)
        {
            if (numofLayout < 0 || numofLayout >= CountofLayouts)
                throw new Exception("В прямоугольнике нет столько слоёв");

            for (int x = numofLayout; x < Width - numofLayout; x++)
            {
                for (int y = numofLayout; y < Height - numofLayout; y++)
                {
                    if (x != numofLayout && x != Width - numofLayout - 1 && y != numofLayout && y != Height - numofLayout - 1)
                        continue;
                    else if (this[x, y] == 0)
                        yield return new Point(x, y);
                }
            }
        }
        #endregion
        #region Generation
        #region AsyncGeneration
        public async void GenerateLabirinthAsync()
        {
            await Task.Run(() => this.GenerateLabirinth());
        }
        #endregion
        public void GenerateLabirinth()
        {
            FillLabirinth();
            GenerationLabirinth();
        }

        public void GenerateLabirinth(Size newSize)
        {
            Size = newSize;
            GenerationLabirinth();
        }

        public void GenerateLabirinth(int width, int height)
        {
            GenerateLabirinth(new Size(width, height));
        }

        public void GenerateLabirinth(int size)
        {
            GenerateLabirinth(new Size(size));
        }
    
        private void GenerationLabirinth()
        {
            IEnumerable<Direction> GetAvaibleDirections(Point checkingPoint, IEnumerable<Point> points)
            {
                bool HasDiagonalNeighboor(Point point, Direction dir)
                {
                    Direction newDir = dir;
                    Point newPoint1 = point;
                    Point newPoint2 = point;

                    newPoint1.OffsetPoint(dir);
                    newPoint2.OffsetPoint(dir);

                    newPoint1.OffsetPoint((Direction)((int)dir == 1 || (int)dir == -1 ? 2 : 1));
                    newPoint2.OffsetPoint((Direction)((int)dir == 1 || (int)dir == -1 ? -2 : -1));

                    return (points.Contains(newPoint1) || points.Contains(newPoint2));
                }
                bool HasVisitedNeighboors(Point point, Point lastPoint)
                {
                    foreach (Direction dir in new Direction[] { Direction.Left, Direction.Right, Direction.Up, Direction.Down })
                    {
                        Point newPoint = point;
                        newPoint.OffsetPoint(dir);
                        if (newPoint == lastPoint)
                            continue;
                        if (points.Contains(newPoint))
                            return true;
                    }

                    return false;
                }
                bool IsExistInLab(Point point)
                {
                    return (0 <= point.X && 0 <= point.Y && point.X < Size.Width && point.Y < Size.Height);
                }

                List<Direction> result = new List<Direction>();

                foreach (Direction dir in new Direction[] { Direction.Left, Direction.Right, Direction.Up, Direction.Down})
                {
                    Point extraPoint = checkingPoint;//points.Last();
                    extraPoint.OffsetPoint(dir);
                    if (IsBorder(extraPoint))
                        continue;
                    else if (!IsExistInLab(extraPoint))
                        continue;
                    else if (points.Contains(extraPoint))
                        continue;
                    else if (HasVisitedNeighboors(extraPoint, checkingPoint) )
                        continue;
                    else if (HasDiagonalNeighboor(extraPoint, dir))
                        continue;
                    else
                        result.Add(dir);
                }

                return result;
            }
            IEnumerable<Point> GetAvaiblePointsToMove(IEnumerable<Point> checkingPoints)
            {
                foreach (Point point in checkingPoints)
                {
                    if (GetAvaibleDirections(point, checkingPoints).Count() != 0)
                        yield return point;
                }
            }
            
            IEnumerable<Point> GetWallsCells(IEnumerable<Point> emptySpace)
            {
                for (int x = 1; x < lab.GetLength(0) - 1; x++)
                {
                    for (int y = 1; y < lab.GetLength(1) - 1; y++)
                    {
                        if (!emptySpace.Contains(new Point(x, y)))
                            yield return new Point(x, y);
                    }
                }
            }

            int countofEmptySpace = ((int)(percentofEmptySpace * new Size(size.Width - 2, size.Height - 2).Square));

            List<Point> firstWay = new List<Point>(1);

            List<Point> secondWay = new List<Point>();

            List<Point> workingList = firstWay;

            Point movingPoint = GetRandomLayoutPoint(1);

            workingList.AddUnique(movingPoint);

            List<Point> visitedList = new List<Point>();

            while (firstWay.Count + secondWay.Count != countofEmptySpace)//(visitedPoints.Count != countofEmptySpace)
            {
                Point oldPoint = movingPoint;

                if (movingPoint.IsZero())
                {
                    if (workingList.Count != countofEmptySpace)
                    {
                        IEnumerable<Point> walls = GetWallsCells(ListUnique.UniteUnique(firstWay, secondWay));

                        if (walls.Count() == 0)
                            break;
                        else
                        {
                            Point newPoint = GetRandomPointFromList(walls);
                            if (firstWay.Contains(newPoint))
                                workingList = firstWay;
                            else if (secondWay.Contains(newPoint))
                                workingList = secondWay;
                            workingList.AddUnique(newPoint);
                            continue;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                if (secondWay.Count == 0 && workingList.Count < countofEmptySpace / 2 && workingList.Count > countofEmptySpace  / 3 && countofEmptySpace >= 4)
                {
                    int doSecWay = random.Next(0, 101);
                    if (doSecWay > 75)
                    {

                        do
                        {
                            movingPoint = GetRandomLayoutPoint(1);
                            bool f = firstWay.Contains(movingPoint);
                            bool b = GetAvaiblePointsToMove(firstWay).Contains(movingPoint);
                        } 
                        while (firstWay.Contains(movingPoint) || GetAvaiblePointsToMove(firstWay).Contains(movingPoint));

                        //firstWay.AddRange(visitedPoints);
                        workingList = secondWay;
                        workingList.AddUnique(movingPoint);
                        //secWay = visitedPoints.Count - 1;
                        continue;
                    }
                }

                Direction dir = GetRandomDirectionFromList(GetAvaibleDirections(movingPoint, ListUnique.UniteUnique(firstWay, secondWay)));  

                movingPoint.OffsetPoint(dir);

                if (IsBorder(movingPoint) || ListUnique.UniteUnique(firstWay, secondWay).Contains(movingPoint))
                {
                    if (dir == Direction.None)
                    {
                        do
                        {
                            movingPoint = GetRandomPointFromList(GetAvaiblePointsToMove(ListUnique.UniteUnique(firstWay, secondWay)));
                            if (movingPoint.IsZero())
                                break;
                        }
                        while (movingPoint.IsZero());
                        if (firstWay.Contains(movingPoint))
                            workingList = firstWay;
                        else if (secondWay.Contains(movingPoint))
                            workingList = secondWay;
                        
                        continue;
                    }
                    movingPoint = oldPoint;
                    continue;
                }

                workingList.AddUnique(movingPoint);
            }

            this.firstWay.AddRange(firstWay);
            this.secondWay.AddRange(secondWay);

            UpdateLabirinth();

            GenerateInsAndExit();
        }

        public void RegenerateInsAndExit()
        {
            GenerateInsAndExit();
        }

        private void GenerateInsAndExit()
        {
            List<Point> preborderPoints = GetEmptyCellsInLayout(1).ToList();

            do
            {
                Point exitPointOne = GetRandomPointFromList(preborderPoints);

                if (firstWay.Contains(exitPointOne))
                {
                    if (firstIn.IsZero())
                    {
                        //this[exitPointOne] = 2;
                        firstIn = exitPointOne;
                    }
                    else if (exit.IsZero())
                    {
                        //this[exitPointOne] = 3;
                        exit = exitPointOne;
                    }
                }
                else if (secondWay.Contains(exitPointOne) && secIn.IsZero())
                {
                    //this[exitPointOne] = 2;
                    secIn = exitPointOne;
                }
                //if (firstIn == exit && preborderPoints.Count != 1 && !firstIn.IsZero())
                if (firstIn == exit && preborderPoints.Intersect(firstWay).Count() != 1 && !firstIn.IsZero())
                {
                    this[exit] = 0;
                    exit = Point.Empty;
                    continue;
                }
            }
            while (firstIn.IsZero() || (secIn.IsZero() && secondWay.Count != 0) || exit.IsZero());

            if (firstIn == exit || secIn == exit)
                this[firstIn] = 4;
            else
            {
                this[firstIn] = 2;
                this[exit] = 3;
                if (!secIn.IsZero())
                    this[secIn] = 2;
            }
        }

        private void UpdateLabirinth()
        {
            foreach (var item in firstWay)
            {
                this[item] = 0;
            }
            if (secondWay.Count != 0)
                foreach (var item in secondWay)
                {
                    this[item] = 0;
                }
        }
        #endregion
        #region GetWays
        private List<Point> GetWay(Point starstPoint)
        {
            IEnumerable<Direction> GetAvaibleDirectionsToMove(Point checkingPoint, IEnumerable<Point> exceptionPoints = null)
            {
                bool IsExistInLab(Point point)
                {
                    return (0 <= point.X && 0 <= point.Y && point.X < Size.Width && point.Y < Size.Height);
                }

                List<Direction> result = new List<Direction>();

                foreach (Direction dir in new Direction[] { Direction.Left, Direction.Right, Direction.Up, Direction.Down })
                {
                    Point extraPoint = checkingPoint;
                    extraPoint.OffsetPoint(dir);
                    if (IsBorder(extraPoint))
                        continue;
                    else if (!IsExistInLab(extraPoint))
                        continue;
                    else if (exceptionPoints != null && exceptionPoints.Contains(extraPoint))
                        continue;
                    else if (this[extraPoint] == 1)
                        continue;
                    else
                        result.Add(dir);
                }
                return result;
            }

            List<Point> way = new List<Point>();
            way.Add(starstPoint);

            Stack<Point> fork = new Stack<Point>();

            List<Point> visitedPoints = new List<Point>();

            Point walker = starstPoint;

            while (true)
            {
                if (walker == exit)
                {
                    break;
                }

                IEnumerable<Direction> avaibleDirs = GetAvaibleDirectionsToMove(walker, visitedPoints);

                if (avaibleDirs.Count() == 1)
                {
                    visitedPoints.Add(walker);
                    walker.OffsetPoint(avaibleDirs.ElementAt(0));
                    way.Add(walker);
                }
                else if (avaibleDirs.Count() == 0)
                {
                    if (fork.Count == 0)
                        break;
                    else
                    {
                        walker = fork.Pop();
                        visitedPoints.Add(way[way.IndexOf(walker) + 1]);
                        way.RemoveSinceUnique(way.IndexOf(walker) + 1);
                        continue;
                    }
                }
                else
                {
                    fork.Push(walker);
                    visitedPoints.Add(walker);
                    walker.OffsetPoint(GetRandomDirectionFromList(avaibleDirs));
                    way.Add(walker);
                }
            }
            return way;
        }

        public List<Point> GetWay()
        {
            if (firstIn.IsZero())
                return new List<Point>();
            return GetWay(firstIn);
        }

        public List<Point> GetFirstWay()
        {
            return GetWay();
        }

        public List<Point> GetSecondWay()
        {
            bool IsSecAndFirstWaysConnected()
            {
                if (secondWay.Count == 0 || secIn.IsZero())
                    return false;
                foreach (Point point in secondWay)
                {
                    foreach (Direction direction in new Direction[4] { Direction.Up, Direction.Down, Direction.Left, Direction.Right })
                    {
                        Point newPoint = point;
                        newPoint.OffsetPoint(direction);
                        if (firstWay.Contains(newPoint))
                            return true;
                    }
                }
                return false;
            }

            if (IsSecAndFirstWaysConnected())
                return GetWay(secIn);
            return new List<Point>();
        }
        #endregion
        #endregion
    }
}
