namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    public static async ValueTask<MemoryStream> ToMemoryStreamAsync(this Stream stream)
    {
        if (stream is null)
            throw new ArgumentNullException(nameof(stream));
        var memoryStream = new MemoryStream();
        await stream.CopyToAsync(memoryStream);
        stream.TrySeek(0, SeekOrigin.Begin);
        memoryStream.TrySeek(0, SeekOrigin.Begin);
        return memoryStream;
    }

    public static async ValueTask<ReadOnlyMemory<byte>> ToReadOnlyMemoryAsync(this Stream stream)
    {
        if (stream is null)
            throw new ArgumentNullException(nameof(stream));
        return (await stream.ReadToEndAsync().ConfigureAwait(false)).AsMemory();
    }

    public static async ValueTask<ReadOnlySequence<byte>> ToReadOnlySequenceAsync(
        this Stream stream
    )
    {
        if (stream is null)
            throw new ArgumentNullException(nameof(stream));
        return new ReadOnlySequence<byte>(await stream.ReadToEndAsync().ConfigureAwait(false));
    }
}
