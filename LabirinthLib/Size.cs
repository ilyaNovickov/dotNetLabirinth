using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
