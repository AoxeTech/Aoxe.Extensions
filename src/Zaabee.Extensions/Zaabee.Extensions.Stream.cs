namespace Zaabee.Extensions;

public static partial class ZaabeeExtension
{
    public static bool IsNullOrEmpty(this Stream? stream) =>
        stream is null || stream.Length is 0;

    public static long TrySeek(this Stream? stream, long offset, SeekOrigin seekOrigin) =>
        stream is not null && stream.CanSeek ? stream.Seek(offset, seekOrigin) : default;

    public static int TryRead(this Stream? stream, byte[] buffer, int offset, int count) =>
        stream is not null && stream.CanRead ? stream.Read(buffer, offset, count) : default;

    public static Task<int> TryReadAsync(this Stream? stream, byte[] buffer, int offset, int count,
        CancellationToken cancellationToken = default) =>
        stream is not null && stream.CanRead
            ? stream.ReadAsync(buffer, offset, count, cancellationToken)
            : Task.FromResult(0);

    public static int TryReadByte(this Stream? stream) =>
        stream is not null && stream.CanRead ? stream.ReadByte() : default;

    public static bool TryWrite(this Stream? stream, byte[] buffer, int offset, int count)
    {
        var canWrite = stream is not null && stream.CanWrite;
        if (canWrite) stream!.Write(buffer, offset, count);
        return canWrite;
    }

    public static async Task<bool> TryWriteAsync(this Stream? stream, byte[] buffer, int offset, int count,
        CancellationToken cancellationToken = default)
    {
        var canWrite = stream is not null && stream.CanWrite;
        if (canWrite) await stream!.WriteAsync(buffer, offset, count, cancellationToken);
        return canWrite;
    }

    public static bool TryWriteByte(this Stream? stream, byte value)
    {
        var canWrite = stream is not null && stream.CanWrite;
        if (canWrite) stream!.WriteByte(value);
        return canWrite;
    }

    public static bool TrySetReadTimeout(this Stream? stream, int milliseconds)
    {
        var canTimeout = stream is not null && stream.CanTimeout;
        if (canTimeout) stream!.ReadTimeout = milliseconds;
        return canTimeout;
    }

    public static bool TrySetReadTimeout(this Stream? stream, TimeSpan timeout) =>
        stream.TrySetReadTimeout(timeout.Milliseconds);

    public static bool TrySetWriteTimeout(this Stream? stream, int milliseconds)
    {
        var canTimeout = stream is not null && stream.CanTimeout;
        if (canTimeout) stream!.WriteTimeout = milliseconds;
        return canTimeout;
    }

    public static bool TrySetWriteTimeout(this Stream? stream, TimeSpan timeout) =>
        stream.TrySetWriteTimeout(timeout.Milliseconds);

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

    public static async Task<byte[]> ReadToEndAsync(this Stream? stream, CancellationToken cancellationToken = default)
    {
        switch (stream)
        {
            case null:
                return Array.Empty<byte>();
            case MemoryStream ms:
                return ms.ToArray();
            default:
#if NETSTANDARD2_0
                using (var memoryStream = new MemoryStream())
#else
                await using (var memoryStream = new MemoryStream())
#endif
                {

#if NETSTANDARD2_0
                    await stream.CopyToAsync(memoryStream);
#else
                    await stream.CopyToAsync(memoryStream, cancellationToken);
#endif
                    return memoryStream.ToArray();
                }
        }
    }

    public static async Task<byte[]> ReadToEndAsync(this Stream? stream, int bufferSize,
        CancellationToken cancellationToken = default)
    {
        switch (stream)
        {
            case null:
                return Array.Empty<byte>();
            case MemoryStream ms:
                return ms.ToArray();
            default:
#if NET5_0
                await using (var memoryStream = new MemoryStream())
#else
                using (var memoryStream = new MemoryStream())
#endif
                {
                    await stream.CopyToAsync(memoryStream, bufferSize, cancellationToken);
                    return memoryStream.ToArray();
                }
        }
    }
}