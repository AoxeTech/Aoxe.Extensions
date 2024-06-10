namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    public static MemoryStream ToMemoryStream(this string str, Encoding? encoding = null) =>
        new(str.GetBytes(encoding));

    public static void WriteTo(this string str, Stream stream, Encoding? encoding = null)
    {
        var buffer = str.GetBytes(encoding);
#if NETSTANDARD2_0
        stream.Write(buffer, 0, buffer.Length);
#else
        stream.Write(buffer);
#endif
    }

    public static bool TryWriteTo(this string str, Stream stream, Encoding? encoding = null)
    {
        var buffer = str.GetBytes(encoding);
        return stream.TryWrite(buffer, 0, buffer.Length);
    }
}
