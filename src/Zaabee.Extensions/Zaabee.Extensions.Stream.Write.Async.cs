namespace Zaabee.Extensions;

public static partial class ZaabeeExtension
{
    public static async Task<bool> TryWriteAsync(this Stream? stream, byte[] buffer,
        CancellationToken cancellationToken = default) =>
        await stream.TryWriteAsync(buffer, 0, buffer.Length, cancellationToken);

    public static async Task<bool> TryWriteAsync(this Stream? stream, byte[] buffer, int offset, int count,
        CancellationToken cancellationToken = default)
    {
        var canWrite = stream is not null && stream.CanWrite;
#if NETSTANDARD2_0
        if (canWrite) await stream!.WriteAsync(buffer, offset, count, cancellationToken);
#else
        if (canWrite) await stream!.WriteAsync(buffer.AsMemory(offset, count), cancellationToken);
#endif
        return canWrite;
    }
}