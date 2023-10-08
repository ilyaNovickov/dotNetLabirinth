using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LabirinthLib
{
    /// <summary>
    /// Класс лабиринта
    /// </summary>
    public class Labirinth
    {
        /*
         * 0 - empty
         * 1 - wall
         */
        #region Vars
        Random random = new Random();//Экземпляр класса рандомайзера
        float percentofEmptySpace = 0.4f;//Кол-во пустого пространства в процентах
        Size size;//Размер лабиринта
        //Координаты входов №1 и №2 и выхода
        Point firstIn = new Point(0, 0);
        Point secIn = new Point(0, 0);
        Point exit = new Point(0, 0);
        //Списки путей №1 и №2
        List<Point> firstWay = new List<Point>(1);
        List<Point> secondWay = new List<Point>(1);
        #endregion
        #region Constr
        /// <summary>
        /// Инициализирует лабиринт
        /// </summary>
        public Labirinth() : this(new Size(10, 10))
        {

        }
        /// <summary>
        /// Инициализирует прямоугольный лабиринт
        /// </summary>
        public Labirinth(int size) : this(new Size(size, size))
        {

        }
        /// <summary>
        /// Инициализирует лабиринт
        /// </summary>
        /// <param name="width">Ширина лабиринта</param>
        /// <param name="height">Высота лабиринта</param>
        public Labirinth(int width, int height) : this(new Size(width, height))
        {

        }
        /// <summary>
        /// Инициализирует лабиринт
        /// </summary>
        /// <param name="size">Размер лабиринта</param>
        public Labirinth(Size size)
        {
            this.Size = size;
        }
        #endregion
        #region Props
        /// <summary>
        /// Получение значения ячейки лабиринта
        /// </summary>
        /// <param name="point">Координаты ячейки лабиринта</param>
        /// <returns>Число 1 - для стен; число 0 - для пустого пространства</returns>
        /// <exception cref="Exception">Координаты нет в лабиринте</exception>
        public int this[Point point]
        {
            get
            {
                if (!IsExistInLab(point))
                    throw new Exception("Координаты не существует в лабиринте");
                return firstWay.Contains(point) || secondWay.Contains(point) || point == FirstIn 
                    || point == SecondIn || point == Exit  ? 0 : 1;
            }
        }
        /// <summary>
        /// Получение значения ячейки лабиринта
        /// </summary>
        /// <param name="x">Значение X</param>
        /// <param name="y">Значение Y</param>
        /// <returns></returns>
        public int this[int x, int y]
        {
            get
            {
                return this[new Point(x, y)];
            }
        }
        /// <summary>
        /// Минимальное кол-во пустово пространства в лабиринте в процентах
        /// </summary>
        public float MinEmptySpace
        {
            get => (1 / (new Size(Width - 2, Height - 2).Square));
        }
        /// <summary>
        /// Кол-во пустово пространства в лабиринте в процентах
        /// </summary>
        public float EmptySpace
        {
            get => percentofEmptySpace;
            set
            {
                if (MinEmptySpace + 0.01f <= value && value <= 1f)
                {
                    percentofEmptySpace = value;
                }
                else
                {
                    percentofEmptySpace = 0.4f;
                }
                FillLabirinth();
            }
        }
        /// <summary>
        /// Размер лабиртна
        /// </summary>
        public Size Size
        {
            get => size;
            set
            {
                //Минимальный размер лабирнта 5x5 с учётом стен
                if (value.Width < 4 && value.Height < 4)
                    return;
                size = value;
                FillLabirinth();
            }
        }
        /// <summary>
        /// Ширина лабиринта
        /// </summary>
        public int Width => size.Width;
        /// <summary>
        /// Высота лабиринта
        /// </summary>
        public int Height => size.Height;
        /// <summary>
        /// Первый вход в лабиринт
        /// </summary>
        public Point FirstIn
        {
            //Если у лабиринта нет входа, то вовращает точку (-1, -1)
            get => !firstIn.IsZero() ? firstIn : new Point(-1, -1);
            set
            {
                if (firstWay.Contains(value))
                    firstIn = value;
                else
                    return;
            }
        }
        /// <summary>
        /// Второй вход в лабиринт
        /// </summary>
        public Point SecondIn
        {
            //Если у лабиринта нет входа, то вовращает точку (-1, -1)
            get => !secIn.IsZero() ? secIn : new Point(-1, -1);
            set
            {
                if (secondWay.Contains(value))
                    secIn = value;
                else
                    return;
            }
        }
        /// <summary>
        /// Выход из лабиринта
        /// </summary>
        public Point Exit
        {
            //Если у лабиринта нет выхода, то вовращает точку (-1, -1)
            get => !exit.IsZero() ? exit : new Point(-1, -1);
            set
            {
                //Выход всегда относится к первому пути
                if (firstWay.Contains(value))
                {
                    firstIn = value;
                }
                else if (secondWay.Contains(value))
                {
                    //Перемтановка списков
                    List<Point> extra = secondWay;
                    secondWay = firstWay;
                    firstWay = secondWay;
                    //Перемтановка входов
                    Point extraPoint = secIn;
                    secIn = firstIn;
                    firstIn = extraPoint;
                }
                else
                    return;
            }
        }
        /// <summary>
        /// Кол-во пустых ячеек в лабиринте
        /// </summary>
        public int CountofEmptyCells
        {
            get
            {
                int count = 0;
                for (int x = 1; x < Width; x++)
                {
                    for (int y = 1; y < Height; y++)
                    {
                        count += this[x, y] == 1 ? 0 : 1;
                    }
                }
                return count;
            }
        }
        /// <summary>
        /// Кол-во слоёв в лабиринте
        /// </summary>
        public int CountofLayouts
        {
            get
            {
                return Math.Min(Size.Width - 1, Size.Height - 1);
            }
        }
        /// <summary>
        /// Копия первого путя в списке
        /// </summary>
        public List<Point> FirstWay
        {
            get
            {
                List<Point> newList = new List<Point>();
                newList.AddRange(firstWay);
                return newList;
            }
        }
        /// <summary>
        /// Копия второго путя в списке
        /// </summary>
        public List<Point> SecondWay
        {
            get
            {
                List<Point> newList = new List<Point>();
                newList.AddRange(secondWay);
                return newList;
            }
        }
        #endregion
        #region Methods
        #region ExtraMethods
        /// <summary>
        /// Заполнения лабиринта пустым пространством
        /// </summary>
        private void FillLabirinth()
        {
            //Обнуление входов/выходов
            exit = Point.Empty;
            firstIn = Point.Empty;
            secIn = Point.Empty;
            //Очистка списков путей
            firstWay.Clear();
            secondWay.Clear();
        }
        /// <summary>
        /// Метод получения случайно точки в определённом слое
        /// </summary>
        /// <param name="numofLayout">Номер слоя</param>
        /// <returns>Координаты ячейки в слое</returns>
        /// <exception cref="Exception">В лабиринте нет столько слоёв</exception>
        private Point GetRandomLayoutPoint(int numofLayout = 0)
        {

            if (numofLayout < 0 || numofLayout >= CountofLayouts)
                throw new Exception("В прямоугольнике нет столько слоёв");

            int offset = numofLayout + 1;
            int x = random.Next(numofLayout, Size.Width - offset);
            int y = random.Next(numofLayout, Size.Height - offset);

            byte doIntZero = (byte)random.Next(0, 3 + 1);

            if (doIntZero == 0)
                x = numofLayout;
            else if (doIntZero == 1)
                y = numofLayout;
            else if (doIntZero == 2)
                x = Size.Width - offset;
            else
                y = Size.Height - offset;

            return new Point(x, y);
        }
        /// <summary>
        /// Получение рандомной точки в списке точек
        /// </summary>
        /// <param name="list">Спикок точек</param>
        /// <returns>Случайная точка</returns>
        internal Point GetRandomPointFromList(IEnumerable<Point> list)
        {
            if (list.Count() == 0)
                return Point.Empty;
            return list.ElementAt(random.Next(0, list.Count()));
        }
        /// <summary>
        /// Получение рандомноого направления в списке направлений
        /// </summary>
        /// <param name="avaibleDirs">Список доступных путей</param>
        /// <returns>Случайный путь</returns>
        internal Direction GetRandomDirectionFromList(IEnumerable<Direction> avaibleDirs)
        {
            if (avaibleDirs.Count() == 0)
                return Direction.None;
            return avaibleDirs.ElementAt(random.Next(0, avaibleDirs.Count<Direction>()));
        }
        /// <summary>
        /// Проверка на то, является ли ячейка - границей лабиринта
        /// </summary>
        /// <param name="point">Координаты ячейки</param>
        /// <returns>Возвращает true если ячейка - граница лабиринта, иначе false</returns>
        internal bool IsBorder(Point point)
        {
            return (point.X == 0 || point.Y == 0 || point.X == Size.Width - 1 || point.Y == Size.Height - 1);
        }
        /// <summary>
        /// Получение пустых ячеек лабиринта в определённом слое
        /// </summary>
        /// <param name="numofLayout">Номер слоя</param>
        /// <returns>Список пустых точек</returns>
        /// <exception cref="Exception">В лабиринте нет столько слоёв</exception>
        private IEnumerable<Point> GetEmptyCellsInLayout(int numofLayout)
        {
            if (numofLayout < 0 || numofLayout >= CountofLayouts)
                throw new Exception("В прямоугольнике нет столько слоёв");

            for (int x = numofLayout; x < Width - numofLayout; x++)
            {
                for (int y = numofLayout; y < Height - numofLayout; y++)
                {
                    if (x != numofLayout && x != Width - numofLayout - 1 && y != numofLayout && y != Height - numofLayout - 1)
                        continue;
                    else if (this[x, y] == 0)
                        yield return new Point(x, y);
                }
            }
        }
        /// <summary>
        /// Существует ли точка в лабиринте
        /// </summary>
        /// <param name="point">Проверяемые координаты</param>
        /// <returns>Возвращает true если ячейка существует в лабиринте, иначе false</returns>
        internal bool IsExistInLab(Point point)
        {
            return (0 <= point.X && 0 <= point.Y && point.X < Size.Width && point.Y < Size.Height);
        }
        #endregion
        #region Generation
        /// <summary>
        /// Асинхронная генерация лабиринта
        /// </summary>
        public async void GenerateLabirinthAsync()
        {
            await Task.Run(() => this.GenerateLabirinth());
        }
        /// <summary>
        /// Генерация лабиринта
        /// </summary>
        public void GenerateLabirinth()
        {
            FillLabirinth();//Заполнение его
            GenerationLabirinth();//Вызов генерации
        }
        /// <summary>
        /// Генерация лабиринта
        /// </summary>
        /// <param name="newSize">Новый размер</param>
        public void GenerateLabirinth(Size newSize)
        {
            Size = newSize;
            GenerationLabirinth();
        }
        /// <summary>
        /// Генерация лабиринта
        /// </summary>
        /// <param name="width">Новыя ширина</param>
        /// <param name="height">Новыя высота</param>
        public void GenerateLabirinth(int width, int height)
        {
            GenerateLabirinth(new Size(width, height));
        }
        /// <summary>
        /// Генерация лабиринта
        /// </summary>
        /// <param name="size">Новый размер прямоугольного лабиринта</param>
        public void GenerateLabirinth(int size)
        {
            GenerateLabirinth(new Size(size));
        }
        /// <summary>
        /// Генерация лабирнта
        /// </summary>
        private void GenerationLabirinth()
        {
            //Получение доступных путей для перемещения точки в стену (Список points - свободное пространство)
            IEnumerable<Direction> GetAvaibleDirections(Point checkingPoint, IEnumerable<Point> points)
            {
                //Имеет ли проверяема точка соседей по диагонали в определённом направлении
                bool HasDiagonalNeighboor(Point point, Direction dir)
                {
                    Direction newDir = dir;
                    Point newPoint1 = point;
                    Point newPoint2 = point;

                    newPoint1.OffsetPoint(dir);
                    newPoint2.OffsetPoint(dir);

                    newPoint1.OffsetPoint((Direction)((int)dir == 1 || (int)dir == -1 ? 2 : 1));
                    newPoint2.OffsetPoint((Direction)((int)dir == 1 || (int)dir == -1 ? -2 : -1));

                    return (points.Contains(newPoint1) || points.Contains(newPoint2));
                }
                //Имеет ли точка посещённых соседей (др свободные точки, кроме последней (lastPoint))
                bool HasVisitedNeighboors(Point point, Point lastPoint)
                {
                    foreach (Direction dir in new Direction[] { Direction.Left, Direction.Right, Direction.Up, Direction.Down })
                    {
                        Point newPoint = point;
                        newPoint.OffsetPoint(dir);
                        if (newPoint == lastPoint)
                            continue;
                        if (points.Contains(newPoint))
                            return true;
                    }

                    return false;
                }

                List<Direction> result = new List<Direction>();

                foreach (Direction dir in new Direction[] { Direction.Left, Direction.Right, Direction.Up, Direction.Down})
                {
                    Point extraPoint = checkingPoint;//points.Last();
                    extraPoint.OffsetPoint(dir);
                    if (IsBorder(extraPoint))
                        continue;
                    else if (!IsExistInLab(extraPoint))
                        continue;
                    else if (points.Contains(extraPoint))
                        continue;
                    else if (HasVisitedNeighboors(extraPoint, checkingPoint) )
                        continue;
                    else if (HasDiagonalNeighboor(extraPoint, dir))
                        continue;
                    else
                        result.Add(dir);
                }

                return result;
            }
            //Получение точек, доступных для дальнейшей генерации лабирнта (если некуда больше идти)
            //Принимает спикок проверяемых точек
            IEnumerable<Point> GetAvaiblePointsToMove(IEnumerable<Point> checkingPoints)
            {
                foreach (Point point in checkingPoints)
                {
                    if (GetAvaibleDirections(point, checkingPoints).Count() != 0)
                        yield return point;
                }
            }  
            //Получение списка стен относительно списка пустого пространства
            IEnumerable<Point> GetWallsCells(IEnumerable<Point> emptySpace)
            {
                for (int x = 1; x < Width - 1; x++)
                {
                    for (int y = 1; y < Height - 1; y++)
                    {
                        if (!emptySpace.Contains(new Point(x, y)))
                            yield return new Point(x, y);
                    }
                }
            }

            int countofEmptySpace = ((int)(percentofEmptySpace * new Size(size.Width - 2, size.Height - 2).Square));

            List<Point> firstWay = new List<Point>(1);

            List<Point> secondWay = new List<Point>();

            List<Point> workingList = firstWay;//Ссылка на список, с которым мы сейчас работаем

            Point movingPoint = GetRandomLayoutPoint(1);//Передвигаемая точка

            workingList.AddUnique(movingPoint);

            /*
             * Для генерации лабиринта без ограничений 
             * (но предёться избавиться от списков firstWay и secondWay)
             * List<Point> visitedList = new List<Point>();
             * while (visitedPoints.Count != countofEmptySpace)
             */
            //Пока не достигнуто требуемое значение пустого пространства
            while (firstWay.Count + secondWay.Count != countofEmptySpace)//(visitedPoints.Count != countofEmptySpace)
            {
                Point oldPoint = movingPoint;

                if (movingPoint.IsZero())
                {
                    if (workingList.Count != countofEmptySpace)
                    {
                        IEnumerable<Point> walls = GetWallsCells(ListUnique.UniteUnique(firstWay, secondWay));

                        if (walls.Count() == 0)
                            break;
                        else
                        {
                            Point newPoint = GetRandomPointFromList(walls);
                            if (firstWay.Contains(newPoint))
                                workingList = firstWay;
                            else if (secondWay.Contains(newPoint))
                                workingList = secondWay;
                            workingList.AddUnique(newPoint);
                            continue;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                if (secondWay.Count == 0 && workingList.Count < countofEmptySpace / 2 && workingList.Count > countofEmptySpace  / 3 && countofEmptySpace >= 4)
                {
                    int doSecWay = random.Next(0, 101);
                    if (doSecWay > 75)
                    {

                        do
                        {
                            movingPoint = GetRandomLayoutPoint(1);
                        } 
                        while (firstWay.Contains(movingPoint) || GetAvaiblePointsToMove(firstWay).Contains(movingPoint));
                        workingList = secondWay;
                        workingList.AddUnique(movingPoint);
                        continue;
                    }
                }

                Direction dir = GetRandomDirectionFromList(GetAvaibleDirections(movingPoint, ListUnique.UniteUnique(firstWay, secondWay)));  

                movingPoint.OffsetPoint(dir);

                if (IsBorder(movingPoint) || ListUnique.UniteUnique(firstWay, secondWay).Contains(movingPoint))
                {
                    if (dir == Direction.None)
                    {
                        do
                        {
                            movingPoint = GetRandomPointFromList(GetAvaiblePointsToMove(ListUnique.UniteUnique(firstWay, secondWay)));
                            if (movingPoint.IsZero())
                                break;
                        }
                        while (movingPoint.IsZero());
                        if (firstWay.Contains(movingPoint))
                            workingList = firstWay;
                        else if (secondWay.Contains(movingPoint))
                            workingList = secondWay;
                        
                        continue;
                    }
                    movingPoint = oldPoint;
                    continue;
                }

                workingList.AddUnique(movingPoint);
            }

            //Добавление точек пустого пространства в списки
            this.firstWay.AddRange(firstWay);
            this.secondWay.AddRange(secondWay);
            //Генерация входов и выходов
            GenerateInsAndExit();
        }
        /// <summary>
        /// Перегенирация входов и выходов
        /// </summary>
        public void RegenerateInsAndExit()
        {
            GenerateInsAndExit();
        }
        /// <summary>
        /// Генерация входов и выходов (около границы)
        /// </summary>
        private void GenerateInsAndExit()
        {
            List<Point> preborderPoints = GetEmptyCellsInLayout(1).ToList();

            Point GetNearBorderPoint(Point checkingPoint)
            {
                if (!preborderPoints.Contains(checkingPoint) || checkingPoint.IsZero())
                    return Point.Empty;
                else
                {
                    Point[] borderPoints = new Point[2];
                    int currentIndex = 0;
                    foreach (Direction direction in new Direction[4] { Direction.Down, Direction.Left, Direction.Right, Direction.Up })
                    {
                        Point extraPoint = checkingPoint;
                        extraPoint.OffsetPoint(direction);
                        if (this.IsBorder(extraPoint))
                        {
                            borderPoints[currentIndex] = extraPoint;
                            currentIndex++;
                        }
                    }

                    if (currentIndex == 1)
                        return borderPoints[0];
                    else if (currentIndex == 2)
                    {
                        return GetRandomPointFromList(borderPoints);
                    }
                    else
                        return Point.Empty;
                }
            }

            do
            {
                Point exitPointOne = GetRandomPointFromList(preborderPoints);

                if (firstWay.Contains(exitPointOne))
                {
                    if (firstIn.IsZero())
                    {
                        firstIn = exitPointOne;
                    }
                    else if (exit.IsZero())
                    {
                        exit = exitPointOne;
                    }
                }
                else if (secondWay.Contains(exitPointOne) && secIn.IsZero())
                {
                    secIn = exitPointOne;
                }
                if (firstIn == exit && preborderPoints.Intersect(firstWay).Count() != 1 && !firstIn.IsZero())
                {
                    exit = Point.Empty;
                    continue;
                }
            }
            while (firstIn.IsZero() || (secIn.IsZero() && secondWay.Count != 0) || exit.IsZero());

            firstIn = GetNearBorderPoint(firstIn);
            if (secondWay.Count != 0)
                secIn = GetNearBorderPoint(secIn);
            exit = GetNearBorderPoint(exit);
        }
        #endregion
        #region GetWays
        /// <summary>
        /// Получение путя из точки начала в точку выхода (exit)
        /// </summary>
        /// <param name="starstPoint"></param>
        /// <returns></returns>
        private List<Point> GetWay(Point starstPoint)
        {
            IEnumerable<Direction> GetAvaibleDirectionsToMove(Point checkingPoint, IEnumerable<Point> exceptionPoints = null)
            {
                bool IsExistInLab(Point point)
                {
                    return (0 <= point.X && 0 <= point.Y && point.X < Size.Width && point.Y < Size.Height);
                }

                List<Direction> result = new List<Direction>();

                foreach (Direction dir in new Direction[] { Direction.Left, Direction.Right, Direction.Up, Direction.Down })
                {
                    Point extraPoint = checkingPoint;
                    extraPoint.OffsetPoint(dir);
                    if (IsBorder(extraPoint) && extraPoint != exit)
                        continue;
                    else if (!IsExistInLab(extraPoint))
                        continue;
                    else if (exceptionPoints != null && exceptionPoints.Contains(extraPoint))
                        continue;
                    else if (this[extraPoint] == 1)
                        continue;
                    else
                        result.Add(dir);
                }
                return result;
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
                if (walker == exit)
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
                        way.RemoveSinceUnique(way.IndexOf(walker) + 1);
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
        /// <summary>
        /// Получение путя выхода из списка
        /// </summary>
        /// <returns>Список ячеек для выхода из лабиринта</returns>
        public List<Point> GetWay()
        {
            if (firstIn.IsZero())
                return new List<Point>();
            return GetWay(firstIn);
        }
        /// <summary>
        /// Получение путя выхода из списка для выхода №1
        /// </summary>
        /// <returns>Список ячеек для выхода из лабиринта</returns>
        public List<Point> GetFirstWay()
        {
            return GetWay();
        }
        /// <summary>
        /// Получение путя выхода из списка для выхода №2
        /// </summary>
        /// <returns>Список ячеек для выхода из лабиринта</returns>
        public List<Point> GetSecondWay()
        {
            //Проверка на то, соеденены ли пути №1 и №2
            bool IsSecAndFirstWaysConnected()
            {
                //Если нет второго путя или входа №2, то вернуть false
                if (secondWay.Count == 0 || secIn.IsZero())
                    return false;
                foreach (Point point in secondWay)
                {
                    foreach (Direction direction in new Direction[4] { Direction.Up, Direction.Down, Direction.Left, Direction.Right })
                    {
                        Point newPoint = point;
                        newPoint.OffsetPoint(direction);
                        if (firstWay.Contains(newPoint))
                            return true;
                    }
                }
                return false;
            }
            //если пути №1 и №2 соеденены, то вернуть путь из входа №2 до выхода
            if (IsSecAndFirstWaysConnected())
                return GetWay(secIn);
            //Иначе вернуть пустой список
            return new List<Point>();
        }
        #endregion
        #endregion
    }
}
