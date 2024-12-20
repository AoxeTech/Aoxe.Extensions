namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    public static int TryRead(this Stream? stream, byte[] buffer) =>
        stream?.CanRead is true
#if NETSTANDARD2_0
            ? stream.Read(buffer, 0, buffer.Length)
#else
            ? stream.Read(buffer)
#endif
            : -1;

    public static int TryRead(this Stream? stream, byte[] buffer, int offset, int count) =>
        stream?.CanRead is true
#if NETSTANDARD2_0
            ? stream.Read(buffer, offset, count)
#else
            ? stream.Read(buffer.AsSpan(offset, count))
#endif
            : -1;

    public static int TryReadByte(this Stream? stream) =>
        stream?.CanRead is true ? stream.ReadByte() : -1;

    public static byte[] ReadToEnd(this Stream? stream)
    {
        switch (stream)
        {
            case null:
                return [];
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
