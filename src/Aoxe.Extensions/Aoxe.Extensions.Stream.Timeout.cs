namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
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

    public static bool TrySetReadTimeout(this Stream? stream, TimeSpan timeout)
    {
        var milliseconds = checked((int)timeout.TotalMilliseconds);
        return stream.TrySetReadTimeout(milliseconds);
    }

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

    public static bool TrySetWriteTimeout(this Stream? stream, TimeSpan timeout)
    {
        var milliseconds = checked((int)timeout.TotalMilliseconds);
        return stream.TrySetWriteTimeout(milliseconds);
    }
}
