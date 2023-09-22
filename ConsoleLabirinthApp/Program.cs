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
            Labirinth lab = new Labirinth(5);
            lab.Print();
            Console.ReadLine();
            while (true)
            {
                lab.RegenarateLabirinth();
                lab.Print();
                string str = Console.ReadLine();

                if (str == "exit")
                    break;
            }
        }
    }
}
