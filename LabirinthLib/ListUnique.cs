using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabirinthLib
{
    public static class ListUnique
    {
        public static bool AddUnique(this List<Point> list, Point value)
        {
            if (list.Contains(value))
                return false;
            list.Add(value);
            return true;
        }

        public static List<Point> UniteUnique(this List<Point> originList, List<Point> list)
        {
            List<Point> result = new List<Point>();
            result.AddRange(originList);
            foreach (Point point in list)
            {
                if (originList.Contains(point))
                    continue;
                else
                    result.Add(point);
            }
            return result;
        }

        public static void RemoveSinceUnique(this List<Point> list, int index)
        {
            int countToDelete = list.Count - index;
            list.RemoveRange(index, countToDelete);
        }

        public static void RemoveSinceUnique(this List<Point> list, int index, int count)
        {
            list.RemoveRange(index, count);
        }

        public static List<Point> CutList(this List<Point> list, int startIndex, int endIndex = -1)
        {
            List<Point> result = new List<Point>();
            int lastIndex = endIndex == -1 ? list.Count : endIndex;
            for (int i = 0; i < lastIndex; i++)
            {
                result.Add(list[i]);
            }
            return result;
        }
    }
}
