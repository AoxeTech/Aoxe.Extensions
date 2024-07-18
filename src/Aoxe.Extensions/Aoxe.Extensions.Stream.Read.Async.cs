namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    public static ValueTask<int> TryReadAsync(
        this Stream? stream,
        byte[] buffer,
        CancellationToken cancellationToken = default
    ) =>
        stream?.CanRead is true
#if NETSTANDARD2_0
            ? new ValueTask<int>(stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken))
#else
            ? stream.ReadAsync(buffer, cancellationToken)
#endif
            : new ValueTask<int>(0);

    public static ValueTask<int> TryReadAsync(
        this Stream? stream,
        byte[] buffer,
        int offset,
        int count,
        CancellationToken cancellationToken = default
    ) =>
        stream?.CanRead is true
#if NETSTANDARD2_0
            ? new ValueTask<int>(stream.ReadAsync(buffer, offset, count, cancellationToken))
#else
            ? stream.ReadAsync(buffer, cancellationToken)
#endif
            : new ValueTask<int>(0);

    public static async ValueTask<byte[]> ReadToEndAsync(
        this Stream? stream,
        CancellationToken cancellationToken = default
    )
    {
        switch (stream)
        {
            case null:
                return [];
            case MemoryStream ms:
                return ms.ToArray();
            default:
#if NETSTANDARD2_0
                using (var memoryStream = new MemoryStream())
                {
                    await stream.CopyToAsync(memoryStream);
#else
                await using (var memoryStream = new MemoryStream())
                {
                    await stream.CopyToAsync(memoryStream, cancellationToken);
#endif
                    return memoryStream.ToArray();
                }
        }
    }

    public static async ValueTask<byte[]> ReadToEndAsync(
        this Stream? stream,
        int bufferSize,
        CancellationToken cancellationToken = default
    )
    {
        switch (stream)
        {
            case null:
                return [];
            case MemoryStream ms:
                return ms.ToArray();
            default:
#if NETSTANDARD2_0
                using (var memoryStream = new MemoryStream())
#else
                await using (var memoryStream = new MemoryStream())
#endif
                {
                    await stream.CopyToAsync(memoryStream, bufferSize, cancellationToken);
                    return memoryStream.ToArray();
                }
        }
    }

    public static async ValueTask<string> ReadStringAsync(
        this Stream? stream,
        Encoding? encoding = null,
        CancellationToken cancellationToken = default
    ) =>
        stream is null
            ? string.Empty
            : (await stream.ReadToEndAsync(cancellationToken)).GetString(encoding ?? Utf8Encoding);
}
