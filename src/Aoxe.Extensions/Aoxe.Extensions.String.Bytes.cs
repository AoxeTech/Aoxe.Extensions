namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    /// <summary>
    /// Converts the string to a UTF-8 encoded byte array
    /// </summary>
    public static byte[] GetUtf8Bytes(this string value) =>
        Encoding.UTF8.GetBytes(value ?? throw new ArgumentNullException(nameof(value)));

    /// <summary>
    /// Converts the string to an ASCII encoded byte array
    /// </summary>
    public static byte[] GetAsciiBytes(this string value) =>
        Encoding.ASCII.GetBytes(value ?? throw new ArgumentNullException(nameof(value)));

    /// <summary>
    /// Converts the string to a Big-Endian Unicode (UTF-16BE) encoded byte array
    /// </summary>
    public static byte[] GetBigEndianUnicodeBytes(this string value) =>
        Encoding.BigEndianUnicode.GetBytes(value ?? throw new ArgumentNullException(nameof(value)));

    /// <summary>
    /// Converts the string using the system's default ANSI encoding
    /// </summary>
    public static byte[] GetDefaultBytes(this string value) =>
        Encoding.Default.GetBytes(value ?? throw new ArgumentNullException(nameof(value)));

    /// <summary>
    /// Converts the string to a UTF-32 encoded byte array
    /// </summary>
    public static byte[] GetUtf32Bytes(this string value) =>
        Encoding.UTF32.GetBytes(value ?? throw new ArgumentNullException(nameof(value)));

    /// <summary>
    /// Converts the string to a Unicode (UTF-16LE) encoded byte array
    /// </summary>
    public static byte[] GetUnicodeBytes(this string value) =>
        Encoding.Unicode.GetBytes(value ?? throw new ArgumentNullException(nameof(value)));

    /// <summary>
    /// Converts the string using the specified encoding (UTF-8 by default)
    /// </summary>
    public static byte[] GetBytes(this string value, Encoding? encoding = null) =>
        (encoding ?? Utf8Encoding).GetBytes(
            value ?? throw new ArgumentNullException(nameof(value))
        );
}
