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
            if (func is null) throw new ArgumentNullException(nameof(func));
            return src.Where(func).ToList();
        }

        public static void ForEach<T>(this IEnumerable<T> src, Action<T> action)
        {
            if (src is null) throw new ArgumentNullException(nameof(src));
            if (action is null) throw new ArgumentNullException(nameof(action));
            src.Select(i =>
            {
                action(i);
                return i;
            });
        }
    }
}