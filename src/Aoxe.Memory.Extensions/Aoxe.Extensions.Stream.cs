namespace Aoxe.Memory.Extensions;

public static partial class AoxeExtension
{
    public static ReadOnlyMemory<byte> ToReadOnlyMemory(this Stream stream) =>
        stream.ReadToEnd().AsMemory();

    public static ReadOnlySequence<byte> ToReadOnlySequence(this Stream stream) =>
        stream.ReadToEnd().ToReadOnlySequence();

    public static async ValueTask<ReadOnlyMemory<byte>> ToReadOnlyMemoryAsync(this Stream stream) =>
        (await stream.ReadToEndAsync()).AsMemory();

    public static async ValueTask<ReadOnlySequence<byte>> ToReadOnlySequenceAsync(
        this Stream stream
    ) => (await stream.ReadToEndAsync()).ToReadOnlySequence();
}
