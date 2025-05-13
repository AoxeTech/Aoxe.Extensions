namespace Aoxe.Extensions;

/// <summary>
/// Provides asynchronous stream operation extensions.
/// </summary>
public static partial class AoxeExtension
{
    /// <summary>
    /// Asynchronously writes the entire byte array to a stream.
    /// </summary>
    /// <param name="buffer">The byte array to write.</param>
    /// <param name="stream">The target stream to write to.</param>
    /// <param name="cancellationToken">Cancellation token to observe.</param>
    /// <returns>A ValueTask representing the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="buffer"/> or <paramref name="stream"/> is null.</exception>
    public static ValueTask WriteToAsync(
        this byte[] buffer,
        Stream stream,
        CancellationToken cancellationToken = default
    )
    {
        if (buffer is null)
            throw new ArgumentNullException(nameof(buffer));
        if (stream is null)
            throw new ArgumentNullException(nameof(stream));

#if NETSTANDARD2_0
        return new ValueTask(stream.WriteAsync(buffer, 0, buffer.Length, cancellationToken));
#else
        return stream.WriteAsync(buffer, cancellationToken);
#endif
    }

    /// <summary>
    /// Attempts to asynchronously write the entire byte array to a stream.
    /// </summary>
    /// <param name="buffer">The byte array to write.</param>
    /// <param name="stream">The target stream to write to.</param>
    /// <param name="cancellationToken">Cancellation token to observe.</param>
    /// <returns>
    /// A <see cref="ValueTask{bool}"/> that completes with true if write succeeded,
    /// or false if operation was canceled.
    /// </returns>
    public static async ValueTask<bool> TryWriteToAsync(
        this byte[] buffer,
        Stream stream,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            await buffer.WriteToAsync(stream, cancellationToken).ConfigureAwait(false);
            return true;
        }
        catch (TaskCanceledException)
        {
            return false;
        }
    }
}
