namespace Zaabee.Extensions;

public static partial class ZaabeeExtension
{
    public static string GetStringByUtf8(this byte[] bytes) =>
        bytes.GetString(Encoding.UTF8);

    public static string GetStringByAscii(this byte[] bytes) =>
        bytes.GetString(Encoding.ASCII);

    public static string GetStringByBigEndianUnicode(this byte[] bytes) =>
        bytes.GetString(Encoding.BigEndianUnicode);

    public static string GetStringByDefault(this byte[] bytes) =>
        bytes.GetString(Encoding.Default);

    public static string GetStringByUtf32(this byte[] bytes) =>
        bytes.GetString(Encoding.UTF32);

    public static string GetStringByUnicode(this byte[] bytes) =>
        bytes.GetString(Encoding.Unicode);

    public static string GetString(this byte[] bytes, Encoding? encoding = null) =>
        (encoding ?? Encoding.UTF8).GetString(bytes);
}