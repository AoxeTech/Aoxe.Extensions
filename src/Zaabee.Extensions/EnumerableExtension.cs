using System;
using System.Collections.Generic;
using System.Linq;

namespace Zaabee.Extensions
{
    public static class EnumerableExtension
    {
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