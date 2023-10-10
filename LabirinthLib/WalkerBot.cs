using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabirinthLib
{
    public static class WalkerBot
    {
        /// <summary>
        /// Является ли путь выходом из лабирнта?
        /// </summary>
        /// <param name="labirinth">Лабиринт</param>
        /// <param name="wayPoints">Проверяемый путь</param>
        /// <returns>Возвращает true, если путь имеет выход, иначе false</returns>
        public static bool HasExit(this Labirinth labirinth, IEnumerable<Point> wayPoints)
        {
            return (wayPoints.Contains(labirinth.FirstIn) || wayPoints.Contains(labirinth.SecondIn)) 
                && wayPoints.Contains(labirinth.Exit);
        }
        /// <summary>
        /// Получить путь из лабринта от входа до выхода
        /// </summary>
        /// <param name="labirinth">Лабиринт</param>
        /// <returns>Очередь точек, ведущий из входа на выход</returns>
        public static Queue<Point> GetWayFromLaborinth(this Labirinth labirinth)
        {
            return GetWayFromLaborinth(labirinth, 1);
        }
        /// <summary>
        /// Получить путь из лабринта от входа до выхода
        /// </summary>
        /// <param name="labirinth">Лабиринт</param>
        /// <param name="numofIn">Номер входа в лабиринт</param>
        /// <returns>Очередь точек, ведущий из входа на выход</returns>
        public static Queue<Point> GetWayFromLaborinth(this Labirinth labirinth, int numofIn)
        {
            return new Queue<Point>(WalkerBot.CreateWay(labirinth, numofIn, false));
        }
        /// <summary>
        /// Получения пути, пройденного ботом по лабиринту
        /// </summary>
        /// <param name="labirinth">Лабиринт</param>
        /// <returns>Список точек, пройденный ботом</returns>
        public static List<Point> GetAllWayFromLaborinth(this Labirinth labirinth)
        {
            return WalkerBot.GetAllWayFromLaborinth(labirinth, 1);
        }
        /// <summary>
        /// Получения пути, пройденного ботом по лабиринту
        /// </summary>
        /// <param name="labirinth">Лабиринт</param>
        /// <param name="numofIn">Номер входа в лабиринт</param>
        /// <returns>Список точек, пройденный ботом</returns>
        public static List<Point> GetAllWayFromLaborinth(this Labirinth labirinth, int numofIn)
        {
            return WalkerBot.CreateWay(labirinth, numofIn, true).ToList();
        }

        /// <summary>
        /// Метод создания пути из лабиринта
        /// </summary>
        /// <param name="labirinth">Лабиринт</param>
        /// <param name="numofIn">Номер входа</param>
        /// <param name="wayWithDeadEnds">Путь должен содержать тупики?</param>
        /// <returns>Коллекция точек пути из лабиринта</returns>
        private static IEnumerable<Point> CreateWay(Labirinth labirinth, int numofIn, bool wayWithDeadEnds = false)
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
                    if (!labirinth.IsExistInLab(extraPoint))
                        continue;
                    else if (exceptionPoints != null && exceptionPoints.Contains(extraPoint))
                        continue;
                    else if (labirinth[extraPoint] == 1 && extraPoint != labirinth.Exit)
                        continue;
                    else
                        result.Add(dir);
                }
                return result;
            }

            Point walker;//Точка, которая будет перемещаться
            //Определения начальной точки
            switch (numofIn)
            {
                case 1:
                    walker = labirinth.FirstIn;
                    break;
                case 2:
                    walker = labirinth.SecondIn;
                    break;
                default:
                    return new List<Point>(); ;
            }

            List<Point> way = new List<Point>()
            {
                walker
            };


            //Стэк развилок
            Stack<Point> fork = new Stack<Point>();
            //Список посещённых точек
            List<Point> visitedPoints = new List<Point>();             

            //Пока бот не достиг выхода
            while (walker != labirinth.Exit)
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
                        break;
                    else
                    {
                        walker = fork.Pop();//Воврат до развилки
                        visitedPoints.Add(way[way.IndexOf(walker) + 1]);
                        if (!wayWithDeadEnds)//Если тупиковые пути не нужны, то удалить тупик
                            way.RemoveSinceUnique(way.IndexOf(walker) + 1);
                        continue;
                    }
                }
                else//Если есть много путей, куда идти
                {
                    fork.Push(walker);//Запомнить точку развилки
                    visitedPoints.Add(walker);
                    //Переместиться в рандомное направление
                    walker.OffsetPoint(labirinth.GetRandomDirectionFromList(avaibleDirs));
                    way.Add(walker);
                }
            }
            return way;
        }
    }
}
