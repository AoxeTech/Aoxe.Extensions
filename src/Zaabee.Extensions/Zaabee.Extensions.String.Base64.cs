namespace Zaabee.Extensions;

public static partial class ZaabeeExtension
{
    public static string ToBase64String(this string s, Encoding? encoding = null) =>
        Convert.ToBase64String(s.GetBytes(encoding));

    public static byte[] ToBase64Bytes(this string s, Encoding? encoding = null) =>
        s.ToBase64String(encoding).GetBytes(encoding);

    public static byte[] FromBase64ToBytes(this string s) =>
        Convert.FromBase64String(s);

    public static string FromBase64ToString(this string s, Encoding? encoding = null) =>
        s.FromBase64ToBytes().GetString(encoding);
}