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

        public Queue<Direction> Directions => directions;

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

            List<Point> way = new List<Point>();
            way.Add(walker);
            //Стэк развилок
            Stack<Point> fork = new Stack<Point>();
            ////Список посещённых точек
            List<Point> visitedPoints = new List<Point>();
            visitedPoints.AddUnique(walker);
            List<Point> wayToExit = new List<Point>();
            wayToExit.Add(walker);
            List<Direction> directions = new List<Direction>();
            //Пока бот не достиг выхода
            while (walker != lab.Exit)
            {
                IEnumerable<Direction> avaibleDirs = GetAvaibleDirectionsToMove(walker, visitedPoints);
                //IEnumerable<Direction> avaibleDirs = GetAvaibleDirectionsToMove(walker, way);

                //Если один доступный путь
                if (avaibleDirs.Count() == 1)
                {
                    directions.Add(avaibleDirs.ElementAt(0));
                    BotOffsetPoint(avaibleDirs.ElementAt(0));
                    visitedPoints.AddUnique(walker);
                    wayToExit.Add(walker);
                    way.Add(walker);
                }
                //Если нет доступных путей (т. е. тупик)
                else if (avaibleDirs.Count() == 0)
                {
                    //Если некуда больше идти - выход из метода
                    if (fork.Count == 0)
                    {
                        this.way = new Queue<Point>(way);
                        return false;
                    }
                    else
                    {
                        visitedPoints.AddUnique(walker);

                        walker = fork.Pop();//Воврат до развилки

                        visitedPoints.AddUnique(walker);

                        wayToExit.RemoveSinceUnique(wayToExit.IndexOf(walker) + 1);
                        List<Point> deadEndWay = way.CopyList(way.IndexOf(walker));
                        deadEndWay.Reverse();
                        way.AddRange(deadEndWay);
                        way.Add(walker);
                        continue;
                    }
                }
                else//Если есть много путей, куда идти
                {
                    fork.Push(walker);//Запомнить точку развилки
                    
                    //Переместиться в рандомное направление
                    Direction dir = lab.GetRandomDirectionFromList(avaibleDirs);
                    directions.Add(dir);
                    BotOffsetPoint(dir);
                    visitedPoints.AddUnique(walker);
                    wayToExit.Add(walker);
                    way.Add(walker);
                }
            }
            way.Add(walker);
            wayToExit.Add(walker);
            this.directions = new Queue<Direction>(directions);
            this.way = new Queue<Point>(way);
            this.wayToExit = new Queue<Point>(wayToExit);
            return true;
        }
    }
}
