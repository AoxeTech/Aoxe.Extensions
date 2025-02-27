namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    /// <summary>
    /// Converts a string to its Base64 representation
    /// </summary>
    /// <param name="value">Input string to encode</param>
    /// <param name="encoding">Text encoding to use (default: UTF-8)</param>
    /// <returns>Base64 encoded string</returns>
    /// <exception cref="ArgumentNullException">Thrown when input is null</exception>
    public static string ToBase64String(this string value, Encoding? encoding = null)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));

        var bytes = (encoding ?? Encoding.UTF8).GetBytes(value);
        return Convert.ToBase64String(bytes);
    }

    /// <summary>
    /// Converts a string to Base64 and returns the ASCII-encoded bytes of the Base64 string
    /// </summary>
    /// <param name="value">Input string to encode</param>
    /// <param name="encoding">Text encoding to use for initial conversion (default: UTF-8)</param>
    /// <returns>ASCII bytes of the Base64 representation</returns>
    public static byte[] ToBase64Bytes(this string value, Encoding? encoding = null)
    {
        var base64String = value.ToBase64String(encoding);
        return Encoding.ASCII.GetBytes(base64String);
    }

    /// <summary>
    /// Converts a Base64 string to its raw byte representation
    /// </summary>
    /// <param name="base64String">Base64 encoded string</param>
    /// <returns>Decoded byte array</returns>
    /// <exception cref="ArgumentNullException">Thrown when input is null</exception>
    /// <exception cref="FormatException">Thrown when input is not valid Base64</exception>
    public static byte[] FromBase64ToBytes(this string base64String)
    {
        if (base64String is null)
            throw new ArgumentNullException(nameof(base64String));

        try
        {
            return Convert.FromBase64String(base64String);
        }
        catch (FormatException ex)
        {
            throw new FormatException("Invalid Base64 format", ex);
        }
    }

    /// <summary>
    /// Decodes a Base64 string to its original string representation
    /// </summary>
    /// <param name="base64String">Base64 encoded string</param>
    /// <param name="encoding">Original text encoding used (default: UTF-8)</param>
    /// <returns>Decoded string</returns>
    public static string DecodeBase64(this string base64String, Encoding? encoding = null)
    {
        var bytes = base64String.FromBase64ToBytes();
        return (encoding ?? Encoding.UTF8).GetString(bytes);
    }
}
