namespace Zaabee.Extensions;

public static partial class ZaabeeExtension
{
    public static sbyte ParseSbyte(this string s) =>
        sbyte.Parse(s);

    public static byte ParseByte(this string s) =>
        byte.Parse(s);

    public static short ParseShort(this string s) =>
        short.Parse(s);

    public static ushort ParseUshort(this string s) =>
        ushort.Parse(s);

    public static int ParseInt(this string s) =>
        int.Parse(s);

    public static uint ParseUint(this string s) =>
        uint.Parse(s);

    public static long ParseLong(this string s) =>
        long.Parse(s);

    public static ulong ParseUlong(this string s) =>
        ulong.Parse(s);

    public static float ParseFloat(this string s) =>
        float.Parse(s);

    public static double ParseDouble(this string s) =>
        double.Parse(s);

    public static decimal ParseDecimal(this string s) =>
        decimal.Parse(s);

    public static bool ParseBool(this string s) =>
        bool.Parse(s);

    public static DateTime ParseDateTime(this string s) =>
        DateTime.Parse(s);

    public static DateTimeOffset ParseDateTimeOffset(this string s) =>
        DateTimeOffset.Parse(s);

    public static object ParseEnum(this string value, Type enumType, bool ignoreCase = false) =>
        Enum.Parse(enumType, value, ignoreCase);
}