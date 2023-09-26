using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabirinthLib
{
    public class Labirinth
    {
        /*
         * 0 - empty
         * 1 - wall
         * 3 - in
         * 4 - exit
         * 5 - in and exit
         */
        Random random = new Random();
        float percentofEmptySpace = 0.4f;
        Size size;
        Point firstIn;
        Point secIn;
        Point exit;
        int[,] lab;

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
            //DrawBorder();
            test();
        }

        private int this[Point point]
        {
            get => lab[point.X, point.Y];
            set => lab[point.X, point.Y] = value;
        }

        private int this[int x, int y]
        {
            get => lab[x, y];
            set => lab[x, y] = value;
        }

        public float EmptySpace
        {
            get => percentofEmptySpace;
            set
            {
                if (0.12 <= value && value < 1f)
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

        public Point FirstIn => firstIn;
        public Point SecondIn => secIn;
        public Point Exit => exit;

        public int CountofEmptyCells
        {
            get
            {
                int count = 0;
                for (int x = 1; x < lab.GetLength(0) - 1; x++)
                {
                    for (int y = 1; y < lab.GetLength(1) - 1; y++)
                    {
                        count += this[x, y] == 0 ? 1 : 0;
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

        //private void DrawBorder()
        //{
        //    for (int x = 0; x < lab.GetLength(0); x++)
        //    {
        //        this[x, 0] = 1;
        //        this[x, Size.Height - 1] = 1;
        //    }
        //    for (int y = 0; y < lab.GetLength(1); y++)
        //    {
        //        this[0, y] = 1;
        //        this[Size.Width - 1, y] = 1;
        //    }
        //}
        private void FillLabirinth()
        {
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

        //private Point GenerateRandomInsidePoint()
        //{
        //    int x = random.Next(, Size.Width - 1);
        //    int y = -1;
        //    if (x == 0 || x == Size.Width - 1)
        //    {
        //        y = random.Next(1, Size.Height - 2);
        //    }
        //    else
        //    {
        //        do
        //        {
        //            y = random.Next(0, Size.Height - 1);
        //        }
        //        while (y != 0 && y != Size.Height - 1);
        //    }

        //    return new Point(x, y);
        //}

        public void Print()
        {
            for (int x = 0; x < lab.GetLength(0); x++)
            {
                for (int y = 0; y < lab.GetLength(1); y++)
                {
                    if (this[x, y] == 2 || this[x, y] == 3)
                        Console.BackgroundColor = ConsoleColor.Red;
                    else
                        Console.BackgroundColor = ConsoleColor.Black;
                    if (this[x, y] == 1)
                        Console.Write("█");
                    else
                        Console.Write(this[x, y]);
                }
                Console.Write("\n");
            }
        }

        public void RegenarateLabirinth()
        {
            FillLabirinth();
            test();
        }

        public void RegenarateLabirinth(Size newSize)
        {
            Size = newSize;
            test();
        }
        public void RegenarateLabirinth(int width, int height)
        {
            RegenarateLabirinth(new Size(width, height));
        }
        public void RegenarateLabirinth(int size)
        {
            RegenarateLabirinth(new Size(size));
        }

        private void test()
        {
            bool IsBorder(Point point)
            {
                return (point.X == 0 || point.Y == 0 || point.X == Size.Width - 1 || point.Y == Size.Height - 1);
            }
            void MovePointByDirection(ref Point point, Direction direction)
            {
                switch (direction)
                {
                    case Direction.Up:
                    case Direction.Down:
                        point.Y += (int)direction;
                        break;
                    case Direction.Left:
                    case Direction.Right:
                        point.X += (int)direction / 2;
                        break;
                }
            }
            Direction GetRandomDirection(IEnumerable<Direction> avaibleDirs)
            {
                if (avaibleDirs.Count() == 0)
                    return Direction.None;
                return avaibleDirs.ElementAt(random.Next(0, avaibleDirs.Count<Direction>()));
            }
            IEnumerable<Direction> GetAvaibleDirections(Point checkingPoint, IEnumerable<Point> points)
            {
                bool HasDiagonalNeighboor(Point point, Direction dir)
                {
                    Direction newDir = dir;
                    Point newPoint1 = point;
                    Point newPoint2 = point;

                    MovePointByDirection(ref newPoint1, dir);
                    MovePointByDirection(ref newPoint2, dir);

                    MovePointByDirection(ref newPoint1, (Direction)((int)dir == 1 || (int)dir == -1 ? 2 : 1));
                    MovePointByDirection(ref newPoint2, (Direction)((int)dir == 1 || (int)dir == -1 ? -2 : -1));

                    if (points.Contains(newPoint1) || points.Contains(newPoint2))
                        return true;
                    return false;

                }
                bool HasVisitedNeighboors(Point point, Point lastPoint)
                {
                    foreach (Direction dir in new Direction[] { Direction.Left, Direction.Right, Direction.Up, Direction.Down })
                    {
                        Point newPoint = point;
                        MovePointByDirection(ref newPoint, dir);
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
                    MovePointByDirection(ref extraPoint, dir);
                    if (IsBorder(extraPoint))
                        continue;
                    else if (!IsExistInLab(extraPoint))
                        continue;
                    else if (points.Contains(extraPoint))
                        continue;
                    else if (HasVisitedNeighboors(extraPoint, checkingPoint))
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
            Point GetRandomPointFromList(IEnumerable<Point> list)
            {
                if (list.Count() == 0)
                    return Point.Empty;
                return list.ElementAt(random.Next(0, list.Count()));
            }
            IEnumerable<Point> GetWallsCells(IEnumerable<Point> emptySpace)
            {
                //List<Point> walls = new List<Point>();
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

            List<Point> visitedPoints = new List<Point>(1);

            Point movingPoint = GetRandomLayoutPoint(1);

            visitedPoints.Add(movingPoint);

            while (visitedPoints.Count != countofEmptySpace)//Для генерации лабиринта без ограничений
            //while (GetAvaiblePointsToMove(visitedPoints).Count() != 0)
            {
                if (movingPoint == Point.Empty)
                {
                    if (visitedPoints.Count != countofEmptySpace)
                    {
                        IEnumerable<Point> walls = GetWallsCells(visitedPoints);

                        if (walls.Count() == 0)
                            break;
                        else
                        {
                            visitedPoints.Add(GetRandomPointFromList(walls));
                            continue;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                Direction dir = GetRandomDirection(GetAvaibleDirections(movingPoint, visitedPoints));

                Point oldPoint = movingPoint;

                MovePointByDirection(ref movingPoint, dir);

                if (IsBorder(movingPoint) || visitedPoints.Contains(movingPoint))
                {
                    if (dir == Direction.None)//=============
                    {
                        do
                        {
                            movingPoint = GetRandomPointFromList(GetAvaiblePointsToMove(visitedPoints));
                            if (movingPoint == Point.Empty)
                                break;
                        }
                        while (movingPoint == Point.Empty);
                        continue;
                    }
                    movingPoint = oldPoint;
                    continue;
                }

                visitedPoints.Add(movingPoint);
            }


            /*
            List<Point> visitedPoint = new List<Point>();

            List<Point> firstWay = new List<Point>();

            Point point1 = GetRandomLayoutPoint(1);

            visitedPoint.Add(point1);

            //Direction[] dirs = new Direction[4];
            List<Direction> dirs = new List<Direction>(4);
            
            
            while (visitedPoint.Count != countofEmptySpace)
            {

                Direction dir = ((Direction)random.Next(1, 4 + 1));

                Point oldPoint = point1;

                MovePointByDirection(ref point1, dir);

                if (IsBorder(point1) || visitedPoint.Contains(point1))
                {
                    if (dirs.Count == 4)
                    {
                        firstWay.AddRange(visitedPoint);
                        do
                        {
                            point1 = GetRandomLayoutPoint(1);
                        }
                        while (!visitedPoint.Contains(point1));
                        visitedPoint.Add(point1);
                        continue;
                    }

                    if (!dirs.Contains(dir))
                        dirs.Add(dir);
                    point1 = oldPoint;
                    continue;
                }

                dirs.Clear();

                visitedPoint.Add(point1);

                if (visitedPoint.Count == countofEmptySpace)
                    break;
            }
            */

            foreach (var item in visitedPoints)
            {
                this[item] = 0;
            }
            //foreach (var item in firstWay)
            //{
            //    this[item] = 5;
            //}

            CreateInsAndExit();
        }

        private void CreateInsAndExit()
        {
            //List<Point> points = new List<Point>();

            //for (int x = 1; x < Size.Width - 2; x++)
            //{
            //    if (this[x, 1] == 1)
            //        points.Add(new Point(x, 1));
            //}
        }

        //private void CreateInsAndExit()
        //{
        //    firstIn = GetRandomLayoutPoint();

        //    do
        //    {
        //        secIn = GetRandomLayoutPoint();
        //    }
        //    while (secIn == firstIn);

        //    do
        //    {
        //        exit = GetRandomLayoutPoint();
        //    }
        //    while (exit == firstIn || exit == secIn);

        //    this[firstIn] = 2;
        //    this[secIn] = 2;
        //    this[exit] = 3;
        //}


        //private Direction GetBorderofPoint(Point point)
        //{
        //    if (point.X == 0 && (1 < point.Y || point.Y < size.Height - 2))
        //        return Direction.Left;
        //    else if (point.X == size.Width - 1 && (1 < point.Y || point.Y < size.Height - 2))
        //        return Direction.Right;
        //    else if (point.Y == 0 && (1 < point.X || point.X < size.Width - 2))
        //        return Direction.Up;
        //    else if (point.Y == size.Height - 1 && (1 < point.X || point.X < size.Width - 2))
        //        return Direction.Down;
        //    else
        //        return Direction.None;
        //}


    }
}
