using System;
using System.Collections.Concurrent;

namespace Zaabee.Extensions
{
    public static class ConcurrentDictionaryExtensions
    {
        public static TValue GetOrAdd<TKey, TValue>(
            this ConcurrentDictionary<TKey, Lazy<TValue>> @this,
            TKey key, Func<TKey, TValue> valueFactory
        ) => @this.GetOrAdd(key, k => new Lazy<TValue>(() => valueFactory(k))).Value;

        public static TValue AddOrUpdate<TKey, TValue>(
            this ConcurrentDictionary<TKey, Lazy<TValue>> @this,
            TKey key, Func<TKey, TValue> addValueFactory,
            Func<TKey, TValue, TValue> updateValueFactory
        ) => @this.AddOrUpdate(key,
            k => new Lazy<TValue>(() => addValueFactory(k)),
            (k, currentValue) => new Lazy<TValue>(
                () => updateValueFactory(k, currentValue.Value)
            )).Value;

        public static bool TryGetValue<TKey, TValue>(
            this ConcurrentDictionary<TKey, Lazy<TValue>> @this,
            TKey key, out TValue value
        )
        {
            value = default;
            var result = @this.TryGetValue(key, out var v);
            if (result) value = v.Value;
            return result;
        }

        // this overload may not make sense to use when you want to avoid
        // the construction of the value when it isn't needed
        public static bool TryAdd<TKey, TValue>(
            this ConcurrentDictionary<TKey, Lazy<TValue>> @this,
            TKey key, TValue value
        ) => @this.TryAdd(key, new Lazy<TValue>(() => value));

        public static bool TryAdd<TKey, TValue>(
            this ConcurrentDictionary<TKey, Lazy<TValue>> @this,
            TKey key, Func<TKey, TValue> valueFactory
        ) => @this.TryAdd(key, new Lazy<TValue>(() => valueFactory(key)));

        public static bool TryRemove<TKey, TValue>(
            this ConcurrentDictionary<TKey, Lazy<TValue>> @this,
            TKey key, out TValue value
        )
        {
            value = default;
            if (!@this.TryRemove(key, out var v)) return false;
            value = v.Value;
            return true;
        }

        public static bool TryUpdate<TKey, TValue>(
            this ConcurrentDictionary<TKey, Lazy<TValue>> @this,
            TKey key, Func<TKey, TValue, TValue> updateValueFactory
        ) => @this.TryGetValue(key, out var existingValue) && @this.TryUpdate(key,
                 new Lazy<TValue>(() => updateValueFactory(key, existingValue.Value)), existingValue);
    }
}