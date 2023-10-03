﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabirinthLib
{
    /// <summary>
    /// Перечесление, обозначающие направление
    /// </summary>
    public enum Direction : int//sbyte
    {
        None = 0,//Никуда
        Up = -1,//Вверх
        Down = 1, //Вниз
        Left = -2, //Влево
        Right = 2//Вправо
    }

    /// <summary>
    /// Статический класс для работы с структурой Point и перечеслением Direction
    /// </summary>
    public static class DirectionExtandentClass
    {
        /// <summary>
        /// Перемещение точки на одно значение по определнному направлению
        /// </summary>
        /// <param name="point">Перемещаемая точк</param>
        /// <param name="dirs">Напраления</param>
        public static void OffsetPoint(this ref Point point, params Direction[] dirs)
        {
            int dx = 0;
            int dy = 0;
            foreach (Direction direction in dirs)
            {
                switch (direction)
                {
                    case Direction.Up:
                    case Direction.Down:
                        dy += (int)direction;
                        break;
                    case Direction.Left:
                    case Direction.Right:
                        dx += (int)direction / 2;
                        break;
                }
            }
            point.Offset(dx, dy);
        }
    }    
}
