using System;

namespace LabirinthLib.Structs
{
    /// <summary>
    /// Структура размера
    /// </summary>
    [Serializable]
    public struct Size
    {
        #region Vars
        private int width, height;
        #endregion
        #region Constr
        /// <summary>
        /// Инициализирует структуру размера согласно параметрам
        /// </summary>
        /// <param name="width">Ширина</param>
        /// <param name="height">Высота</param>
        /// <exception cref="Exception"></exception>
        public Size(int width, int height)
        {
            if (width < 0 || height < 0)
                throw new Exception("Отрицательный размер");
            this.width = width;
            this.height = height;
        }
        /// <summary>
        /// Инициализирует структуру размера согласно параметру
        /// </summary>
        /// <param name="size">Значение ширины и высоты</param>
        public Size(int size) : this(size, size) { }
        #endregion
        #region Props
        /// <summary>
        /// Ширина размера
        /// </summary>
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
        /// <summary>
        /// Высота размера
        /// </summary>
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
        /// <summary>
        /// Площадь размера
        /// </summary>
        public int Square => width * height;
        /// <summary>
        /// Нулевой размер
        /// </summary>
        public static Size Empty => new Size(0, 0);
        #endregion
        #region Methods
        //Переопределения методов базового класса object (просто так)
        public override int GetHashCode()
        {
            return width.GetHashCode() ^ height.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj is Size size)
            {
                return (this.width == size.width && this.height == size.height);
            }
            return false;
        }
        public override string ToString()
        {
            return this.width + "x" + this.height;
        }
        #region Convertion operation
        public static implicit operator System.Drawing.Size(Size size)
        {
            return new System.Drawing.Size(size.Width, size.Height);
        }

        public static explicit operator Size(System.Drawing.Size size)
        {
            return new Size(size.Width, size.Height);
        }
        #endregion
        #region Operators
        //Переопределения операторов проверки на равенства и мат операторы
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
        #endregion
        #endregion
    }
}
