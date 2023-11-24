namespace Zaabee.Memory.Extensions;

public static partial class ZaabeeExtension
{
    public static Span<T> ToSpan<T>(this T[] array) => new(array);

    public static Span<T> ToSpan<T>(this T[] array, int start, int length) =>
        new(array, start, length);

    public static Memory<T> ToMemory<T>(this T[] array) => new(array);

    public static Memory<T> ToMemory<T>(this T[] array, int start, int length) =>
        new(array, start, length);

    public static ReadOnlySpan<T> ToReadOnlySpan<T>(this T[] array) => new(array);

    public static ReadOnlySpan<T> ToReadOnlySpan<T>(this T[] array, int start, int length) =>
        new(array, start, length);

    public static ReadOnlyMemory<T> ToReadOnlyMemory<T>(this T[] array) => new(array);

    public static ReadOnlyMemory<T> ToReadOnlyMemory<T>(this T[] array, int start, int length) =>
        new(array, start, length);

    public static ReadOnlySequence<T> ToReadOnlySequence<T>(this T[] array) => new(array);

    public static ReadOnlySequence<T> ToReadOnlySequence<T>(
        this T[] array,
        int start,
        int length
    ) => new(array, start, length);
}
