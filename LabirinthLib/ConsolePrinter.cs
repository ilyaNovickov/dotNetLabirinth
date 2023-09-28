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
                    if (labirinth[x, y] == 2 || labirinth[x, y] == 3)
                        Console.BackgroundColor = ConsoleColor.Red;
                    else if (labirinth[x, y] == 6)
                        Console.BackgroundColor = ConsoleColor.Yellow;
                    else
                        Console.BackgroundColor = ConsoleColor.Black;
                    if (labirinth[x, y] == 1)
                        Console.Write("█");
                    else
                        Console.Write(labirinth[x, y]);
                }
                Console.Write("\n");
            }
        }
    }
}
