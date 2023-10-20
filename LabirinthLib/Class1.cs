using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabirinthLib.Structs;

namespace LabirinthLib
{
    public class new_WalkerBot
    {
        private Labirinth lab;

        private Queue<Point> way;

        public new_WalkerBot(Labirinth lab)
        {
            this.lab = lab;
        }

        public Labirinth Labirinth
        {
            get => lab;
            set
            {
                lab = value;

                way.Clear();
            }
        }

        public Queue<Point> WayQueue => way;

        public List<Point> WayList => way.ToList();

        public event EventHandler<DeadEndFindEventArgs> DeadEndFintEvent;

        

        public bool FindExit(int numofEnter = 1)
        {
            //Метод получения доступных направлений для перемещения точек, исключая указанные
            IEnumerable<Direction> GetAvaibleDirectionsToMove(Point checkingPoint, IEnumerable<Point> exceptionPoints = null)
            {
                List<Direction> result = new List<Direction>();

                foreach (Direction dir in new Direction[] { Direction.Left, Direction.Right, Direction.Up, Direction.Down })
                {
                    Point extraPoint = checkingPoint;
                    extraPoint.OffsetPoint(dir);
                    //Пропускаем те точки, которых нет в лабиринте и те, которые являются исключающими
                    //а также, те которые - стена и невыход
                    if (!lab.IsExistInLab(extraPoint))
                        continue;
                    else if (exceptionPoints != null && exceptionPoints.Contains(extraPoint))
                        continue;
                    else if (lab[extraPoint] == 1 && extraPoint != lab.Exit)
                        continue;
                    else
                        result.Add(dir);
                }
                return result;
            }

            Point walker;

            switch (numofEnter)
            {
                case 1:
                    walker = lab.FirstIn;
                    break;
                case 2:
                    walker = lab.SecondIn;
                    break;
                default:
                    throw new Exception("Нет таково входа в лабиринт");
            }

            if (walker.IsZero())
                return false;

            List<Point> way = new List<Point>();
            way.Add(walker);
            //Стэк развилок
            Stack<Point> fork = new Stack<Point>();
            //Список посещённых точек
            List<Point> visitedPoints = new List<Point>();

            //Пока бот не достиг выхода
            while (walker != lab.Exit)
            {
                IEnumerable<Direction> avaibleDirs = GetAvaibleDirectionsToMove(walker, visitedPoints);

                //Если один доступный путь
                if (avaibleDirs.Count() == 1)
                {
                    visitedPoints.Add(walker);
                    walker.OffsetPoint(avaibleDirs.ElementAt(0));
                    way.Add(walker);
                }
                //Если нет доступных путей (т. е. тупик)
                else if (avaibleDirs.Count() == 0)
                {
                    //Если некуда больше идти - выход из метода
                    if (fork.Count == 0)
                        return false;
                    else
                    {
                        walker = fork.Pop();//Воврат до развилки

                        List<Point> deadEndWay = way.CopyList(way.IndexOf(walker));
                        if (DeadEndFintEvent != null)
                            DeadEndFintEvent(this, new DeadEndFindEventArgs(deadEndWay));

                        way.Add(walker);
                        continue;
                    }
                }
                else//Если есть много путей, куда идти
                {
                    fork.Push(walker);//Запомнить точку развилки
                    visitedPoints.Add(walker);
                    //Переместиться в рандомное направление
                    walker.OffsetPoint(lab.GetRandomDirectionFromList(avaibleDirs));
                    way.Add(walker);
                }
            }
            this.way = new Queue<Point>(way);
            return true;
        }
    }
}
