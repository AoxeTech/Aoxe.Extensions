namespace Zaabee.Extensions;

public static partial class ZaabeeExtension
{
    public static async Task WriteToAsync(this byte[] buffer, Stream stream,
        CancellationToken cancellationToken = default) =>
#if NETSTANDARD2_0
        await stream.WriteAsync(buffer, 0, buffer.Length, cancellationToken);
#else
        await stream.WriteAsync(buffer, cancellationToken);
#endif

    public static Task<bool> TryWriteToAsync(this byte[] buffer, Stream stream,
        CancellationToken cancellationToken = default) =>
        stream.TryWriteAsync(buffer, 0, buffer.Length, cancellationToken);
}