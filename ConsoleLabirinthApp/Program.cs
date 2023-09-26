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
            Labirinth lab = new Labirinth(5, 5);
            lab.EmptySpace = 0.6f;
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
            }
        }
    }
}
