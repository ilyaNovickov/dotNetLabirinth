using System;
using System.Collections.Generic;

namespace LabirinthLib
{
    public static class ListUnique
    {
        public static bool AddUnique<T>(this List<T> list, T value)
        {
            if (list.Contains(value))
                return false;
            list.Add(value);
            return true;
        }

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

        public static void RemoveSinceUnique<T>(this List<T> list, int index)
        {
            int countToDelete = list.Count - index;
            list.RemoveRange(index, countToDelete);
        }

        public static void RemoveSinceUnique<T>(this List<T> list, int index, int count)
        {
            list.RemoveRange(index, count);
        }

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

        public static List<T> GetShortestList<T>(this List<T> first, List<T> second)
        {
            int minSize = Math.Min(first.Count, second.Count);

            return first.Count == minSize ? first : second;
        }
    }
}
