namespace Zaabee.Extensions;

public static partial class ZaabeeExtension
{
    public static byte[] GetUtf8Bytes(this string value) =>
        Encoding.UTF8.GetBytes(value);

    public static byte[] GetAsciiBytes(this string value) =>
        Encoding.ASCII.GetBytes(value);

    public static byte[] GetBigEndianUnicodeBytes(this string value) =>
        Encoding.BigEndianUnicode.GetBytes(value);

    public static byte[] GetDefaultBytes(this string value) =>
        Encoding.Default.GetBytes(value);

    public static byte[] GetUtf32Bytes(this string value) =>
        Encoding.UTF32.GetBytes(value);

    public static byte[] GetUnicodeBytes(this string value) =>
        Encoding.Unicode.GetBytes(value);

    public static byte[] GetBytes(this string value, Encoding? encoding = null) =>
        (encoding ?? Utf8Encoding).GetBytes(value);
}