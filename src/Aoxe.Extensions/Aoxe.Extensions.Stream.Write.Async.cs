namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    public static async ValueTask<bool> TryWriteAsync(
        this Stream? stream,
        byte[] buffer,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            if (stream is not { CanWrite: true })
                return false;

#if NETSTANDARD2_0
            await stream
                .WriteAsync(buffer, 0, buffer.Length, cancellationToken)
                .ConfigureAwait(false);
#else
            await stream.WriteAsync(buffer, cancellationToken).ConfigureAwait(false);
#endif
            return true;
        }
        catch (Exception ex)
            when (ex is IOException or ObjectDisposedException or NotSupportedException)
        {
            return false;
        }
    }

    public static async ValueTask<bool> TryWriteAsync(
        this Stream? stream,
        byte[] buffer,
        int offset,
        int count,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            if (stream is not { CanWrite: true })
                return false;

#if NETSTANDARD2_0
            await stream.WriteAsync(buffer, offset, count, cancellationToken).ConfigureAwait(false);
#else
            await stream
                .WriteAsync(buffer.AsMemory(offset, count), cancellationToken)
                .ConfigureAwait(false);
#endif
            return true;
        }
        catch (Exception ex)
            when (ex is IOException or ObjectDisposedException or NotSupportedException)
        {
            return false;
        }
    }

    public static async ValueTask WriteAsync(
        this Stream? stream,
        string? str,
        Encoding? encoding = null,
        CancellationToken cancellationToken = default
    )
    {
        if (stream is null || str is null)
            return;

        var bytes = (encoding ?? Encoding.UTF8).GetBytes(str);

#if NETSTANDARD2_0
        await stream.WriteAsync(bytes, 0, bytes.Length, cancellationToken).ConfigureAwait(false);
#else
        await stream.WriteAsync(bytes, cancellationToken).ConfigureAwait(false);
#endif
    }

    public static async ValueTask<bool> TryWriteAsync(
        this Stream? stream,
        string? str,
        Encoding? encoding = null,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            if (stream is not { CanWrite: true } || str is null)
                return false;

            var bytes = (encoding ?? Encoding.UTF8).GetBytes(str);
            return await stream.TryWriteAsync(bytes, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex) when (ex is ArgumentException or EncoderFallbackException)
        {
            return false;
        }
    }
}
