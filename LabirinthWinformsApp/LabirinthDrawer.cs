using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using LabirinthLib;
using LabirinthLib.Structs;

namespace LabirinthWinformsApp
{
    public static class LabirinthDrawer
    {
        public static void DrawLabirinth(this Labirinth lab, Graphics g)
        {
            Pen pen = new Pen(Color.Black, 2);
            SolidBrush brush = new SolidBrush(Color.White);

            g.DrawRectangle(pen, 0, 0, lab.Width, lab.Height);
        }
    }
}
