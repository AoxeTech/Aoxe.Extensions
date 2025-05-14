namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    public static MemoryStream ToMemoryStream(this string str, Encoding? encoding = null) =>
        new MemoryStream((encoding ?? Encoding.UTF8).GetBytes(str));

    public static void WriteTo(this string str, Stream stream, Encoding? encoding = null)
    {
        if (!stream.CanWrite)
            throw new InvalidOperationException("Stream is not writable");

        var buffer = (encoding ?? Encoding.UTF8).GetBytes(str);

#if NETSTANDARD2_0
        stream.Write(buffer, 0, buffer.Length);
#else
        stream.Write(buffer);
#endif
    }

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
