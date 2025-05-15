namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    public static async ValueTask WriteToAsync(
        this string str,
        Stream stream,
        Encoding? encoding = null,
        CancellationToken cancellationToken = default
    )
    {
        if (str is null)
            throw new ArgumentNullException(nameof(str));
        if (stream is null)
            throw new ArgumentNullException(nameof(stream));
        if (!stream.CanWrite)
            throw new InvalidOperationException("Stream is not writable");

        var buffer = (encoding ?? Encoding.UTF8).GetBytes(str);

#if NETSTANDARD2_0
        await stream.WriteAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false);
#else
        await stream.WriteAsync(buffer, cancellationToken).ConfigureAwait(false);
#endif
    }

    public static async ValueTask<bool> TryWriteToAsync(
        this string str,
        Stream? stream,
        Encoding? encoding = null,
        CancellationToken cancellationToken = default
    )
    {
        if (str is null)
            throw new ArgumentNullException(nameof(str));
        if (stream is null || !stream.CanWrite)
            return false;
        try
        {
            var buffer = (encoding ?? Encoding.UTF8).GetBytes(str);

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
            when (ex
                    is IOException
                        or UnauthorizedAccessException
                        or NotSupportedException
                        or ObjectDisposedException
            )
        {
            return false;
        }
    }
}
