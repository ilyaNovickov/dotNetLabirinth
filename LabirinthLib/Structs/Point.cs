using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LabirinthLib.Structs
{
    /// <summary>
    /// Структура координат в 2D пространстве
    /// </summary>
    [Serializable]
    public struct Point
    {
        #region Vars
        private int x, y;
        #endregion
        #region Constr
        /// <summary>
        /// Инициализирует структруру согласно параметрам
        /// </summary>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        /// <summary>
        /// Инициализирует структруру согласно параметру
        /// </summary>
        /// <param name="coord">Координата X и Y</param>
        public Point(int coord) : this(coord, coord) { }
        #endregion
        #region Props
        /// <summary>
        /// Значение X
        /// </summary>
        public int X { get { return x; } set {  x = value; } }
        /// <summary>
        /// Значение Y
        /// </summary>
        public int Y { get { return y; } set { y = value; } }
        /// <summary>
        /// Нулевая точка
        /// </summary>
        public static Point Empty => new Point(0, 0);
        #endregion
        #region Methods
        public override string ToString()
        {
            return $"({this.x}, {this.y}";
        }
        /// <summary>
        /// Смещение координаты на определённое значение
        /// </summary>
        /// <param name="dx">Смещение по оси X</param>
        /// <param name="dy">Смещение по оси Y</param>
        public void Offset(int dx, int dy)
        {
            this.x += dx;
            this.y += dy;
        }
        /// <summary>
        /// Смещение координаты на определённое значение 
        /// и возврат копии структуры
        /// </summary>
        /// <param name="dx">Смещение по оси X</param>
        /// <param name="dy">Смещение по оси Y</param>
        /// <returns>Копия структуры, смещённая на определённые значения</returns>
        public Point GetOffsetedPoint(int dx, int dy)
        {
            Point newPoint = this;
            newPoint.Offset(dx, dy);
            return newPoint;
        }
        /// <summary>
        /// Проверка координаты на равенство нулю
        /// </summary>
        /// <returns></returns>
        public bool IsZero()
        {
            return this == Point.Empty;
        }
        //Переопределения методов базового класса object
        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj is Point point)
            {
                return (this.X == point.X && this.Y == point.Y);
            }
            return false;
        }
        #region Convertion operation
        public static implicit operator System.Drawing.Point(Point point)
        {
            return new System.Drawing.Point(point.X, point.Y);
        }

        public static explicit operator Point(System.Drawing.Point point)
        {
            return new Point(point.X, point.Y);
        }

        public static implicit operator System.Drawing.PointF(Point point)
        {
            return new System.Drawing.PointF(point.X, point.Y);
        }

        public static explicit operator Point(System.Drawing.PointF point)
        {
            return new Point((int)point.X, (int)point.Y);
        }
        #endregion
        #region Operators
        //Переопределения операторов проверки на равенство и мат операторы
        public static bool operator ==(Point point1, Point point2)
        {
            return (point1.X == point2.X && point1.Y == point2.Y);
        }

        public static bool operator !=(Point point1, Point point2)
        {
            return (point1.X != point2.X || point1.Y != point2.Y);
        }

        public static Point operator +(Point point1, Point point2)
        {
            return new Point(point1.X + point2.X, point1.Y + point2.Y);
        }

        public static Point operator +(Point point1, int value)
        {
            return new Point(point1.X + value, point1.Y + value);
        }

        public static Point operator - (Point point1, Point point2)
        {
            return new Point(point1.X - point2.X, point1.Y - point2.Y);
        }

        public static Point operator -(Point point1, int value)
        {
            return new Point(point1.X - value, point1.Y - value);
        }

        public static Point operator * (Point point1, Point point2)
        {
            return new Point(point1.X * point2.X, point1.Y * point2.Y);
        }

        public static Point operator * (Point point1, int value)
        {
            return new Point(point1.X * value, point1.Y * value);
        }

        public static Point operator / (Point point1, Point point2)
        {
            return new Point(point1.X / point2.X, point1.Y / point2.Y);
        }

        public static Point operator / (Point point1, int value)
        {
            return new Point(point1.X / value, point1.Y / value);
        }
        #endregion
        #endregion
    }
}
