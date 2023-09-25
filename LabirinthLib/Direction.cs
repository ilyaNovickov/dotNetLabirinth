using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabirinthLib
{
    public enum Direction : sbyte
    {
        None = 0,
        Up = 1,
        Down = -1, 
        Left = -2, 
        Right = 2
    }
}
