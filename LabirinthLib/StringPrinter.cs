using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabirinthLib
{
    public static class StringPrinter
    {
        public static string GetStringLabirinth(this Labirinth lab)
        {
            StringBuilder stringBuilder = new StringBuilder();

            const string wall = "1";
            const string empty = "0";
            const string enter = "3";
            const string exit = "4";
            const string exitAndEnter = "5";

            for (int x = 0; x < lab.Width; x++)
            {
                for (int y = 0; y < lab.Height; y++)
                {
                    LabirinthLib.Structs.Point point = new LabirinthLib.Structs.Point(x, y);
                    if ((point == lab.FirstIn || point == lab.SecondIn) && point == lab.Exit)
                        stringBuilder.Append(exitAndEnter);
                    else if (point == lab.FirstIn || point == lab.SecondIn)
                        stringBuilder.Append(enter);
                    else if (point == lab.Exit)
                        stringBuilder.Append(exit);
                    else if (lab[point] == 1)
                        stringBuilder.Append(wall);
                    else if (lab[point] == 0)
                        stringBuilder.Append(empty);
                }
                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }
    }
}
