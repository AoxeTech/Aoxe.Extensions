namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    public static string ToBase64String(this string value, Encoding? encoding = null) =>
        Convert.ToBase64String((encoding ?? Encoding.UTF8).GetBytes(value));

    public static byte[] ToBase64Bytes(this string value, Encoding? encoding = null) =>
        Encoding.ASCII.GetBytes(value.ToBase64String(encoding));

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

    public static string DecodeBase64(this string base64String, Encoding? encoding = null)
    {
        var bytes = base64String.FromBase64ToBytes();
        return (encoding ?? Encoding.UTF8).GetString(bytes);
    }
}
