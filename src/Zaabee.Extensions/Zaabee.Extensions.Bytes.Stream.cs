namespace Zaabee.Extensions;

public static partial class ZaabeeExtension
{
    public static void WriteTo(this byte[] buffer, Stream stream) =>
#if NETSTANDARD2_0
        stream.Write(buffer, 0, buffer.Length);
#else
        stream.Write(buffer);
#endif

    public static bool TryWriteTo(this byte[] buffer, Stream stream) =>
        stream.TryWrite(buffer, 0, buffer.Length);
}