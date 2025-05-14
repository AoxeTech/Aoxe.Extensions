namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    public static byte[] CloneNew(this byte[] src)
    {
        var dest = new byte[(src ?? throw new ArgumentNullException(nameof(src))).Length];
        Array.Copy(src, dest, src.Length);
        return dest;
    }
}
