namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    public static void AddRange<T>(this ICollection<T> source, IEnumerable<T> collections)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        if (collections is null)
            throw new ArgumentNullException(nameof(collections));

        if (source is List<T> list)
        {
            list.AddRange(collections);
            return;
        }

        foreach (var item in collections)
            source.Add(item);
    }

    public static int IndexOf<T>(
        this IEnumerable<T?> source,
        T? value,
        IEqualityComparer<T?>? comparer = null
    )
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        if (value is null)
            throw new ArgumentNullException(nameof(value));

        comparer ??= EqualityComparer<T?>.Default;
        var index = 0;

        foreach (var item in source)
        {
            if (comparer.Equals(item, value))
                return index;
            index++;
        }

        return -1;
    }

    public static void IndexForeach<T>(this IEnumerable<T> source, Action<int, T> action)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        if (action is null)
            throw new ArgumentNullException(nameof(action));
#if NET9_0_OR_GREATER
        foreach (var (index, item) in source.Index())
        {
            action(index, item);
        }
#else
        var index = 0;
        foreach (var item in source)
        {
            action(index++, item);
        }
#endif
    }

    public static bool NotContains<T>(this IEnumerable<T> source, T item) => !source.Contains(item);

    public static List<T?> ToList<T>(this IEnumerable<T?> source, Func<T?, bool> predicate) =>
        source.Where(predicate).ToList();

    public static void ForEach<T>(this IEnumerable<T?> source, Action<T?>? action)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        if (action is null)
            return;

        foreach (var item in source)
            action(item);
    }

    public static IEnumerable<T?> ForEachLazy<T>(this IEnumerable<T?> source, Action<T?>? action)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        if (action is null)
            return source;

        return source.Select(item =>
        {
            action(item);
            return item;
        });
    }
}
