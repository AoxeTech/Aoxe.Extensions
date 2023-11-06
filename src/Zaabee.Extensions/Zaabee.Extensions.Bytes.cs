namespace Zaabee.Extensions;

public static partial class ZaabeeExtension
{
    public static T[] Copy<T>(this T[] src)
    {
        var dest = new T[src.Length];
        for (var i = 0; i < src.Length; i++) dest[i] = src[i];
        return dest;
    }
}