using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabirinthLib;

namespace ConsoleLabirinthApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Labirinth lab = new Labirinth(10, 10);
            lab.EmptySpace = 0.4f;
            lab.RegenarateLabirinth();
            lab.Print();
            Console.WriteLine(lab.CountofEmptyCells);
            Console.ReadLine();
            while (true)
            {
                lab.RegenarateLabirinth();
                lab.Print();
                Console.WriteLine(lab.CountofEmptyCells);
                string str = Console.ReadLine();

                if (str == "exit")
                    break;
                else if (float.TryParse(str, out float space))
                    lab.EmptySpace = space;
                else if (str.StartsWith("size"))
                {
                    if(int.TryParse(str.Substring(4), out int size))
                    {
                        lab.Size = new Size(size, size);
                    }
                }
            }
        }
    }
}
