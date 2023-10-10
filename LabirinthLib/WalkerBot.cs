using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabirinthLib
{
    public static class WalkerBot
    {
        public static bool HasExit(Labirinth labirinth, IEnumerable<Point> wayPoints)
        {
            return (wayPoints.Contains(labirinth.FirstIn) || wayPoints.Contains(labirinth.SecondIn)) 
                && wayPoints.Contains(labirinth.Exit);
        }

        public static Queue<Point> GetWayFromLaborinth(Labirinth labirinth)
        {
            return GetWayFromLaborinth(labirinth, 1);
        }

        public static Queue<Point> GetWayFromLaborinth(Labirinth labirinth, int numofIn)
        {
            return new Queue<Point>(WalkerBot.CreateWay(labirinth, numofIn, false));
        }

        public static List<Point> GetAllWayFromLaborinth(Labirinth labirinth)
        {
            return WalkerBot.GetAllWayFromLaborinth(labirinth, 1);
        }

        public static List<Point> GetAllWayFromLaborinth(Labirinth labirinth, int numofIn)
        {
            return WalkerBot.CreateWay(labirinth, numofIn, true).ToList();
        }

        private static IEnumerable<Point> CreateWay(Labirinth labirinth, int numofIn, bool wayWithDeadEnds = false)
        {
            Point walker;

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

            IEnumerable<Direction> GetAvaibleDirectionsToMove(Point checkingPoint, IEnumerable<Point> exceptionPoints = null)
            {

                List<Direction> result = new List<Direction>();

                foreach (Direction dir in new Direction[] { Direction.Left, Direction.Right, Direction.Up, Direction.Down })
                {
                    Point extraPoint = checkingPoint;
                    extraPoint.OffsetPoint(dir);
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

            //Стэк развилок
            Stack<Point> fork = new Stack<Point>();
            //Список посещённых точек
            List<Point> visitedPoints = new List<Point>();             

            while (walker != labirinth.Exit)
            {

                IEnumerable<Direction> avaibleDirs = GetAvaibleDirectionsToMove(walker, visitedPoints);

                if (avaibleDirs.Count() == 1)
                {
                    visitedPoints.Add(walker);
                    walker.OffsetPoint(avaibleDirs.ElementAt(0));
                    way.Add(walker);
                }
                else if (avaibleDirs.Count() == 0)
                {
                    //Если некуда больше идти - выход из метода
                    if (fork.Count == 0)
                        break;
                    else
                    {
                        walker = fork.Pop();
                        visitedPoints.Add(way[way.IndexOf(walker) + 1]);
                        if (!wayWithDeadEnds)
                            way.RemoveSinceUnique(way.IndexOf(walker) + 1);
                        continue;
                    }
                }
                else
                {
                    fork.Push(walker);
                    visitedPoints.Add(walker);
                    walker.OffsetPoint(labirinth.GetRandomDirectionFromList(avaibleDirs));
                    way.Add(walker);
                }
            }
            return way;
        }
    }
}
