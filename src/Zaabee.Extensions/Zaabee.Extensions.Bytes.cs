namespace Zaabee.Extensions;

public static partial class ZaabeeExtension
{
    public static byte[] CloneNew(this byte[] src)
    {
        var dest = new byte[src.Length];
        for (var i = 0; i < src.Length; i++) dest[i] = src[i];
        return dest;
    }
}