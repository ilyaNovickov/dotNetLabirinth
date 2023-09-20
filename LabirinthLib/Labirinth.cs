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
        float countofEmptySpace = 0.4f;
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

            CreateInsAndExit();
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
            get => countofEmptySpace;
            set
            {
                if (0 < value && value < 1f)
                {
                    countofEmptySpace = value;
                }
                else
                {
                    countofEmptySpace = 0.4f;
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

        private void CreateInsAndExit()
        {
            firstIn = GenerateRandomBorderPoint();

            do
            {
                secIn = GenerateRandomBorderPoint();
            }
            while (secIn != firstIn);

            do
            {
                exit = GenerateRandomBorderPoint();
            }
            while (exit != firstIn && exit != secIn);
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

    }
}
