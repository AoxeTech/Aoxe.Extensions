namespace Aoxe.Extensions;

/// <summary>
/// Provides extension methods for byte array manipulation and conversion.
/// </summary>
public static partial class AoxeExtension
{
    /// <summary>
    /// Creates a deep copy of the source byte array.
    /// </summary>
    /// <param name="src">The source byte array to clone. Cannot be null.</param>
    /// <returns>A new byte array containing the same values as the source array.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the source array is null.</exception>
    public static byte[] CloneNew(this byte[] src)
    {
        if (src == null)
            throw new ArgumentNullException(nameof(src));

        var dest = new byte[src.Length];
        Array.Copy(src, dest, src.Length);
        return dest;
    }

    /// <summary>
    /// Converts each byte to two 4-bit nibbles (high and low) stored in separate bytes.
    /// </summary>
    /// <param name="src">The source byte array to convert. Cannot be null.</param>
    /// <returns>
    /// A new byte array where each original byte is split into two bytes:
    /// - First byte contains the high nibble (0-15)
    /// - Second byte contains the low nibble (0-15)
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when the source array is null.</exception>
    public static byte[] ToHex(this byte[] src)
    {
        if (src == null)
            throw new ArgumentNullException(nameof(src));

        var dest = new byte[src.Length * 2];
        for (var i = 0; i < src.Length; i++)
        {
            var b = src[i];
            dest[i * 2] = (byte)(b >> 4); // High nibble
            dest[i * 2 + 1] = (byte)(b & 0xF); // Low nibble
        }
        return dest;
    }

    /// <summary>
    /// Reconstructs the original byte array from nibble pairs.
    /// </summary>
    /// <param name="dest">The nibble array to convert. Cannot be null, must have even length.</param>
    /// <returns>
    /// A new byte array where each pair of bytes is combined into a single byte:
    /// - First byte treated as high nibble (0-15)
    /// - Second byte treated as low nibble (0-15)
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when the input array is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the input array has odd length.</exception>
    public static byte[] FromHex(this byte[] dest)
    {
        if (dest == null)
            throw new ArgumentNullException(nameof(dest));
        if (dest.Length % 2 != 0)
            throw new ArgumentException(
                "Hex-encoded byte array must have even length.",
                nameof(dest)
            );

        var src = new byte[dest.Length / 2];
        for (var i = 0; i < src.Length; i++)
        {
            var hi = dest[i * 2];
            var lo = dest[i * 2 + 1];
            src[i] = (byte)((hi << 4) | lo); // Combine nibbles
        }
        return src;
    }
}
