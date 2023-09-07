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

    public static byte[] FromHexString(this string hexString)
    {
#if NETSTANDARD2_0
        var numberChars = hexString.Length;
        var bytes = new byte[numberChars / 2];
        for (var i = 0; i < numberChars; i += 2)
            bytes[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
        return bytes;
#else
        return Convert.FromHexString(hexString);
#endif
    }
}