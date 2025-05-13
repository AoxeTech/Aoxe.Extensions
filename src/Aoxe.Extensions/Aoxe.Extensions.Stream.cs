namespace Aoxe.Extensions;

/// <summary>
/// Provides extension methods for working with <see cref="Stream"/> objects.
/// </summary>
public static partial class AoxeExtension
{
    /// <summary>
    /// Determines whether a stream is null or empty.
    /// </summary>
    /// <param name="stream">The stream to check.</param>
    /// <returns>
    /// <c>true</c> if the stream is null, not seekable, or has zero length; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// For non-seekable streams, always returns <c>false</c> since length cannot be determined.
    /// </remarks>
    public static bool IsNullOrEmpty(this Stream? stream) =>
        stream is null or { CanSeek: true, Length: 0 };

    /// <summary>
    /// Attempts to seek within a stream without throwing exceptions.
    /// </summary>
    /// <param name="stream">The stream to seek within.</param>
    /// <param name="offset">The seek offset.</param>
    /// <param name="seekOrigin">The seek origin point.</param>
    /// <returns>
    /// The new position if successful; otherwise, -1.
    /// </returns>
    public static long TrySeek(this Stream? stream, long offset, SeekOrigin seekOrigin) =>
        stream?.CanSeek == true ? stream.Seek(offset, seekOrigin) : -1;

    /// <summary>
    /// Copies the stream's contents to a new <see cref="MemoryStream"/>.
    /// </summary>
    /// <param name="stream">The source stream to copy.</param>
    /// <returns>A new <see cref="MemoryStream"/> containing the stream data.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="stream"/> is null.</exception>
    /// <exception cref="NotSupportedException">Thrown when the stream is not readable.</exception>
    /// <remarks>
    /// Preserves the original stream's position if seekable.
    /// </remarks>
    public static MemoryStream ToMemoryStream(this Stream stream)
    {
        if (stream is null)
            throw new ArgumentNullException(nameof(stream));
        if (!stream.CanRead)
            throw new NotSupportedException("Stream is not readable");

        var originalPosition = stream.CanSeek ? stream.Position : 0;
        var memoryStream = new MemoryStream();

        try
        {
            if (stream.CanSeek)
                stream.Position = 0;

            stream.CopyTo(memoryStream);
        }
        finally
        {
            if (stream.CanSeek)
                stream.Position = originalPosition;
        }

        memoryStream.Position = 0;
        return memoryStream;
    }

    /// <summary>
    /// Reads all bytes from the stream into a <see cref="ReadOnlyMemory{T}"/>.
    /// </summary>
    public static ReadOnlyMemory<byte> ToReadOnlyMemory(this Stream stream) =>
        stream.ReadToEnd().AsMemory();

    /// <summary>
    /// Reads all bytes from the stream into a <see cref="ReadOnlySequence{T}"/>.
    /// </summary>
    public static ReadOnlySequence<byte> ToReadOnlySequence(this Stream stream) =>
        new(stream.ReadToEnd());
}
