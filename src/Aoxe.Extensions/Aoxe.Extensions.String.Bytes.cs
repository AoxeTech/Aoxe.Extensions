namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    public static byte[] GetUtf8Bytes(this string value)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));
        return Encoding.UTF8.GetBytes(value);
    }

    public static byte[] GetAsciiBytes(this string value)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));
        return Encoding.ASCII.GetBytes(value);
    }

    public static byte[] GetBigEndianUnicodeBytes(this string value)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));
        return Encoding.BigEndianUnicode.GetBytes(value);
    }

    public static byte[] GetDefaultBytes(this string value)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));
        return Encoding.Default.GetBytes(value);
    }

    public static byte[] GetUtf32Bytes(this string value)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));
        return Encoding.UTF32.GetBytes(value);
    }

    public static byte[] GetUnicodeBytes(this string value)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));
        return Encoding.Unicode.GetBytes(value);
    }

    public static byte[] GetBytes(this string value, Encoding? encoding = null)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));
        return (encoding ?? Utf8Encoding).GetBytes(value);
    }
}
