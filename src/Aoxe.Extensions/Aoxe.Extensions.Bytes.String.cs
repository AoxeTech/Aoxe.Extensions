namespace Aoxe.Extensions;

/// <summary>
/// Provides encoding-related extension methods for byte arrays.
/// </summary>
public static partial class AoxeExtension
{
    /// <summary>
    /// Converts a byte array to a string using UTF-8 encoding.
    /// </summary>
    public static string GetStringByUtf8(this byte[] bytes) => bytes.GetString(Encoding.UTF8);

    /// <summary>
    /// Converts a byte array to a string using ASCII encoding.
    /// </summary>
    public static string GetStringByAscii(this byte[] bytes) => bytes.GetString(Encoding.ASCII);

    /// <summary>
    /// Converts a byte array to a string using Big-Endian Unicode encoding.
    /// </summary>
    public static string GetStringByBigEndianUnicode(this byte[] bytes) =>
        bytes.GetString(Encoding.BigEndianUnicode);

    /// <summary>
    /// Converts a byte array to a string using the system's default encoding.
    /// </summary>
    public static string GetStringByDefault(this byte[] bytes) => bytes.GetString(Encoding.Default);

    /// <summary>
    /// Converts a byte array to a string using UTF-32 encoding.
    /// </summary>
    public static string GetStringByUtf32(this byte[] bytes) => bytes.GetString(Encoding.UTF32);

    /// <summary>
    /// Converts a byte array to a string using Unicode (UTF-16) encoding.
    /// </summary>
    public static string GetStringByUnicode(this byte[] bytes) => bytes.GetString(Encoding.Unicode);

    /// <summary>
    /// Converts a byte array to a string using the specified encoding.
    /// </summary>
    /// <param name="bytes">The byte array to convert.</param>
    /// <param name="encoding">The encoding to use (defaults to UTF-8 if null).</param>
    /// <returns>The decoded string.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="bytes"/> is null.</exception>
    public static string GetString(this byte[] bytes, Encoding? encoding = null)
    {
        if (bytes == null)
            throw new ArgumentNullException(nameof(bytes));
        return (encoding ?? Encoding.UTF8).GetString(bytes);
    }

    /// <summary>
    /// Converts a byte array to a hexadecimal string representation.
    /// </summary>
    /// <param name="hash">The byte array to convert.</param>
    /// <returns>A hexadecimal string.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="hash"/> is null.</exception>
    public static string ToHexString(this byte[] hash)
    {
        if (hash == null)
            throw new ArgumentNullException(nameof(hash));

#if NETSTANDARD2_0
        var hex = new StringBuilder(hash.Length * 2);
        foreach (var b in hash)
            hex.Append(b.ToString("X2"));
        return hex.ToString();
#else
        return Convert.ToHexString(hash);
#endif
    }

    /// <summary>
    /// Converts a byte array containing hexadecimal data to a string.
    /// </summary>
    /// <param name="hexBytes">Byte array containing hexadecimal data.</param>
    /// <returns>The decoded string.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="hexBytes"/> is null.</exception>
    public static string FromHexToString(this byte[] hexBytes)
    {
        if (hexBytes == null)
            throw new ArgumentNullException(nameof(hexBytes));
        return new string(hexBytes.FromHex().Select(c => (char)c).ToArray());
    }

    /// <summary>
    /// Converts a hexadecimal string to its corresponding byte array representation.
    /// </summary>
    /// <param name="hexString">The hexadecimal string to convert.</param>
    /// <returns>A byte array containing the values represented by the hexadecimal string.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="hexString"/> is null.</exception>
    /// <exception cref="ArgumentException">
    /// Thrown when:
    /// <list type="bullet">
    /// <item><description>The input string has an odd number of characters</description></item>
    /// <item><description>The input contains non-hexadecimal characters</description></item>
    /// </list>
    /// </exception>
    /// <remarks>
    /// <para>This method is case-insensitive and supports both uppercase and lowercase hex digits.</para>
    /// <para>Each pair of characters in the input string represents a single byte (00-FF).</para>
    /// <para>Example conversions:</para>
    /// <example>
    /// "01AB" => new byte[] { 0x01, 0xAB }
    /// "ff"   => new byte[] { 0xFF }
    /// ""     => Array.Empty&lt;byte&gt;()
    /// </example>
    /// </remarks>
    public static byte[] ToHexBytes(this string hexString)
    {
        if (hexString == null)
            throw new ArgumentNullException(nameof(hexString));
        if (hexString.Length % 2 != 0)
            throw new ArgumentException(
                "Hexadecimal string must have even length.",
                nameof(hexString)
            );

        byte[] result = new byte[hexString.Length / 2];

        for (int i = 0; i < result.Length; i++)
        {
            string byteSegment = hexString.Substring(i * 2, 2);
            try
            {
                result[i] = Convert.ToByte(byteSegment, 16);
            }
            catch (FormatException ex)
            {
                throw new ArgumentException(
                    $"Invalid hexadecimal character(s) in segment '{byteSegment}' at position {i * 2}.",
                    nameof(hexString),
                    ex
                );
            }
        }

        return result;
    }
}
