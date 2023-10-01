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
        public static void OffsetPoint(this ref Point point, params Direction[] dirs)
        {
            int dx = 0;
            int dy = 0;
            foreach (Direction direction in dirs)
            {
                switch (direction)
                {
                    case Direction.Up:
                    case Direction.Down:
                        dy += (int)direction;
                        break;
                    case Direction.Left:
                    case Direction.Right:
                        dx += (int)direction / 2;
                        break;
                }
            }
            point.Offset(dx, dy);
        }
    }    
}
