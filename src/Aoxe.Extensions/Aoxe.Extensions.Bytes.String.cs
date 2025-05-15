namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    public static string GetStringByUtf8(this byte[] bytes)
    {
        if (bytes is null)
            throw new ArgumentNullException(nameof(bytes));
        return bytes.GetString(Encoding.UTF8);
    }

    public static string GetStringByAscii(this byte[] bytes)
    {
        if (bytes is null)
            throw new ArgumentNullException(nameof(bytes));
        return bytes.GetString(Encoding.ASCII);
    }

    public static string GetStringByBigEndianUnicode(this byte[] bytes)
    {
        if (bytes is null)
            throw new ArgumentNullException(nameof(bytes));
        return bytes.GetString(Encoding.BigEndianUnicode);
    }

    public static string GetStringByDefault(this byte[] bytes)
    {
        if (bytes is null)
            throw new ArgumentNullException(nameof(bytes));
        return bytes.GetString(Encoding.Default);
    }

    public static string GetStringByUtf32(this byte[] bytes)
    {
        if (bytes is null)
            throw new ArgumentNullException(nameof(bytes));
        return bytes.GetString(Encoding.UTF32);
    }

    public static string GetStringByUnicode(this byte[] bytes)
    {
        if (bytes is null)
            throw new ArgumentNullException(nameof(bytes));
        return bytes.GetString(Encoding.Unicode);
    }

    public static string GetString(this byte[] bytes, Encoding? encoding = null)
    {
        if (bytes is null)
            throw new ArgumentNullException(nameof(bytes));
        return (encoding ?? Encoding.UTF8).GetString(bytes);
    }
}
