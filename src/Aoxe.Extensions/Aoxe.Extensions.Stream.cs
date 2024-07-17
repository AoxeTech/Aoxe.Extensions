namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    public static bool IsNullOrEmpty(this Stream? stream) => stream is null || stream.Length is 0;

    public static long TrySeek(this Stream? stream, long offset, SeekOrigin seekOrigin) =>
        stream?.CanSeek is true ? stream.Seek(offset, seekOrigin) : default;

    public static MemoryStream ToMemoryStream(this Stream stream)
    {
        var memoryStream = new MemoryStream();
        stream.CopyTo(memoryStream);
        stream.TrySeek(0, SeekOrigin.Begin);
        memoryStream.TrySeek(0, SeekOrigin.Begin);
        return memoryStream;
    }

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
