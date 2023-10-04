using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabirinthLib
{
    public static class WalkerBot
    {
        public static List<Point> GetWayFromLaborinth(Labirinth labirinth, int numofIn)
        {
            Point starstPoint;
            switch (numofIn)
            {
                case 1:
                    starstPoint = labirinth.FirstIn;
                    break;
                case 2:
                    starstPoint = labirinth.SecondIn;
                    break;
                default:
                    return new List<Point>(); ;
            }

            bool IsBorder(Point point)
            {
                return (point.X == 0 || point.Y == 0 || point.X == labirinth.Width - 1 || point.Y == labirinth.Height - 1);
            }
            IEnumerable<Direction> GetAvaibleDirectionsToMove(Point checkingPoint, IEnumerable<Point> exceptionPoints = null)
            {
                bool IsExistInLab(Point point)
                {
                    return (0 <= point.X && 0 <= point.Y && point.X < labirinth.Width && point.Y < labirinth.Height);
                }

                List<Direction> result = new List<Direction>();

                foreach (Direction dir in new Direction[] { Direction.Left, Direction.Right, Direction.Up, Direction.Down })
                {
                    Point extraPoint = checkingPoint;
                    extraPoint.OffsetPoint(dir);
                    if (IsBorder(extraPoint) && extraPoint != labirinth.Exit)
                        continue;
                    else if (!IsExistInLab(extraPoint))
                        continue;
                    else if (exceptionPoints != null && exceptionPoints.Contains(extraPoint))
                        continue;
                    else if (labirinth[extraPoint] == 1)
                        continue;
                    else
                        result.Add(dir);
                }
                return result;
            }
            Direction GetRandomDirectionFromList(IEnumerable<Direction> avaibleDirs)
            {
                Random random = new Random();
                if (avaibleDirs.Count() == 0)
                    return Direction.None;
                return avaibleDirs.ElementAt(random.Next(0, avaibleDirs.Count<Direction>()));
            }

            //Список путя выхода из лабиринта
            List<Point> way = new List<Point>()
            {
                starstPoint
            };
            //Стэк развилок
            Stack<Point> fork = new Stack<Point>();
            //Список посещённых точек
            List<Point> visitedPoints = new List<Point>();
            //Передвигаемая точка
            Point walker = starstPoint;

            while (true)
            {
                if (walker == labirinth.Exit)
                {
                    break;
                }

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
                        //way.RemoveSinceUnique(way.IndexOf(walker) + 1);
                        continue;
                    }
                }
                else
                {
                    fork.Push(walker);
                    visitedPoints.Add(walker);
                    walker.OffsetPoint(GetRandomDirectionFromList(avaibleDirs));
                    way.Add(walker);
                }
            }
            return way;
        }
    }
}
