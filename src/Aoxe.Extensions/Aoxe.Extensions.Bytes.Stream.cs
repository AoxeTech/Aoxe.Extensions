namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    public static MemoryStream ToMemoryStream(this byte[] bytes) =>
        new(bytes ?? throw new ArgumentNullException(nameof(bytes)));

    public static void WriteTo(this byte[] bytes, Stream stream) =>
#if NETSTANDARD2_0
        stream.Write(bytes ?? throw new ArgumentNullException(nameof(bytes)), 0, bytes.Length);
#else
        stream.Write(bytes ?? throw new ArgumentNullException(nameof(bytes)));
#endif

    public static bool TryWriteTo(this byte[] bytes, Stream stream)
    {
        try
        {
            (bytes ?? throw new ArgumentNullException(nameof(bytes))).WriteTo(stream);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
