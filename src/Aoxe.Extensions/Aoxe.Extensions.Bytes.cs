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
}
