namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    public static bool TrySetReadTimeout(this Stream? stream, int milliseconds)
    {
        var canTimeout = stream?.CanTimeout is true;
        if (canTimeout)
            stream!.ReadTimeout = milliseconds;
        return canTimeout;
    }

    public static bool TrySetReadTimeout(this Stream? stream, TimeSpan timeout) =>
        stream.TrySetReadTimeout(timeout.Milliseconds);

    public static bool TrySetWriteTimeout(this Stream? stream, int milliseconds)
    {
        var canTimeout = stream?.CanTimeout is true;
        if (canTimeout)
            stream!.WriteTimeout = milliseconds;
        return canTimeout;
    }

    public static bool TrySetWriteTimeout(this Stream? stream, TimeSpan timeout) =>
        stream.TrySetWriteTimeout(timeout.Milliseconds);
}
