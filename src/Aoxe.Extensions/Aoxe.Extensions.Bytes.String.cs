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
}
