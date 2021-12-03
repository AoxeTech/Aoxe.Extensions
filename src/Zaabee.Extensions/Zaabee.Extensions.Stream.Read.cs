namespace Zaabee.Extensions;

public static partial class ZaabeeExtension
{
    public static int TryRead(this Stream? stream, byte[] buffer) =>
        stream.TryRead(buffer, 0, buffer.Length);

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
}