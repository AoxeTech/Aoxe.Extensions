namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    public static MemoryStream ToMemoryStream(this byte[] bytes) =>
        new(bytes ?? throw new ArgumentNullException(nameof(bytes)));

    public static void WriteTo(this byte[] bytes, Stream stream)
    {
        if (bytes is null)
            throw new ArgumentNullException(nameof(bytes));
        if (stream is null)
            throw new ArgumentNullException(nameof(stream));
#if NETSTANDARD2_0
        stream.Write(bytes, 0, bytes.Length);
#else
        stream.Write(bytes);
#endif
    }

    public static bool TryWriteTo(this byte[] bytes, Stream stream)
    {
        if (bytes is null)
            throw new ArgumentNullException(nameof(bytes));
        if (stream is null)
            throw new ArgumentNullException(nameof(stream));
        try
        {
            bytes.WriteTo(stream);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
