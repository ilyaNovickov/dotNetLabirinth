using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabirinthLib;
using LabirinthLib.Structs;
using LabirinthLib.Printers;

namespace ConsoleLabirinthApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Labirinth lab = new Labirinth();

            lab.DoDebugLab2();

            lab.Print();

            Console.WriteLine(lab.FirstIn);
            Console.WriteLine(lab.Exit + "\n");

            string str = "1 6";
            do
            {
                var asd = str.Split(' ');
                if (asd.All(value => int.TryParse(value, out int num)))
                {
                    lab.ResetExit();
                    lab.SetExit(new Point(int.Parse(asd.First()), int.Parse(asd.Last())));
                    lab.Print();
                    Console.WriteLine(lab.FirstIn);
                    Console.WriteLine(lab.Exit + "\n");
                }
                else
                {
                    break;
                }

                str = Console.ReadLine();
            }
            while (true);     
        }

        //static void Main(string[] args)
        //{
        //    List<int> ints = new List<int>()
        //    {
        //        50, 30, 20, 30, 50
        //    };
        //    List<int> intsToDelete = new List<int>()
        //    {
        //        30, 50
        //    };

        //    foreach (int i in ints.IndexesofRange(intsToDelete))
        //    {
        //        Console.WriteLine(i);
        //    }

        //    Console.ReadLine();
        //}

        //static void Main(string[] args)
        //{
        //    Console.WriteLine("---Команды---\n" + 
        //                        "exit - выход из программы\n" +
        //                        "sizeN - изменить размер лабиринта на квадратный со сторонами N\n" + 
        //                        "1,0 = % пустого процента (любоу дробное число)\n" +
        //                        "bot|bot2 - вывод пути выхода из лабиринта БЕЗ тупиков\n" +
        //                        "botall|botall2 - вывод пути выхода из лабиринта С тупиками\n" +
        //                        "-------------");
        //    Labirinth lab = new Labirinth(10, 10);
        //    lab.EmptySpace = 0.4f;
        //    lab.GenerateLabirinth();
        //    lab.Print();
        //    Console.WriteLine(lab.CountofEmptyCells);
        //    Console.ReadLine();
        //    while (true)
        //    {
        //        lab.GenerateLabirinth();
        //        lab.Print();
        //        Console.WriteLine(lab.CountofEmptyCells);
        //        string str = Console.ReadLine();

        //        if (str == "exit")
        //            break;
        //        else if (float.TryParse(str, out float space))
        //            lab.EmptySpace = space;
        //        else if (str.StartsWith("size"))
        //        {
        //            if(int.TryParse(str.Substring(4), out int size))
        //            {
        //                lab.Size = new Size(size, size);
        //            }
        //        }
        //        else if (str == "bot")
        //        {
        //            lab.PrintWithBotWay();
        //            Console.ReadLine();
        //        }
        //        else if (str == "bot2")
        //        {
        //            lab.PrintWithBotWay(2);
        //            Console.ReadLine();
        //        }
        //        else if (str == "botall")
        //        {
        //            lab.PrintWithBotAllWay();
        //            Console.ReadLine();
        //        }
        //        else if (str == "botall2")
        //        {
        //            lab.PrintWithBotAllWay(2);
        //            Console.ReadLine();
        //        }
        //    }
        //}
    }
}
