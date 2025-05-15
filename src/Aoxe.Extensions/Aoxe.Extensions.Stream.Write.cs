namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    public static bool TryWrite(this Stream? stream, byte[] buffer)
    {
        if (stream is not { CanWrite: true })
            return false;
        try
        {
#if NETSTANDARD2_0
            stream.Write(buffer, 0, buffer.Length);
#else
            stream.Write(buffer);
#endif
            return true;
        }
        catch (Exception ex)
            when (ex is IOException or ObjectDisposedException or NotSupportedException)
        {
            return false;
        }
    }

    public static bool TryWrite(this Stream? stream, byte[] buffer, int offset, int count)
    {
        if (offset < 0 || count < 0 || offset + count > buffer.Length)
            throw new ArgumentException("Invalid offset or count");

        try
        {
            if (stream is not { CanWrite: true })
                return false;

            stream.Write(buffer, offset, count);
            return true;
        }
        catch (Exception ex)
            when (ex is IOException or ObjectDisposedException or NotSupportedException)
        {
            return false;
        }
    }

    public static bool TryWriteByte(this Stream? stream, byte value)
    {
        if (stream is null)
            return false;

        try
        {
            if (!stream.CanWrite)
                return false;

            stream.WriteByte(value);
            return true;
        }
        catch (Exception ex)
            when (ex is IOException or ObjectDisposedException or NotSupportedException)
        {
            return false;
        }
    }

    public static void Write(this Stream? stream, string? str, Encoding? encoding = null)
    {
        if (stream is null || str is null)
            return;

        var bytes = (encoding ?? Encoding.UTF8).GetBytes(str);
#if NETSTANDARD2_0
        stream.Write(bytes, 0, bytes.Length);
#else
        stream.Write(bytes);
#endif
    }

    public static bool TryWrite(this Stream? stream, string? str, Encoding? encoding = null)
    {
        if (stream is null || str is null)
            return false;
        try
        {
            var bytes = (encoding ?? Encoding.UTF8).GetBytes(str);
            return stream.TryWrite(bytes);
        }
        catch (Exception ex) when (ex is EncoderFallbackException or ArgumentException)
        {
            return false;
        }
    }
}
