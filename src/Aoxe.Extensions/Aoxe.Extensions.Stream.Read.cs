namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    public static int TryRead(this Stream? stream, byte[] buffer) =>
        stream is not null && stream.CanRead
#if NETSTANDARD2_0
            ? stream.Read(buffer, 0, buffer.Length)
#else
            ? stream.Read(buffer)
#endif
            : default;

    public static int TryRead(this Stream? stream, byte[] buffer, int offset, int count) =>
        stream is not null && stream.CanRead ? stream.Read(buffer, offset, count) : default;

    public static int TryReadByte(this Stream? stream) =>
        stream is not null && stream.CanRead ? stream.ReadByte() : -1;

    public static byte[] ReadToEnd(this Stream? stream)
    {
        switch (stream)
        {
            case null:
                return Array.Empty<byte>();
            case MemoryStream ms:
                return ms.ToArray();
            default:
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
        }
    }

    public static string ReadString(this Stream? stream, Encoding? encoding = null) =>
        stream is null ? string.Empty : stream.ReadToEnd().GetString(encoding ?? Utf8Encoding);
}
