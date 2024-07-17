namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    public static bool TryWrite(this Stream? stream, byte[] buffer)
    {
        var canWrite = stream?.CanWrite is true;
        if (canWrite)
#if NETSTANDARD2_0
            stream!.Write(buffer, 0, buffer.Length);
#else
            stream!.Write(buffer);
#endif
        return canWrite;
    }

    public static bool TryWrite(this Stream? stream, byte[] buffer, int offset, int count)
    {
        var canWrite = stream?.CanWrite is true;
        if (canWrite)
#if NETSTANDARD2_0
            stream!.Write(buffer, offset, count);
#else
            stream!.Write(buffer);
#endif
        return canWrite;
    }

    public static bool TryWriteByte(this Stream? stream, byte value)
    {
        var canWrite = stream?.CanWrite is true;
        if (canWrite)
            stream!.WriteByte(value);
        return canWrite;
    }

    public static void Write(this Stream? stream, string str, Encoding? encoding = null)
    {
        if (stream is null)
            return;
        var bytes = str.GetBytes(encoding ?? Encoding.UTF8);
#if NETSTANDARD2_0
        stream.Write(bytes, 0, bytes.Length);
#else
        stream.Write(bytes);
#endif
    }

    public static bool TryWrite(this Stream? stream, string str, Encoding? encoding = null) =>
        stream.TryWrite(str.GetBytes(encoding ?? Encoding.UTF8));
}
