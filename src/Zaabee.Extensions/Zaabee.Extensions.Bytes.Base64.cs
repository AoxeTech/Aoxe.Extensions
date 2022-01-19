namespace Zaabee.Extensions;

public static partial class ZaabeeExtension
{
    public static string ToBase64String(this byte[] bytes) =>
        Convert.ToBase64String(bytes);

    public static byte[] ToBase64Bytes(this byte[] bytes, Encoding? encoding = null) =>
        bytes.ToBase64String().GetBytes(encoding);

    public static byte[] DecodeBase64ToBytes(this byte[] bytes, Encoding? encoding = null) =>
        Convert.FromBase64String(bytes.GetString(encoding));

    public static string DecodeBase64ToString(this byte[] bytes, Encoding? encoding = null) =>
        Convert.FromBase64String(bytes.GetString(encoding)).GetString(encoding);
}