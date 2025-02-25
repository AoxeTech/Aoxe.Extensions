namespace Aoxe.Extensions;

/// <summary>
/// Provides extension methods for stream operations.
/// </summary>
public static partial class AoxeExtension
{
    /// <summary>
    /// Creates a new MemoryStream containing the byte array data.
    /// </summary>
    /// <param name="bytes">The byte array to wrap in a MemoryStream.</param>
    /// <returns>A MemoryStream containing the byte array data.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="bytes"/> is null.</exception>
    public static MemoryStream ToMemoryStream(this byte[] bytes)
    {
        if (bytes == null)
            throw new ArgumentNullException(nameof(bytes));
        return new MemoryStream(bytes);
    }

    /// <summary>
    /// Writes the entire byte array to a stream.
    /// </summary>
    /// <param name="buffer">The byte array to write.</param>
    /// <param name="stream">The target stream to write to.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="buffer"/> or <paramref name="stream"/> is null.</exception>
    public static void WriteTo(this byte[] buffer, Stream stream)
    {
        if (buffer == null)
            throw new ArgumentNullException(nameof(buffer));
        if (stream == null)
            throw new ArgumentNullException(nameof(stream));

#if NETSTANDARD2_0
        stream.Write(buffer, 0, buffer.Length);
#else
        stream.Write(buffer);
#endif
    }

    /// <summary>
    /// Attempts to write the entire byte array to a stream.
    /// </summary>
    /// <param name="buffer">The byte array to write.</param>
    /// <param name="stream">The target stream to write to.</param>
    /// <returns>true if write succeeded; false if any exception occurred.</returns>
    public static bool TryWriteTo(this byte[] buffer, Stream stream)
    {
        try
        {
            buffer.WriteTo(stream);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
