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
        /*
         * Так как нельзя нарисовать один пиксель (1х1)
         * то надо рисовать хотя бы 2х2
         */
        public static void DrawLabirinth(this Labirinth lab, Graphics g)
        {
            Pen pen1 = new Pen(Color.Black, 1);
            Pen pen2 = new Pen(Color.White, 1);
            SolidBrush brush = new SolidBrush(Color.White);

            g.DrawRectangle(pen1, 0, 0, lab.Width, lab.Height);

            //for (int x = 1; x < lab.Width - 1; x++)
            //{
            //    for (int y = 1; y < lab.Height - 1; y++)
            //    {
            //        if (lab[x, y] == 0)
            //            g.DrawRectangle(pen2, new Rectangle(x, y, 1, 1));
            //        else if (lab[x, y] == 1)
            //            g.DrawRectangle(pen1, new Rectangle(x, y, 1, 1));
            //    }
            //}

            g.DrawRectangle(new Pen(Color.Red, 1), 5, 5, 1, 1);
            g.DrawLine(new Pen(Color.Blue), 5, 5, 5, 5);

            g.FillRectangle(new SolidBrush(Color.Green), 0, 0, 1, 1);
            g.FillRectangle(new SolidBrush(Color.Yellow), 0, 0, 0.1f, 0.1f);
        }
    }
}
