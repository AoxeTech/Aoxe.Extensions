namespace Aoxe.Extensions;

/// <summary>
/// Provides extension methods for byte array manipulation and conversions.
/// </summary>
public static partial class AoxeExtension
{
    /// <summary>
    /// Converts a byte array to its Base64 string representation.
    /// </summary>
    /// <param name="bytes">The byte array to convert.</param>
    /// <returns>A Base64 encoded string.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="bytes"/> is null.</exception>
    public static string ToBase64String(this byte[] bytes)
    {
        if (bytes == null)
            throw new ArgumentNullException(nameof(bytes));
        return Convert.ToBase64String(bytes);
    }

    /// <summary>
    /// Converts a byte array to a Base64 encoded byte array using the specified encoding.
    /// </summary>
    /// <param name="bytes">The byte array to convert.</param>
    /// <param name="encoding">The encoding to use (defaults to UTF-8 if null).</param>
    /// <returns>A byte array containing the Base64 encoded data.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="bytes"/> is null.</exception>
    public static byte[] ToBase64Bytes(this byte[] bytes, Encoding? encoding = null)
    {
        if (bytes == null)
            throw new ArgumentNullException(nameof(bytes));
        return bytes.ToBase64String().GetBytes(encoding);
    }

    /// <summary>
    /// Decodes a Base64 encoded byte array back to its original byte representation.
    /// </summary>
    /// <param name="bytes">The Base64 encoded byte array.</param>
    /// <param name="encoding">The encoding used for the Base64 string (defaults to UTF-8 if null).</param>
    /// <returns>The decoded byte array.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="bytes"/> is null.</exception>
    /// <exception cref="FormatException">Thrown when the input is not valid Base64 data.</exception>
    public static byte[] DecodeBase64ToBytes(this byte[] bytes, Encoding? encoding = null)
    {
        if (bytes == null)
            throw new ArgumentNullException(nameof(bytes));
        return Convert.FromBase64String(bytes.GetString(encoding));
    }

    /// <summary>
    /// Decodes a Base64 encoded byte array to a string using the specified encoding.
    /// </summary>
    /// <param name="bytes">The Base64 encoded byte array.</param>
    /// <param name="encoding">The encoding to use for decoding (defaults to UTF-8 if null).</param>
    /// <returns>The decoded string.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="bytes"/> is null.</exception>
    /// <exception cref="FormatException">Thrown when the input is not valid Base64 data.</exception>
    public static string DecodeBase64ToString(this byte[] bytes, Encoding? encoding = null)
    {
        if (bytes == null)
            throw new ArgumentNullException(nameof(bytes));
        return Convert.FromBase64String(bytes.GetString(encoding)).GetString(encoding);
    }
}
