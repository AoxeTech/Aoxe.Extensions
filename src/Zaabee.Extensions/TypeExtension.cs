using System;
using System.Collections.Concurrent;

namespace Zaabee.Extensions
{
    public static class TypeExtension
    {
        private static readonly ConcurrentDictionary<Type, object> ValueTypeCache =
            new ConcurrentDictionary<Type, object>();

        public static object GetDefaultValue(this Type type) =>
            type.IsValueType
                ? ValueTypeCache.GetOrAdd(type, _ => Activator.CreateInstance(type))
                : null;
    }
}