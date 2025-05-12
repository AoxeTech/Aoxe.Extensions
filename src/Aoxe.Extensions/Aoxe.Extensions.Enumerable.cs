namespace Aoxe.Extensions;

/// <summary>
/// Provides extension methods for collection manipulation and conversion.
/// </summary>
public static partial class AoxeExtension
{
    /// <summary>
    /// Adds multiple items to a collection.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="source">The target collection.</param>
    /// <param name="collections">The items to add.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="source"/> or <paramref name="collections"/> is null.
    /// </exception>
    /// <remarks>
    /// Optimized for <see cref="List{T}"/> using its AddRange method. For other collection types,
    /// items are added sequentially.
    /// </remarks>
    public static void AddRange<T>(this ICollection<T> source, IEnumerable<T> collections)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));
        if (collections == null)
            throw new ArgumentNullException(nameof(collections));

        if (source is List<T> list)
        {
            list.AddRange(collections);
            return;
        }

        foreach (var item in collections)
        {
            source.Add(item);
        }
    }

    /// <summary>
    /// Finds the index of the first occurrence of a value in a sequence.
    /// </summary>
    /// <typeparam name="T">The type of elements in the sequence.</typeparam>
    /// <param name="source">The sequence to search.</param>
    /// <param name="value">The value to locate.</param>
    /// <param name="comparer">The equality comparer to use (defaults to <see cref="EqualityComparer{T}.Default"/>).</param>
    /// <returns>The zero-based index of the value, or -1 if not found.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is null.</exception>
    public static int IndexOf<T>(
        this IEnumerable<T?> source,
        T? value,
        IEqualityComparer<T?>? comparer = null
    )
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

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

    /// <summary>
    /// Executes an action for each element in the sequence, providing the element's index.
    /// </summary>
    /// <typeparam name="T">The type of elements in the sequence.</typeparam>
    /// <param name="source">The sequence to iterate.</param>
    /// <param name="action">The action to execute for each element.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is null.</exception>
    public static void IndexForeach<T>(this IEnumerable<T> source, Action<int, T> action)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

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

    /// <summary>
    /// Determines whether a sequence does not contain a specified element.
    /// </summary>
    /// <typeparam name="T">The type of elements in the sequence.</typeparam>
    /// <param name="source">The sequence to check.</param>
    /// <param name="item">The item to locate.</param>
    /// <returns>true if the item is not found; otherwise, false.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is null.</exception>
    public static bool NotContains<T>(this IEnumerable<T> source, T item)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        return !source.Contains(item);
    }

    /// <summary>
    /// Filters and converts a sequence to a list.
    /// </summary>
    /// <typeparam name="T">The type of elements in the sequence.</typeparam>
    /// <param name="source">The sequence to convert.</param>
    /// <param name="predicate">An optional filter predicate.</param>
    /// <returns>A filtered list of elements.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is null.</exception>
    public static List<T?> ToList<T>(this IEnumerable<T?> source, Func<T?, bool> predicate)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        return source.Where(predicate).ToList();
    }

    /// <summary>
    /// Executes an action for each element in the sequence.
    /// </summary>
    /// <typeparam name="T">The type of elements in the sequence.</typeparam>
    /// <param name="source">The sequence to iterate.</param>
    /// <param name="action">The action to execute for each element.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is null.</exception>
    public static void ForEach<T>(this IEnumerable<T?> source, Action<T?>? action)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));
        if (action == null)
            return;

        foreach (var item in source)
        {
            action(item);
        }
    }

    /// <summary>
    /// Executes an action for each element in a sequence while maintaining lazy evaluation.
    /// </summary>
    /// <typeparam name="T">The type of elements in the sequence.</typeparam>
    /// <param name="source">The sequence to iterate.</param>
    /// <param name="action">The action to execute for each element.</param>
    /// <returns>The original sequence.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is null.</exception>
    /// <remarks>
    /// The action will be executed each time the sequence is enumerated.
    /// </remarks>
    public static IEnumerable<T?> ForEachLazy<T>(this IEnumerable<T?> source, Action<T?>? action)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));
        if (action == null)
            return source;

        return source.Select(item =>
        {
            action(item);
            return item;
        });
    }
}
