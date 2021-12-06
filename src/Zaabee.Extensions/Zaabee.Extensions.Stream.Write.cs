namespace Zaabee.Extensions;

public static partial class ZaabeeExtension
{
    public static bool TryWrite(this Stream? stream, byte[] buffer)
    {
        var canWrite = stream is not null && stream.CanWrite;
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
        var canWrite = stream is not null && stream.CanWrite;
        if (canWrite) stream!.Write(buffer, offset, count);
        return canWrite;
    }

    public static bool TryWriteByte(this Stream? stream, byte value)
    {
        var canWrite = stream is not null && stream.CanWrite;
        if (canWrite) stream!.WriteByte(value);
        return canWrite;
    }
}