using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabirinthLib
{
    public class Labirinth
    {
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
                if (value.Width < 3 && value.Height < 3)
                    return;
                lab = new int[value.Width, value.Height];
            }
        }

        public Point FirstIn => firstIn;
        public Point SecondIn => secIn;
        public Point Exit => exit;
    }
}
