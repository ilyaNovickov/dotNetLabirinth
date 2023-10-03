using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabirinthLib
{
    public static class ConsolePrinter
    {
        public static void Print(this Labirinth labirinth)
        {
            for (int y = 0; y < labirinth.Height; y++)
            {
                for (int x = 0; x < labirinth.Width; x++)
                {
                    Point point = new Point(x, y);
                    if (point == labirinth.FirstIn && point == labirinth.Exit)
                        Console.BackgroundColor = ConsoleColor.Yellow;
                    else if (point == labirinth.FirstIn || point == labirinth.SecondIn)
                        Console.BackgroundColor = ConsoleColor.Red;
                    else if (point == labirinth.Exit)
                        Console.BackgroundColor = ConsoleColor.Blue;
                    else
                        Console.BackgroundColor = ConsoleColor.Black;

                    if (labirinth[point] == 1)
                        Console.Write("█");
                    else
                        Console.Write(" ");
                }
                Console.Write("\n");
            }
        }

        public static void PrintWithWay(this Labirinth labirinth, bool secWay = false)
        {
            List<Point> way;
            if (!secWay)
                way = labirinth.GetWay();
            else
                way = labirinth.GetSecondWay();

            for (int y = 0; y < labirinth.Height; y++)
            {
                for (int x = 0; x < labirinth.Width; x++)
                {
                    Point point = new Point(x, y);
                    if (point == labirinth.FirstIn && point == labirinth.Exit)
                        Console.BackgroundColor = ConsoleColor.Yellow;
                    else if (point == labirinth.FirstIn || point == labirinth.SecondIn)
                        Console.BackgroundColor = ConsoleColor.Red;
                    else if (point == labirinth.Exit)
                        Console.BackgroundColor = ConsoleColor.Blue;
                    else
                        Console.BackgroundColor = ConsoleColor.Black;

                    if (labirinth[point] == 1)
                        Console.Write("█");
                    else if (way.Contains(new Point(x, y)))
                        Console.Write("8");
                    else
                        Console.Write(" ");
                }
                Console.Write("\n");
            }
        }
    }
}
