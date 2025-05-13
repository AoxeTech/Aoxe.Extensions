namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    #region ToHex
    public static string ToHexString(this string input, Encoding? encoding = null)
    {
        encoding ??= Encoding.UTF8;
        return encoding.GetBytes(input).ToHexString();
    }

    public static string ToHexString(this byte[] bytes)
    {
        if (bytes is null)
            throw new ArgumentNullException(nameof(bytes));
        return BitConverter.ToString(bytes).Replace("-", "");
    }

    public static byte[] ToHexBytes(this string input, Encoding? encoding = null)
    {
        encoding ??= Encoding.UTF8;
        return encoding.GetBytes(input).ToHexBytes();
    }

    public static byte[] ToHexBytes(this byte[] bytes)
    {
        if (bytes is null)
            throw new ArgumentNullException(nameof(bytes));
        return Encoding.ASCII.GetBytes(bytes.ToHexString());
    }

    #endregion

    #region FromHex

    public static string FromHexToString(this string hexString, Encoding? encoding = null)
    {
        encoding ??= Encoding.UTF8;
        return encoding.GetString(hexString.FromHexToBytes());
    }

    public static byte[] FromHexToBytes(this string hexString)
    {
        if (hexString is null)
            throw new ArgumentNullException(nameof(hexString));
        ValidateHexFormat(hexString);

        byte[] bytes = new byte[hexString.Length / 2];
        for (int i = 0; i < bytes.Length; i++)
        {
            bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
        }
        return bytes;
    }

    public static string FromHexToString(this byte[] hexBytes, Encoding? encoding = null)
    {
        encoding ??= Encoding.UTF8;
        return encoding.GetString(hexBytes.FromHexToBytes());
    }

    public static byte[] FromHexToBytes(this byte[] hexBytes)
    {
        if (hexBytes is null)
            throw new ArgumentNullException(nameof(hexBytes));
        string hexString = Encoding.ASCII.GetString(hexBytes);
        return hexString.FromHexToBytes();
    }

    #endregion

    private static void ValidateHexFormat(string hexString)
    {
        if (hexString.Length % 2 != 0)
            throw new FormatException("Hexadecimal string length must be even.");

        if (hexString.Any(c => !IsHexChar(c)))
            throw new FormatException("Invalid hexadecimal characters detected.");
    }

    private static bool IsHexChar(char c) =>
        c is >= '0' and <= '9' or >= 'A' and <= 'F' or >= 'a' and <= 'f';
}
