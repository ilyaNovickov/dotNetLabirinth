using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LabirinthLib
{
    public struct Point
    {
        #region Vars
        private int x, y;
        #endregion
        #region Constr
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Point(int coord) : this(coord, coord) { }
        #endregion
        #region Props
        public int X { get { return x; } set {  x = value; } }
        public int Y { get { return y; } set { y = value; } }
        public static Point Empty => new Point(0, 0);
        #endregion
        #region Methods
        public void Offset(int dx, int dy)
        {
            this.x += dx;
            this.y += dy;
        }

        public Point GetOffsetedPoint(int dx, int dy)
        {
            Point newPoint = this;
            newPoint.Offset(dx, dy);
            return newPoint;
        }

        public bool IsZero()
        {
            return this == Point.Empty;
        }

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
        #region Operators
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
