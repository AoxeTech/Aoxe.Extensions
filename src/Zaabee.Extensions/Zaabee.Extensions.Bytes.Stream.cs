namespace Zaabee.Extensions;

public static partial class ZaabeeExtension
{
    public static MemoryStream ToStream(this byte[] buffer)
    {
        var ms = new MemoryStream();
        buffer.WriteTo(ms);
        ms.Seek(0, SeekOrigin.Begin);
        return ms;
    }

    public static MemoryStream TryToStream(this byte[] buffer)
    {
        var ms = new MemoryStream();
        buffer.TryWriteTo(ms);
        ms.Seek(0, SeekOrigin.Begin);
        return ms;
    }

    public static void WriteTo(this byte[] buffer, Stream stream) =>
        stream.Write(buffer, 0, buffer.Length);

    public static bool TryWriteTo(this byte[] buffer, Stream stream) =>
        stream.TryWrite(buffer, 0, buffer.Length);
}