namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    /// <summary>
    /// Asynchronously writes the string to a stream using the specified encoding
    /// </summary>
    /// <param name="str">The string to write</param>
    /// <param name="stream">Target stream to write to</param>
    /// <param name="encoding">Text encoding to use (default: UTF-8)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <exception cref="ArgumentNullException">Thrown when stream is null</exception>
    /// <exception cref="InvalidOperationException">Thrown when stream is not writable</exception>
    public static async ValueTask WriteToAsync(
        this string str,
        Stream stream,
        Encoding? encoding = null,
        CancellationToken cancellationToken = default
    )
    {
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

    /// <summary>
    /// Attempts to asynchronously write the string to a stream using the specified encoding
    /// </summary>
    /// <param name="str">The string to write</param>
    /// <param name="stream">Target stream to write to</param>
    /// <param name="encoding">Text encoding to use (default: UTF-8)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>
    /// ValueTask containing true if write succeeded, false if failed
    /// </returns>
    public static async ValueTask<bool> TryWriteToAsync(
        this string str,
        Stream? stream,
        Encoding? encoding = null,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            if (stream is null || !stream.CanWrite)
                return false;

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
