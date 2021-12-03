namespace Zaabee.Extensions;

public static partial class ZaabeeExtension
{
    public static byte[] ToUtf8Bytes(this string value) =>
        Encoding.UTF8.GetBytes(value);

    public static byte[] ToAsciiBytes(this string value) =>
        Encoding.ASCII.GetBytes(value);

    public static byte[] ToBigEndianUnicodeBytes(this string value) =>
        Encoding.BigEndianUnicode.GetBytes(value);

    public static byte[] ToDefaultBytes(this string value) =>
        Encoding.Default.GetBytes(value);

    public static byte[] ToUtf32Bytes(this string value) =>
        Encoding.UTF32.GetBytes(value);

    public static byte[] ToUnicodeBytes(this string value) =>
        Encoding.Unicode.GetBytes(value);

    public static byte[] ToBytes(this string value, Encoding? encoding = null) =>
        (encoding ?? Utf8Encoding).GetBytes(value);
}