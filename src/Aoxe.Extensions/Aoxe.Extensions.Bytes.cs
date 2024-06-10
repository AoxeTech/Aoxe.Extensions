namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    public static byte[] CloneNew(this byte[] src)
    {
        var dest = new byte[src.Length];
        for (var i = 0; i < src.Length; i++)
            dest[i] = src[i];
        return dest;
    }

    public static byte[] ToHex(this byte[] src)
    {
        var dest = new byte[src.Length * 2];
        for (var i = 0; i < src.Length; i++)
        {
            var b = src[i];
            dest[i * 2] = (byte)(b / 16);
            dest[i * 2 + 1] = (byte)(b % 16);
        }
        return dest;
    }

    public static byte[] FromHex(this byte[] dest)
    {
        var src = new byte[dest.Length / 2];
        for (var i = 0; i < src.Length; i++)
        {
            var hi = dest[i * 2];
            var lo = dest[i * 2 + 1];
            src[i] = (byte)(hi * 16 + lo);
        }
        return src;
    }
}
