using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabirinthLib.Structs;

namespace LabirinthLib.Printers
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

        public static void PrintWithBotWay(this Labirinth labirinth)
        {
            PrintWithBotWay(labirinth, 1);
        }

        public static void PrintWithBotWay(this Labirinth labirinth, int numofWay)
        {
            List<Point> way = WalkerBot.GetWayFromLaborinth(labirinth, numofWay).ToList();

            if (way.Count == 0)
                return;

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
                    {
                        Console.Write((char)(way.IndexOf(point) + 65));
                    }
                    else
                        Console.Write(" ");
                }
                Console.Write("\n");
            }
        }

        public static void PrintWithBotAllWay(this Labirinth labirinth)
        {
            PrintWithBotAllWay(labirinth, 1);
        }

        public static void PrintWithBotAllWay(this Labirinth labirinth, int numofWay)
        {
            List<Point> way = WalkerBot.GetAllWayFromLaborinth(labirinth, numofWay);

            if (way.Count == 0)
                return;

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
                    {
                        Console.Write((char)(way.IndexOf(point) + 65));
                    }
                    else
                        Console.Write(" ");
                }
                Console.Write("\n");
            }
        }
    }
}
