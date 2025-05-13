namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    public static async ValueTask<MemoryStream> ToMemoryStreamAsync(this Stream stream)
    {
        var memoryStream = new MemoryStream();
        await stream.CopyToAsync(memoryStream);
        stream.TrySeek(0, SeekOrigin.Begin);
        memoryStream.TrySeek(0, SeekOrigin.Begin);
        return memoryStream;
    }

    /// <summary>
    /// Asynchronously reads all bytes into a <see cref="ReadOnlyMemory{T}"/>.
    /// </summary>
    public static async ValueTask<ReadOnlyMemory<byte>> ToReadOnlyMemoryAsync(this Stream stream) =>
        (await stream.ReadToEndAsync().ConfigureAwait(false)).AsMemory();

    /// <summary>
    /// Asynchronously reads all bytes into a <see cref="ReadOnlySequence{T}"/>.
    /// </summary>
    public static async ValueTask<ReadOnlySequence<byte>> ToReadOnlySequenceAsync(
        this Stream stream
    ) => new(await stream.ReadToEndAsync().ConfigureAwait(false));
}
