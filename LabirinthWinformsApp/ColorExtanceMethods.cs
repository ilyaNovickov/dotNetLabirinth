using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LabirinthWinformsApp
{
    public static class ColorExtanceMethods
    {
        public static void Invert(ref this Color color)
        {
            color = GetInvert(color);
        }

        public static Color GetInvert(this Color color)
        {
            byte r = (byte)(byte.MaxValue - color.R);
            byte g = (byte)(byte.MaxValue - color.G);
            byte b = (byte)(byte.MaxValue - color.B);
            return Color.FromArgb(r, g, b);
        }
    }
}
