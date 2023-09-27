using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabirinthLib
{
    public enum Direction : int//sbyte
    {
        None = 0,
        Up = -1,
        Down = 1, 
        Left = -2, 
        Right = 2
    }

    public static class DirectionExtandentClass
    {
        public static void OffsetPoint(this Point point, params Direction[] dirs)
        {
            foreach (Direction direction in dirs)
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
        }
    }

    public static class ListUnique
    {
        public static bool AddUnique(this List<Point> list, Point value)
        {
            if (list.Contains(value))
                return false;
            list.Add(value);
            return true;
        }

        public static List<Point> UniteUnique(this List<Point> originList, List<Point> list)
        {
            List<Point> result = new List<Point>();
            result.AddRange(originList);
            foreach (Point point in list)
            {
                if (originList.Contains(point))
                    continue;
                else
                    result.Add(point);
            }
            return result;
        }
    }
}
