using LabirinthLib.Structs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LabirinthLib
{
    /// <summary>
    /// Статический класс для методов расширения для обобщённых списков
    /// </summary>
    public static class ListUnique
    {
        /// <summary>
        /// Добавление уникального значения в список
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">Рабочий список</param>
        /// <param name="value">Добавляемое значение</param>
        /// <returns>Возвращает true, если значение было успешно добавлено в список, иначе false</returns>
        public static bool AddUnique<T>(this List<T> list, T value)
        {
            if (list.Contains(value))
                return false;
            list.Add(value);
            return true;
        }
        /// <summary>
        /// Объеденение двух списков в один с уникальными значенями
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="originList">Список №1</param>
        /// <param name="list">Список №2</param>
        /// <returns>Список с уникальными значениями</returns>
        public static List<T> UniteUnique<T>(this List<T> originList, List<T> list)
        {
            List<T> result = new List<T>();
            result.AddRange(originList);
            foreach (T valueT in list)
            {
                if (originList.Contains(valueT))
                    continue;
                else
                    result.Add(valueT);
            }
            return result;
        }
        /// <summary>
        /// Удаление элементов, начиная с индекса и до конца списка
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">Список</param>
        /// <param name="index">Индекс, с которого начинается удаление</param>
        public static void RemoveSinceUnique<T>(this List<T> list, int index)
        {
            int countToDelete = list.Count - index;
            list.RemoveRange(index, countToDelete);
        }
        /// <summary>
        /// Удаление n-ого кол-во элементов, начиная с индекса
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">Список</param>
        /// <param name="index">Индекс, с которого начинается удаление</param>
        /// <param name="count">Кол-во элементов на удаление</param>
        public static void RemoveSinceUnique<T>(this List<T> list, int index, int count)
        {
            list.RemoveRange(index, count);
        }

        /// <summary>
        /// Вырезать из списка определённый диапазон
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">Обрабатываемый список</param>
        /// <param name="startIndex">Индекс начала диапазона</param>
        /// <param name="endIndex">Индекс конца диапазона. Если он равень -1 ,то до конца списка</param>
        /// <returns>Выходной список</returns>
        public static List<T> CutList<T>(this List<T> list, int startIndex, int endIndex = -1)
        {
            List<T> result = new List<T>();
            int lastIndex = endIndex == -1 ? list.Count - 1 : endIndex;
            for (int i = 0; i <= lastIndex; i++)
            {
                result.Add(list[i]);
            }
            list.RemoveSinceUnique(startIndex, endIndex == -1 ? list.Count - startIndex : list.Count - endIndex);
            return result;
        }
        /// <summary>
        /// Скопировать из списка определённый диапазон
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">Обрабатываемый список</param>
        /// <param name="startIndex">Индекс начала диапазона</param>
        /// <param name="endIndex">Индекс конца диапазона. Если он равень -1 ,то до конца списка</param>
        /// <returns>Выходной список</returns>
        public static List<T> CopyList<T>(this List<T> list, int startIndex, int endIndex = -1)
        {
            List<T> result = new List<T>();
            int lastIndex = endIndex == -1 ? list.Count - 1 : endIndex;
            for (int i = startIndex; i <= lastIndex; i++)
            {
                result.Add(list[i]);
            }
            return result;
        }
        /// <summary>
        /// Получение самого короткого списка
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first">Список №1</param>
        /// <param name="second">Список №2</param>
        /// <returns>Ссылка на самый которкий список</returns>
        public static List<T> GetShortestList<T>(this List<T> first, List<T> second)
        {
            int minSize = Math.Min(first.Count, second.Count);

            return first.Count == minSize ? first : second;
        }


        public static void AddUniqueRange<T>(this List<T> list, IEnumerable<T> values)
        {
            foreach (T item in values)
            {
                if (!list.Contains(item))
                    list.Add(item);
            }
        }

        public static List<T> CutListByCount<T>(this List<T> list, int startIndex, int count)
        {
            List<T> result = new List<T>();

            for (int i = startIndex; result.Count != count; i++)
            {
                result.Add(list[i]);
            }

            list.RemoveRange(startIndex, count);

            return result;
        }

        public static int[] IndexesofRange<T>(this List<T> list, IEnumerable<T> values)
        {

            int index = list.IndexOf(values.First());

            List<int> indexesToDelete = new List<int>()
            {
                index
            };

            for (int i = index + 1; i < list.Count && (i - index) < values.Count(); i++)
            {

                if (indexesToDelete.Count == values.Count())
                    return indexesToDelete.ToArray();



                if (list[i].Equals(values.ElementAt(i - index)))
                    indexesToDelete.Add(i);
                else
                {
                    index = list.IndexOf(values.First(), index + 1);
                    indexesToDelete.Clear();
                    indexesToDelete.Add(index);
                    i = index;
                }
            }

            return indexesToDelete.ToArray();
        }

        public static List<System.Drawing.Point> ToDrawingPointList(this List<Point> list)
        {
            List<System.Drawing.Point> result = new List<System.Drawing.Point>();

            foreach (Point labPoint in list)
            {
                result.Add(labPoint);
            }

            return result;
        }

        //     public static void DeleteRange<T>(this List<T> list, IEnumerable<T> values)
        //     {
        //         Queue<T> queueToDelete = new Queue<T>();

        //         int index = list.IndexOf(values.First());

        //         if (index == -1)
        //             throw new Exception("Нет такого диапазона");

        //for (int i = index; i < list.Count; i++)
        //         {
        //             if (list[i].Equals(values.ElementAt(i - index)))
        //	    queueToDelete.Enqueue(list[i]);
        //             else

        //}
        //     }
    }
}
