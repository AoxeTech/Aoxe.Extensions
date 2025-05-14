namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    public static bool IsNullOrEmpty(this Stream? stream) =>
        stream is null or { CanSeek: true, Length: 0 };

    public static long TrySeek(this Stream? stream, long offset, SeekOrigin seekOrigin) =>
        stream?.CanSeek == true ? stream.Seek(offset, seekOrigin) : -1;

    public static MemoryStream ToMemoryStream(this Stream stream)
    {
        if (stream is null)
            throw new ArgumentNullException(nameof(stream));
        if (!stream.CanRead)
            throw new NotSupportedException("Stream is not readable");

        var originalPosition = stream.CanSeek ? stream.Position : 0;
        var memoryStream = new MemoryStream();

        try
        {
            if (stream.CanSeek)
                stream.Position = 0;

            stream.CopyTo(memoryStream);
        }
        finally
        {
            if (stream.CanSeek)
                stream.Position = originalPosition;
        }

        memoryStream.Position = 0;
        return memoryStream;
    }

    public static ReadOnlyMemory<byte> ToReadOnlyMemory(this Stream stream) =>
        stream.ReadToEnd().AsMemory();

    public static ReadOnlySequence<byte> ToReadOnlySequence(this Stream stream) =>
        new(stream.ReadToEnd());
}
