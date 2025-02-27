namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    /// <summary>
    /// Attempts to set the read timeout for a stream
    /// </summary>
    /// <param name="stream">Target stream</param>
    /// <param name="milliseconds">Timeout in milliseconds (must be non-negative)</param>
    /// <returns>True if timeout was set, false if stream doesn't support timeouts</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown for negative timeout values</exception>
    public static bool TrySetReadTimeout(this Stream? stream, int milliseconds)
    {
        if (milliseconds < 0)
            throw new ArgumentOutOfRangeException(
                nameof(milliseconds),
                "Timeout must be non-negative"
            );

        try
        {
            if (stream?.CanTimeout != true)
                return false;
            stream.ReadTimeout = milliseconds;
            return true;
        }
        catch (InvalidOperationException)
        {
            return false;
        }
    }

    /// <summary>
    /// Attempts to set the read timeout for a stream using a TimeSpan
    /// </summary>
    /// <exception cref="OverflowException">Thrown when timeout exceeds Int32.MaxValue milliseconds</exception>
    public static bool TrySetReadTimeout(this Stream? stream, TimeSpan timeout)
    {
        var milliseconds = checked((int)timeout.TotalMilliseconds);
        return stream.TrySetReadTimeout(milliseconds);
    }

    /// <summary>
    /// Attempts to set the write timeout for a stream
    /// </summary>
    /// <param name="stream">Target stream</param>
    /// <param name="milliseconds">Timeout in milliseconds (must be non-negative)</param>
    /// <returns>True if timeout was set, false if stream doesn't support timeouts</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown for negative timeout values</exception>
    public static bool TrySetWriteTimeout(this Stream? stream, int milliseconds)
    {
        if (milliseconds < 0)
            throw new ArgumentOutOfRangeException(
                nameof(milliseconds),
                "Timeout must be non-negative"
            );

        try
        {
            if (stream?.CanTimeout != true)
                return false;
            stream.WriteTimeout = milliseconds;
            return true;
        }
        catch (InvalidOperationException)
        {
            return false;
        }
    }

    /// <summary>
    /// Attempts to set the write timeout for a stream using a TimeSpan
    /// </summary>
    /// <exception cref="OverflowException">Thrown when timeout exceeds Int32.MaxValue milliseconds</exception>
    public static bool TrySetWriteTimeout(this Stream? stream, TimeSpan timeout)
    {
        var milliseconds = checked((int)timeout.TotalMilliseconds);
        return stream.TrySetWriteTimeout(milliseconds);
    }
}
