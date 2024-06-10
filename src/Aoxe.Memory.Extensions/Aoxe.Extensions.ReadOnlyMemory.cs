namespace Aoxe.Memory.Extensions;

public static partial class AoxeExtension
{
    public static ReadOnlySequence<T> ToReadOnlySequence<T>(this ReadOnlyMemory<T> memory) =>
        new(memory);
}
