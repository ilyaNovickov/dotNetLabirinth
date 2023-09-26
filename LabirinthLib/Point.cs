using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabirinthLib
{
    public struct Point
    {
        private int x, y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Point(int coord) : this(coord, coord) { }
        public int X { get { return x; } set {  x = value; } }
        public int Y { get { return y; } set { y = value; } }
        public static Point Empty => new Point(0, 0);

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
    }
}
