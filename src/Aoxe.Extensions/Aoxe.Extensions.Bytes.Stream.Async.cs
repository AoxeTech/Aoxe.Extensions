namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    public static ValueTask WriteToAsync(
        this byte[] buffer,
        Stream stream,
        CancellationToken cancellationToken = default
    ) =>
#if NETSTANDARD2_0
        new(stream.WriteAsync(buffer, 0, buffer.Length, cancellationToken));
#else
        stream.WriteAsync(buffer, cancellationToken);
#endif

    public static async ValueTask<bool> TryWriteToAsync(
        this byte[] buffer,
        Stream stream,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            await buffer.WriteToAsync(stream, cancellationToken);
            return true;
        }
        catch (TaskCanceledException)
        {
            return false;
        }
    }
}
