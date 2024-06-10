namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    public static byte[] GetUtf8Bytes(this string value) => Encoding.UTF8.GetBytes(value);

    public static byte[] GetAsciiBytes(this string value) => Encoding.ASCII.GetBytes(value);

    public static byte[] GetBigEndianUnicodeBytes(this string value) =>
        Encoding.BigEndianUnicode.GetBytes(value);

    public static byte[] GetDefaultBytes(this string value) => Encoding.Default.GetBytes(value);

    public static byte[] GetUtf32Bytes(this string value) => Encoding.UTF32.GetBytes(value);

    public static byte[] GetUnicodeBytes(this string value) => Encoding.Unicode.GetBytes(value);

    public static byte[] GetBytes(this string value, Encoding? encoding = null) =>
        (encoding ?? Utf8Encoding).GetBytes(value);

    public static byte[] FromHex(this string hexString)
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

    public static byte[] ToHex(this string str)
    {
        var bytes = str.GetBytes();
        var dest = new byte[bytes.Length * 2];
        for (var i = 0; i < bytes.Length; i++)
        {
            var b = bytes[i];
            dest[i * 2] = (byte)(b / 16);
            dest[i * 2 + 1] = (byte)(b % 16);
        }
        return dest;
    }
}
