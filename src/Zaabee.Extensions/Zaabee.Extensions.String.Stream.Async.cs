namespace Zaabee.Extensions;

public static partial class ZaabeeExtension
{
    public static async Task WriteToAsync(this string str,
        Stream stream,
        Encoding? encoding = null,
        CancellationToken cancellationToken = default)
    {
        var buffer = str.GetBytes(encoding);
#if NETSTANDARD2_0
        await stream.WriteAsync(buffer, 0, buffer.Length, cancellationToken);
#else
        await stream.WriteAsync(buffer, cancellationToken);
#endif
    }

    public static Task<bool> TryWriteToAsync(this string str,
        Stream stream,
        Encoding? encoding = null,
        CancellationToken cancellationToken = default)
    {
        var buffer = str.GetBytes(encoding);
        return stream.TryWriteAsync(buffer, 0, buffer.Length, cancellationToken);
    }
}