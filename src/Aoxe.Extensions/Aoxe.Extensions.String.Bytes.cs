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

    /// <summary>
    /// Converts a hex string to a byte array
    /// </summary>
    /// <exception cref="FormatException">Thrown for invalid hex characters or odd-length input</exception>
    public static byte[] FromHex(this string hexString)
    {
        if (hexString == null)
            throw new ArgumentNullException(nameof(hexString));

#if NETSTANDARD2_0
        if (hexString.Length % 2 != 0)
            throw new FormatException("Hex string must have even length");

        var bytes = new byte[hexString.Length / 2];
        for (var i = 0; i < hexString.Length; i += 2)
        {
            var c1 = hexString[i];
            var c2 = hexString[i + 1];

            if (!IsHexChar(c1) || !IsHexChar(c2))
                throw new FormatException($"Invalid hex characters at position {i}");

            bytes[i / 2] = (byte)((GetHexValue(c1) << 4) | GetHexValue(c2));
        }
        return bytes;
#else
        try
        {
            return Convert.FromHexString(hexString);
        }
        catch (FormatException ex)
        {
            throw new FormatException("Invalid hex format: " + ex.Message, ex);
        }
#endif
    }

    /// <summary>
    /// Converts the string to a hex-encoded byte array (ASCII representation)
    /// </summary>
    public static byte[] ToHexBytes(this string str, Encoding? encoding = null)
    {
        var bytes = (encoding ?? Utf8Encoding).GetBytes(
            str ?? throw new ArgumentNullException(nameof(str))
        );
        var hexChars = new char[bytes.Length * 2];

        for (var i = 0; i < bytes.Length; i++)
        {
            var b = bytes[i];
            hexChars[i * 2] = ToHexChar(b >> 4);
            hexChars[i * 2 + 1] = ToHexChar(b & 0xF);
        }

        return Encoding.ASCII.GetBytes(hexChars);
    }

#if NETSTANDARD2_0
    private static bool IsHexChar(char c) =>
        c is >= '0' and <= '9' or >= 'A' and <= 'F' or >= 'a' and <= 'f';

    private static int GetHexValue(char c) =>
        c switch
        {
            >= '0' and <= '9' => c - '0',
            >= 'A' and <= 'F' => c - 'A' + 10,
            >= 'a' and <= 'f' => c - 'a' + 10,
            _ => throw new ArgumentOutOfRangeException(nameof(c))
        };
#endif

    private static char ToHexChar(int value) => (char)(value < 10 ? value + '0' : value - 10 + 'A');
}
