namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    public static ValueTask WriteToAsync(
        this byte[] bytes,
        Stream stream,
        CancellationToken cancellationToken = default
    )
    {
        if (bytes is null)
            throw new ArgumentNullException(nameof(bytes));
        if (stream is null)
            throw new ArgumentNullException(nameof(stream));
        return
#if NETSTANDARD2_0
        new ValueTask(stream.WriteAsync(bytes, 0, bytes.Length, cancellationToken));
#else
        stream.WriteAsync(bytes, cancellationToken);
#endif
    }

    public static async ValueTask<bool> TryWriteToAsync(
        this byte[] bytes,
        Stream stream,
        CancellationToken cancellationToken = default
    )
    {
        if (bytes is null)
            throw new ArgumentNullException(nameof(bytes));
        if (stream is null)
            throw new ArgumentNullException(nameof(stream));
        try
        {
            await bytes.WriteToAsync(stream, cancellationToken);
            return true;
        }
        catch (TaskCanceledException)
        {
            return false;
        }
    }
}
