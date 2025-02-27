namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    /// <summary>
    /// Creates a MemoryStream containing the string data in the specified encoding
    /// </summary>
    /// <param name="str">The string to convert</param>
    /// <param name="encoding">Text encoding to use (default: UTF-8)</param>
    /// <returns>MemoryStream containing the encoded string data</returns>
    public static MemoryStream ToMemoryStream(this string str, Encoding? encoding = null)
    {
        var bytes = (encoding ?? Encoding.UTF8).GetBytes(str);
        return new MemoryStream(bytes);
    }

    /// <summary>
    /// Writes the string to a stream using the specified encoding
    /// </summary>
    /// <param name="str">The string to write</param>
    /// <param name="stream">Target stream to write to</param>
    /// <param name="encoding">Text encoding to use (default: UTF-8)</param>
    /// <exception cref="ArgumentNullException">Thrown when stream is null</exception>
    /// <exception cref="InvalidOperationException">Thrown when stream is not writable</exception>
    public static void WriteTo(this string str, Stream stream, Encoding? encoding = null)
    {
        if (stream is null)
            throw new ArgumentNullException(nameof(stream));
        if (!stream.CanWrite)
            throw new InvalidOperationException("Stream is not writable");

        var buffer = (encoding ?? Encoding.UTF8).GetBytes(str);

#if NETSTANDARD2_0
        stream.Write(buffer, 0, buffer.Length);
#else
        stream.Write(buffer);
#endif
    }

    /// <summary>
    /// Attempts to write the string to a stream using the specified encoding
    /// </summary>
    /// <param name="str">The string to write</param>
    /// <param name="stream">Target stream to write to</param>
    /// <param name="encoding">Text encoding to use (default: UTF-8)</param>
    /// <returns>True if write succeeded, false if failed</returns>
    public static bool TryWriteTo(this string str, Stream? stream, Encoding? encoding = null)
    {
        try
        {
            if (stream is null || !stream.CanWrite)
                return false;

            var buffer = (encoding ?? Encoding.UTF8).GetBytes(str);

#if NETSTANDARD2_0
            stream.Write(buffer, 0, buffer.Length);
#else
            stream.Write(buffer);
#endif
            return true;
        }
        catch (Exception ex)
            when (ex
                    is IOException
                        or ObjectDisposedException
                        or NotSupportedException
                        or UnauthorizedAccessException
            )
        {
            return false;
        }
    }
}
