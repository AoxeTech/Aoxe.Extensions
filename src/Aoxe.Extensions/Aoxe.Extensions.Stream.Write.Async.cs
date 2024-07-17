namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    public static async ValueTask<bool> TryWriteAsync(
        this Stream? stream,
        byte[] buffer,
        CancellationToken cancellationToken = default
    )
    {
        var canWrite = stream?.CanWrite is true;
        if (canWrite)
#if NETSTANDARD2_0
            await stream!.WriteAsync(buffer, 0, buffer.Length, cancellationToken);
#else
            await stream!.WriteAsync(buffer, cancellationToken);
#endif
        return canWrite;
    }

    public static async ValueTask<bool> TryWriteAsync(
        this Stream? stream,
        byte[] buffer,
        int offset,
        int count,
        CancellationToken cancellationToken = default
    )
    {
        var canWrite = stream?.CanWrite is true;
        if (canWrite)
#if NETSTANDARD2_0
            await stream!.WriteAsync(buffer, offset, count, cancellationToken);
#else
            await stream!.WriteAsync(buffer.AsMemory(offset, count), cancellationToken);
#endif
        return canWrite;
    }

    public static ValueTask WriteAsync(
        this Stream? stream,
        string str,
        Encoding? encoding = null,
        CancellationToken cancellationToken = default
    )
    {
        if (stream is null)
            return default;
        var bytes = str.GetBytes(encoding ?? Encoding.UTF8);
#if NETSTANDARD2_0
        return new ValueTask(stream.WriteAsync(bytes, 0, bytes.Length, cancellationToken));
#else
        return stream.WriteAsync(bytes, cancellationToken);
#endif
    }

    public static ValueTask<bool> TryWriteAsync(
        this Stream? stream,
        string str,
        Encoding? encoding = null,
        CancellationToken cancellationToken = default
    ) => stream.TryWriteAsync(str.GetBytes(encoding ?? Encoding.UTF8), cancellationToken);
}
