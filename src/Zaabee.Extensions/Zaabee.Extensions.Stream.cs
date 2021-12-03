namespace Zaabee.Extensions;

public static partial class ZaabeeExtension
{
    public static bool IsNullOrEmpty(this Stream? stream) =>
        stream is null || stream.Length is 0;

    public static long TrySeek(this Stream? stream, long offset, SeekOrigin seekOrigin) =>
        stream is not null && stream.CanSeek ? stream.Seek(offset, seekOrigin) : default;
}