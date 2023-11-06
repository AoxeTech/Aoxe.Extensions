namespace Zaabee.Extensions;

public static partial class ZaabeeExtension
{
    public static bool IsNullOrEmpty(this Stream? stream) =>
        stream is null || stream.Length is 0;

    public static long TrySeek(this Stream? stream, long offset, SeekOrigin seekOrigin) =>
        stream is not null && stream.CanSeek ? stream.Seek(offset, seekOrigin) : default;

    public static MemoryStream ToMemoryStream(this Stream stream)
    {
        var memoryStream = new MemoryStream();
        stream.CopyTo(memoryStream);
        stream.TrySeek(0, SeekOrigin.Begin);
        memoryStream.TrySeek(0, SeekOrigin.Begin);
        return memoryStream;
    }
}