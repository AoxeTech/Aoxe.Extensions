namespace Zaabee.Extensions;

public static partial class ZaabeeExtension
{
#if NETSTANDARD2_0
    public static Task<int> TryReadAsync(this Stream? stream, byte[] buffer,
        CancellationToken cancellationToken = default) =>
        stream.TryReadAsync(buffer, 0, buffer.Length, cancellationToken);
#else
    public static ValueTask<int> TryReadAsync(this Stream? stream, byte[] buffer,
        CancellationToken cancellationToken = default) =>
        stream is not null && stream.CanRead
            ? stream.ReadAsync(buffer, cancellationToken)
#if NETCOREAPP3_1
            : new ValueTask<int>(0);
#else
            : ValueTask.FromResult(0);
#endif
#endif

    public static Task<int> TryReadAsync(this Stream? stream, byte[] buffer, int offset, int count,
        CancellationToken cancellationToken = default) =>
        stream is not null && stream.CanRead
            ? stream.ReadAsync(buffer, offset, count, cancellationToken)
            : Task.FromResult(0);

    public static async Task<byte[]> ReadToEndAsync(this Stream? stream, CancellationToken cancellationToken = default)
    {
        switch (stream)
        {
            case null:
                return Array.Empty<byte>();
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

    public static async Task<byte[]> ReadToEndAsync(this Stream? stream, int bufferSize,
        CancellationToken cancellationToken = default)
    {
        switch (stream)
        {
            case null:
                return Array.Empty<byte>();
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
}