namespace Zaabee.Extensions;

public static partial class ZaabeeExtension
{
    public static async Task<MemoryStream> ToStreamAsync(this byte[] buffer,
        CancellationToken cancellationToken = default)
    {
        var ms = new MemoryStream();
        await buffer.WriteToAsync(ms, cancellationToken);
        ms.Seek(0, SeekOrigin.Begin);
        return ms;
    }

    public static async Task<MemoryStream> TryToStreamAsync(this byte[] buffer,
        CancellationToken cancellationToken = default)
    {
        var ms = new MemoryStream();
        await buffer.TryWriteToAsync(ms, cancellationToken);
        ms.Seek(0, SeekOrigin.Begin);
        return ms;
    }

    public static async Task WriteToAsync(this byte[] buffer, Stream stream,
        CancellationToken cancellationToken = default) =>
        await stream.WriteAsync(buffer, 0, buffer.Length, cancellationToken);

    public static Task<bool> TryWriteToAsync(this byte[] buffer, Stream stream,
        CancellationToken cancellationToken = default) =>
        stream.TryWriteAsync(buffer, 0, buffer.Length, cancellationToken);
}