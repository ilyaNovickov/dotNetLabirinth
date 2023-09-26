using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace LabirinthLib
{
    public struct Size
    {
        int width, height;
        public Size(int width, int height)
        {
            if (width < 0 || height < 0)
                throw new Exception("Отрицательный размер");
            this.width = width;
            this.height = height;
        }
        public Size(int size) : this(size, size) { }
        public int Width
        {
            get => width;
            set
            {
                if (value < 0)
                    throw new Exception("Отрицательный размер");
                width = value;
            }
        }
        public int Height
        {
            get => height;
            set
            {
                if (value < 0)
                    throw new Exception("Отрицательный размер");
                height = value;
            }
        }

        public int Square => width * height;

        public static bool operator ==(Size size1, Size size2)
        {
            return (size1.Width == size2.Width && size2.Height == size1.Height);
        }

        public static bool operator !=(Size size1, Size size2)
        {
            return (size1.Width != size2.Width || size2.Height != size1.Height);
        }

        public static Size operator +(Size size1, Size size2)
        {
            return new Size(size1.Width + size2.Width, size1.Height + size2.Height);
        }

        public static Size operator +(Size size1, int value)
        {
            return new Size(size1.Width + value, size1.Height + value);
        }

        public static Size operator -(Size size1, Size size2)
        {
            return new Size(size1.Width - size2.Width, size1.Height - size2.Height);
        }

        public static Size operator -(Size size1, int value)
        {
            return new Size(size1.Width - value, size1.Height - value);
        }

        public static Size operator *(Size size1, Size size2)
        {
            return new Size(size1.Width * size2.Width, size1.Height * size2.Height);
        }

        public static Size operator *(Size size1, int value)
        {
            return new Size(size1.Width * value, size1.Height * value);
        }

        public static Size operator /(Size size1, Size size2)
        {
            return new Size(size1.Width / size2.Width, size1.Height / size2.Height);
        }

        public static Size operator /(Size size1, int value)
        {
            return new Size(size1.Width / value, size1.Height / value);
        }
    }
}
