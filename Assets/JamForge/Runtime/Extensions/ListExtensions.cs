using System.Collections.Generic;

namespace JamForge
{
    public static class ListExtensions
    {
        public static T AddTo<T>(this T data, List<T> list)
        {
            list.Add(data);
            return data;
        }

        public static List<T> AddMultiple<T>(this List<T> list, params T[] data)
        {
            list.AddRange(data);
            return list;
        }
    }
}
