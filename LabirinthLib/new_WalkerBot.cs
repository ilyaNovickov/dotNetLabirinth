﻿using System;
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

        private Queue<Point> wayToExit;

        private Queue<Direction> directions;

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

                if (way != null)
                    way.Clear();
                if (wayToExit != null)
                    wayToExit.Clear();
                if (directions != null)
                    directions.Clear();
            }
        }

        public Queue<Point> WayQueue => way;

        public List<Point> WayList => way.ToList();

        public Queue<Point> WayToExitQueue => wayToExit;

        public List<Point> WayToExitList => wayToExit != null ? wayToExit.ToList() : null;

        public Queue<Direction> DirectionsQueue => directions;

        public List<Direction> DirectionsList => directions != null ? directions.ToList() : null;

        public event EventHandler<DeadEndFindEventArgs> DeadEndFintEvent;

        public event EventHandler<BotMoveEventArgs> BotMoveEvent;

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

            void ReverseDirections(ref List<Direction> directions1)
            {
                for (int i = 0; i < directions1.Count(); i++)
                {
                    directions1[i] = (Direction)(-((int)directions1[i]));
                }
            }
            

            Point walker;

            void BotOffsetPoint(Direction direction)
            {
                Point prevPoint = walker;
                walker.OffsetPoint(direction);
                if (BotMoveEvent != null)
                    BotMoveEvent(this, new BotMoveEventArgs(walker, prevPoint, direction));
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

            List<List<Point>> visitedDeadEnds = new List<List<Point>>();

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
						this.directions = new Queue<Direction>(directions);
						this.way = new Queue<Point>(way);
						return false;
                    }

					walker = fork.Pop();

					wayToExit.RemoveSinceUnique(wayToExit.IndexOf(walker) + 1);

					List<Point> deadEndWay = new List<Point>();

					List<Direction> deadEndDirection = new List<Direction>();

					int indexofFork = way.IndexOf(walker);

					deadEndWay = way.CopyList(indexofFork);
					deadEndDirection = directions.CopyList(indexofFork);

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
					}
					deadEndWay.Reverse();
					deadEndWay.RemoveAt(0);
					deadEndDirection.RemoveAt(0);
					deadEndDirection.Reverse();
					//deadEndDirection.RemoveAt(0);
					ReverseDirections(ref deadEndDirection);
					directions.AddRange(deadEndDirection);
					way.AddRange(deadEndWay);

					prevFork = walker;
					continue;
				}

			}
            this.directions = new Queue<Direction>(directions);
            this.way = new Queue<Point>(way);
            this.wayToExit = new Queue<Point>(wayToExit);
            return true;
        }
    }
}
