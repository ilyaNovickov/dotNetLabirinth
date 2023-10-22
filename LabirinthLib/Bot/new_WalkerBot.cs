using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabirinthLib.Structs;

namespace LabirinthLib.Bot
{
    /// <summary>
    /// Класс бота, который ищет выход из лабиринта
    /// </summary>
    public class new_WalkerBot
    {
        private Labirinth lab;

        private List<Point> way;

        private List<Point> wayToExit;

        private List<Direction> directions;

        private List<Point> deadEnds;

        /// <summary>
        /// Инцилизирует экземпляр бота
        /// </summary>
        /// <param name="lab">Лабиринт, где будет находиться бот</param>
        public new_WalkerBot(Labirinth lab)
        {
            this.lab = lab;
        }

        /// <summary>
        /// Лабиринт с ботом
        /// </summary>
        public Labirinth Labirinth
        {
            get => lab;
            set
            {
                lab = value;

                way?.Clear();
                wayToExit?.Clear();
                directions?.Clear();
            }
        }

        /// <summary>
        /// Очередь всего пройденного ботом пути
        /// </summary>
        public Queue<Point> WayQueue => new Queue<Point>(way);

        /// <summary>
        /// Список всего пройденнго ботом пути
        /// </summary>
        public List<Point> WayList => new List<Point>(way);

        /// <summary>
        /// Очередь выхода из лабиринта
        /// </summary>
        public Queue<Point> WayToExitQueue => wayToExit != null ? new Queue<Point>(wayToExit) : null;

        /// <summary>
        /// Список выхода из лабиринта
        /// </summary>
        public List<Point> WayToExitList => wayToExit != null ? new List<Point>(wayToExit) : null;

        /// <summary>
        /// Очередь направленний, по кторым шёл бот
        /// </summary>
        public Queue<Direction> DirectionsQueue => new Queue<Direction>(directions);

        /// <summary>
        /// Список направленний, по кторым шёл бот
        /// </summary>
        public List<Direction> DirectionsList => directions != null ? new List<Direction>(directions) : null;

        /// <summary>
        /// Список точек-тупиков, в которые шёл бот 
        /// </summary>
        public List<Point> DeadEndsList => deadEnds;



        /// <summary>
        /// Находит выход из лабиринта по указанному входу
        /// </summary>
        /// <param name="numofEnter">Номер входа в лабиринт</param>
        /// <returns>Возвращает true если выход из лабиринта был найден, иначе false</returns>
        /// <exception cref="Exception">Нет таково входа в лабиринт</exception>
        public bool FindExit(int numofEnter = 1)
        {
            //Метод получения доступных направлений для перемещения точек, исключая указанные
            IEnumerable<Direction> GetAvaibleDirectionsToMove(Point checkingPoint, IEnumerable<Point> exceptionPoints)
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
                    else if (exceptionPoints.Contains(extraPoint))
                        continue;
                    else if (lab[extraPoint] == 1 && extraPoint != lab.Exit)
                        continue;
                    else if (extraPoint == lab.Exit)
                        return new List<Direction>() { dir };
                    else
                        result.Add(dir);
                }
                return result;
            }
            //Метод инверсирует направление из списка
            void ReverseDirections(ref List<Direction> directions1)
            {
                for (int i = 0; i < directions1.Count(); i++)
                {
                    directions1[i] = (Direction)(-((int)directions1[i]));
                }
            }
            

            Point walker;

            //Метод перемещения бота по лабиринту
            void BotOffsetPoint(Direction direction)
            {
                //Point prevPoint = walker;
                walker.OffsetPoint(direction);
                //if (BotMoveEvent != null)
                //    BotMoveEvent(this, new BotMoveEventArgs(walker, prevPoint, direction));
            }

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

            List<Point> way = new List<Point>
            {
                walker
            };
            //Стэк развилок
            Stack<Point> fork = new Stack<Point>();
            ////Список посещённых точек
            //List<Point> visitedPoints = new List<Point>()
            //{
            //    walker
            //};
            List<Point> wayToExit = new List<Point>
            {
                walker
            };
            List<Direction> directions = new List<Direction>
            {
                Direction.None
            };

            Point prevFork = new Point();

			//List<List<Point>> visitedDeadEnds = new List<List<Point>>();
			List<Point> visitedDeadEnds = new List<Point>();

			//Пока бот не достиг выхода
			while (walker != lab.Exit)
            {
				//IEnumerable<Direction> avaibleDirs = GetAvaibleDirectionsToMove(walker, visitedPoints);
                IEnumerable<Direction> avaibleDirs = GetAvaibleDirectionsToMove(walker, way);

				if (avaibleDirs.Count() == 1)
                {
                    directions.Add(avaibleDirs.First());
                    BotOffsetPoint(avaibleDirs.First());
                    //visitedPoints.AddUnique(walker);
                    way.Add(walker);
                    wayToExit.Add(walker);
                }
                else if (avaibleDirs.Count() > 1)
                {
                    fork.Push(walker);

                    Direction dir = lab.GetRandomDirectionFromList(avaibleDirs);

                    directions.Add(dir);
                    BotOffsetPoint(dir);
                    //visitedPoints.AddUnique(walker);
                    way.Add(walker);
                    wayToExit.Add(walker);
                }
				else
				{
                    if (fork.Count == 0)
                    {
						this.directions = directions;
						this.way = way;
                        this.deadEnds = visitedDeadEnds;
						return false;
                    }

					walker = fork.Pop();

                    //var test = wayToExit.CutList(wayToExit.IndexOf(walker) + 1);
					wayToExit.RemoveSinceUnique(wayToExit.IndexOf(walker) + 1);

					int indexofFork = way.IndexOf(walker);

                    List<Point> deadEndWay = way.CopyList(indexofFork);
                    List<Direction> deadEndDirection = directions.CopyList(indexofFork);

					if (!prevFork.IsZero() && deadEndWay.Contains(prevFork))
					{
						int indexofFirstPreviousFork = way.IndexOf(prevFork);
						way.Reverse();
						int indexofLastPreviousFork = way.Count - 1 - way.IndexOf(prevFork);
						way.Reverse();
                        //deadEndWay.RemoveSinceUnique(indexofFirstPreviousFork - indexofFork + 1, indexofLastPreviousFork - indexofFirstPreviousFork);
                        //deadEndDirection.RemoveSinceUnique(indexofFirstPreviousFork - indexofFork + 1, indexofLastPreviousFork - indexofFirstPreviousFork);
                        deadEndWay.CutListByCount(indexofFirstPreviousFork - indexofFork + 1, indexofLastPreviousFork - indexofFirstPreviousFork);
						deadEndDirection.CutListByCount(indexofFirstPreviousFork - indexofFork + 1, indexofLastPreviousFork - indexofFirstPreviousFork);

                        deadEndWay.Reverse();
						deadEndDirection.Reverse();
						//foreach (Point p in visitedDeadEnds)
      //                      deadEndWay.Remove(p);
                        for (int i = 0; i < visitedDeadEnds.Count; i++)
                        {
                            int index = deadEndWay.IndexOf(visitedDeadEnds[i]);

                            if (deadEndWay.Remove(visitedDeadEnds[i]))
                            {
                                i--;
								deadEndDirection.RemoveAt(index);
                            }
						}
						deadEndDirection.Reverse();
						deadEndWay.Reverse();
					}
					deadEndWay.Reverse();
					   visitedDeadEnds.Add(deadEndWay.ElementAt(0));
					deadEndWay.RemoveAt(0);
					deadEndDirection.RemoveAt(0);
					deadEndDirection.Reverse();
					//deadEndDirection.RemoveAt(0);
					ReverseDirections(ref deadEndDirection);
					directions.AddRange(deadEndDirection);
					way.AddRange(deadEndWay);

					prevFork = walker;

                    deadEndWay.Reverse();
					deadEndWay.RemoveAt(0);

					visitedDeadEnds.AddUniqueRange(deadEndWay);

					continue;
				}

			}
            this.wayToExit = wayToExit;
            this.directions = directions;
            this.way = way;
            this.deadEnds = visitedDeadEnds;
            return true;
        }
    }
}
