namespace Zaabee.Memory.Extensions;

public static partial class SpanExtensions
{
    public static ReadOnlySequence<T> ToReadOnlySequence<T>(this ReadOnlyMemory<T> memory) =>
        new(memory);
}