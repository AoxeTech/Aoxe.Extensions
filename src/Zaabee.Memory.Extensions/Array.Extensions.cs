namespace Zaabee.Memory.Extensions;

public static partial class SpanExtensions
{
    public static Span<T> ToSpan<T>(this T[] bytes) =>
        new(bytes);

    public static Span<T> ToSpan<T>(this T[] bytes, int start, int length) =>
        new(bytes, start, length);

    public static Memory<T> ToMemory<T>(this T[] bytes) =>
        new(bytes);

    public static Memory<T> ToMemory<T>(this T[] bytes, int start, int length) =>
        new(bytes, start, length);

    public static ReadOnlySpan<T> ToReadOnlySpan<T>(this T[] bytes) =>
        new(bytes);

    public static ReadOnlySpan<T> ToReadOnlySpan<T>(this T[] bytes, int start, int length) =>
        new(bytes, start, length);

    public static ReadOnlyMemory<T> ToReadOnlyMemory<T>(this T[] bytes) =>
        new(bytes);

    public static ReadOnlyMemory<T> ToReadOnlyMemory<T>(this T[] bytes, int start, int length) =>
        new(bytes, start, length);

    public static ReadOnlySequence<T> ToReadOnlySequence<T>(this T[] bytes) =>
        new(bytes);

    public static ReadOnlySequence<T> ToReadOnlySequence<T>(this T[] bytes, int start, int length) =>
        new(bytes, start, length);
}