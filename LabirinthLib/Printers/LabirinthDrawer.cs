using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using LabirinthLib;
using LabirinthLib.Structs;

namespace LabirinthWinformsApp
{
    public static class LabirinthDrawer
    {
        public static void DrawLabirinth(this Labirinth lab, Graphics g)
        {
            Color wall = Color.Black;
            Color empty = Color.White;
            Color enter = Color.Red;
            Color exit = Color.Blue;
            Color exitAndEnter = Color.Yellow;

            for (int y = 0; y < lab.Height; y++)
            {
                for (int x = 0; x < lab.Width; x++)
                {
                    Color color = Color.Black;
                    LabirinthLib.Structs.Point point = new LabirinthLib.Structs.Point(x, y);
                    if ((point == lab.FirstIn || point == lab.SecondIn) && point == lab.Exit)
                        color = exitAndEnter;
                    else if (point == lab.FirstIn || point == lab.SecondIn)
                        color = enter;
                    else if (point == lab.Exit)
                        color = exit;
                    else if (lab[point] == 1)
                        color = wall;
                    else if (lab[point] == 0)
                        color = empty;

                    using (SolidBrush brush = new SolidBrush(color))
                        g.FillRectangle(brush, point.X, point.Y, 1, 1);
                }
            }
        }

        [Obsolete]
        public static void DrawLabirinthOLD(this Labirinth lab, Graphics g)
        {
            Pen wall = new Pen(Color.Black, 1);
            Pen empty = new Pen(Color.White, 1);
            Pen enter = new Pen(Color.Red, 1);
            Pen exit = new Pen(Color.Blue, 1);
            Pen exitAndEnter = new Pen(Color.Yellow, 1);

            g.FillRectangle(new SolidBrush(Color.White), 0, 0, lab.Width, lab.Height);
            g.DrawRectangle(wall, 0, 0, lab.Width, lab.Height);

            for (int x = 1; x < lab.Width - 1; x++)
            {
                for (int y = 0; y < lab.Height - 1; y++)
                {
                    LabirinthLib.Structs.Point point = new LabirinthLib.Structs.Point(x, y);
                    if ((point == lab.FirstIn || point == lab.SecondIn) && point == lab.Exit)
                        g.DrawRectangle(exitAndEnter, point.X, point.Y, 0.5f, 0.5f);
                    else if (point == lab.FirstIn || point == lab.SecondIn)
                        g.DrawRectangle(enter, point.X, point.Y, 0.5f, 0.5f);
                    else if (point == lab.Exit)
                        g.DrawRectangle(exit, point.X, point.Y, 0.5f, 0.5f);
                    else if (lab[point] == 1)
                        g.DrawRectangle(wall, point.X, point.Y, 0.5f, 0.5f);
                    else if (lab[point] == 0)
                        g.DrawRectangle(empty, point.X, point.Y, 0.5f, 0.5f);
                }
            }
        }

        /*
         * Так как нельзя нарисовать один пиксель (1х1)
         * то надо рисовать хотя бы 2х2
         */
        //public static void DrawLabirinth(this Labirinth lab, Graphics g)
        //{
        //    Pen pen1 = new Pen(Color.Black, 1);
        //    Pen pen2 = new Pen(Color.White, 1);
        //    SolidBrush brush = new SolidBrush(Color.White);

        //    g.DrawRectangle(pen1, 0, 0, lab.Width, lab.Height);

        //    //for (int x = 1; x < lab.Width - 1; x++)
        //    //{
        //    //    for (int y = 1; y < lab.Height - 1; y++)
        //    //    {
        //    //        if (lab[x, y] == 0)
        //    //            g.DrawRectangle(pen2, new Rectangle(x, y, 1, 1));
        //    //        else if (lab[x, y] == 1)
        //    //            g.DrawRectangle(pen1, new Rectangle(x, y, 1, 1));
        //    //    }
        //    //}

        //    g.DrawRectangle(new Pen(Color.Red, 1), 5, 5, 1, 1);
        //    g.DrawLine(new Pen(Color.Blue), 5, 5, 5, 5);

        //    g.FillRectangle(new SolidBrush(Color.Green), 0, 0, 1, 1);
        //    g.FillRectangle(new SolidBrush(Color.Yellow), 0, 0, 0.5f, 0.5f);
        //}
    }
}
