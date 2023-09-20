using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabirinthLib
{
    public class Labirinth
    {
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

        public Labirinth(int width, int height) : this(new Size(width, height))
        {

        }

        public Labirinth(Size size)
        {
            this.Size = size;
            DrawBorder();
            CreateInsAndExit();
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
                if (0 < value && value < 1f)
                {
                    percentofEmptySpace = value;
                }
                else
                {
                    percentofEmptySpace = 0.4f;
                }
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
            }
        }

        public Point FirstIn => firstIn;
        public Point SecondIn => secIn;
        public Point Exit => exit;

        private void DrawBorder()
        {
            for (int x = 0; x < lab.GetLength(0); x++)
            {
                this[x, 0] = 1;
                this[x, Size.Height - 1] = 1;
            }
            for (int y = 0; y < lab.GetLength(1); y++)
            {
                this[0, y] = 1;
                this[Size.Width - 1, y] = 1;
            }
        }

        private void CreateInsAndExit()
        {
            firstIn = GenerateRandomBorderPoint();

            do
            {
                secIn = GenerateRandomBorderPoint();
            }
            while (secIn == firstIn);

            do
            {
                exit = GenerateRandomBorderPoint();
            }
            while (exit == firstIn && exit == secIn);

            this[firstIn] = 2;
            this[secIn] = 2;
            this[exit] = 3;
        }

        private Point GenerateRandomBorderPoint()
        {
            int x = random.Next(0, Size.Width - 1);
            int y = -1;
            if (x == 0 || x == Size.Width - 1)
            {
                y = random.Next(1, Size.Height - 2);
            }
            else
            {
                do
                {
                    y = random.Next(0, Size.Height - 1 );
                }
                while (y != 0 && y != Size.Height - 1);
            }

            return new Point(x, y);
        }

        public void Print()
        {
            for (int x =0; x < lab.GetLength(0); x++)
            {
                for (int y = 0; y < lab.GetLength(1); y++)
                {
                    Console.Write(this[x, y]);
                }
                Console.Write("\n");
            }
        }

        private void test()
        {
            int countofEmptySpace = ((int)(percentofEmptySpace * size.Square));
            int usedSpace = 0;
            List<Point> visitedPoint = new List<Point>();

            Point currentPoint = firstIn;
            Direction dir = GetBorderofPoint(firstIn);
            MovePointByDirection(ref currentPoint, dir);
            visitedPoint.Add(currentPoint);
            usedSpace++;

            int whileBreaker = 0;
            while (true)
            {
                if (whileBreaker == 3000)
                    return;

                Direction oldDir = dir;
                dir = ((Direction)random.Next(0, 4));
                if (dir == oldDir)
                {
                    dir = oldDir;
                    continue;
                }
                MovePointByDirection(ref currentPoint, dir);
                visitedPoint.Add(currentPoint);
                usedSpace++;

                if (usedSpace == countofEmptySpace)
                {
                    Point exitPoint = new Point();
                    MovePointByDirection(ref exitPoint, GetBorderofPoint(exit));
                    if (visitedPoint.Contains(exitPoint))
                    {
                        UpdateLab(visitedPoint);
                        break;
                    }
                }

                whileBreaker++;
            }
        }

        private void UpdateLab(List<Point> wayPoints)
        {
            foreach (var item in wayPoints)
            {
                this[item] = 5;
            }
        }
        private Direction GetBorderofPoint(Point point)
        {
            if (point.X == 0 && (1 < point.Y || point.Y < size.Height - 2))
                return Direction.Left;
            else if (point.X == size.Width - 1 && (1 < point.Y || point.Y < size.Height - 2))
                return Direction.Right;
            else if (point.Y == 0 && (1 < point.X || point.X < size.Width - 2))
                return Direction.Up;
            else if (point.Y == size.Height - 1 && (1 < point.X || point.X < size.Width - 2))
                return Direction.Down;
            else
                return Direction.None;
        }

        private void MovePointByDirection(ref Point point, Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    point.Y--;
                    break;
                case Direction.Down:
                    point.Y++;
                    break;
                case Direction.Left:
                    point.X++;
                    break;
                case Direction.Right:
                    point.X++;
                    break;
            }
        }
    }
}
