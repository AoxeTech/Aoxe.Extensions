using System;
using System.Collections.Generic;
using System.Linq;

namespace Zaabee.Extensions
{
    public static class EnumerableExtension
    {
        public static void AddRange<T>(this IList<T> source, IEnumerable<T> collections)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (collections is null) throw new ArgumentNullException(nameof(collections));

            if (source is List<T> list) list.AddRange(collections);
            else
                foreach (var item in collections)
                    source.Add(item);
        }

        public static int IndexOf<T>(this IEnumerable<T> source, T value, IEqualityComparer<T> comparer = null)
        {
            var index = 0;
            comparer ??= EqualityComparer<T>.Default; // or pass in as a parameter
            foreach (var item in source)
            {
                if (comparer.Equals(item, value)) return index;
                index++;
            }

            return -1;
        }

        public static bool NotContains<T>(this IEnumerable<T> source, T item) =>
            !source.Contains(item);

        public static IList<T> ToList<T>(this IEnumerable<T> src, Func<T, bool> func)
        {
            if (src is null) throw new ArgumentNullException(nameof(src));
            return func is null ? src.ToList() : src.Where(func).ToList();
        }

        public static void ForEach<T>(this IEnumerable<T> src, Action<T> action)
        {
            if (src is null) throw new ArgumentNullException(nameof(src));
            if (action is null) return;
            foreach (var i in src) action(i);
        }

        public static IEnumerable<T> ForEachLazy<T>(this IEnumerable<T> src, Action<T> action)
        {
            if (src is null) throw new ArgumentNullException(nameof(src));
            if (action is null) return src;
            return src.Select(i =>
            {
                action(i);
                return i;
            });
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> src) => src is null || !src.Any();
    }
}