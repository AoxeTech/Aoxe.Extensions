namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    /// <summary>
    /// Converts an array to a <see cref="Span{T}"/>
    /// </summary>
    /// <param name="array">Source array to convert</param>
    public static Span<T> ToSpan<T>(this T[] array) => new(array);

    /// <summary>
    /// Converts a portion of an array to a <see cref="Span{T}"/>
    /// </summary>
    /// <param name="array">Source array to convert</param>
    /// <param name="start">Starting index in the array</param>
    /// <param name="length">Number of elements to include</param>
    public static Span<T> ToSpan<T>(this T[] array, int start, int length) =>
        new(array, start, length);

    /// <summary>
    /// Converts an array to a <see cref="Memory{T}"/>
    /// </summary>
    /// <param name="array">Source array to convert</param>
    public static Memory<T> ToMemory<T>(this T[] array) => new(array);

    /// <summary>
    /// Converts a portion of an array to a <see cref="Memory{T}"/>
    /// </summary>
    /// <param name="array">Source array to convert</param>
    /// <param name="start">Starting index in the array</param>
    /// <param name="length">Number of elements to include</param>
    public static Memory<T> ToMemory<T>(this T[] array, int start, int length) =>
        new(array, start, length);

    /// <summary>
    /// Converts an array to a <see cref="ReadOnlySpan{T}"/>
    /// </summary>
    /// <param name="array">Source array to convert</param>
    public static ReadOnlySpan<T> ToReadOnlySpan<T>(this T[] array) => new(array);

    /// <summary>
    /// Converts a portion of an array to a <see cref="ReadOnlySpan{T}"/>
    /// </summary>
    /// <param name="array">Source array to convert</param>
    /// <param name="start">Starting index in the array</param>
    /// <param name="length">Number of elements to include</param>
    public static ReadOnlySpan<T> ToReadOnlySpan<T>(this T[] array, int start, int length) =>
        new(array, start, length);

    /// <summary>
    /// Converts an array to a <see cref="ReadOnlyMemory{T}"/>
    /// </summary>
    /// <param name="array">Source array to convert</param>
    public static ReadOnlyMemory<T> ToReadOnlyMemory<T>(this T[] array) => new(array);

    /// <summary>
    /// Converts a portion of an array to a <see cref="ReadOnlyMemory{T}"/>
    /// </summary>
    /// <param name="array">Source array to convert</param>
    /// <param name="start">Starting index in the array</param>
    /// <param name="length">Number of elements to include</param>
    public static ReadOnlyMemory<T> ToReadOnlyMemory<T>(this T[] array, int start, int length) =>
        new(array, start, length);

    /// <summary>
    /// Converts an array to a <see cref="ReadOnlySequence{T}"/>
    /// </summary>
    /// <param name="array">Source array to convert</param>
    public static ReadOnlySequence<T> ToReadOnlySequence<T>(this T[] array) => new(array);

    /// <summary>
    /// Converts a portion of an array to a <see cref="ReadOnlySequence{T}"/>
    /// </summary>
    /// <param name="array">Source array to convert</param>
    /// <param name="start">Starting index in the array</param>
    /// <param name="length">Number of elements to include</param>
    public static ReadOnlySequence<T> ToReadOnlySequence<T>(
        this T[] array,
        int start,
        int length
    ) => new(array, start, length);
}
