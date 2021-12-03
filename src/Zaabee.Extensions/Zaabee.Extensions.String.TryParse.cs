namespace Zaabee.Extensions;

public static partial class ZaabeeExtension
{
    public static sbyte TryParseSbyte(this string s, sbyte @default = default) =>
        sbyte.TryParse(s, out var result) ? result : @default;

    public static byte TryParseByte(this string s, byte @default = default) =>
        byte.TryParse(s, out var result) ? result : @default;

    public static short TryParseShort(this string s, short @default = default) =>
        short.TryParse(s, out var result) ? result : @default;

    public static ushort TryParseUshort(this string s, ushort @default = default) =>
        ushort.TryParse(s, out var result) ? result : @default;

    public static int TryParseInt(this string s, int @default = default) =>
        int.TryParse(s, out var result) ? result : @default;

    public static uint TryParseUint(this string s, uint @default = default) =>
        uint.TryParse(s, out var result) ? result : @default;

    public static long TryParseLong(this string s, long @default = default) =>
        long.TryParse(s, out var result) ? result : @default;

    public static ulong TryParseUlong(this string s, ulong @default = default) =>
        ulong.TryParse(s, out var result) ? result : @default;

    public static float TryParseFloat(this string s, float @default = default) =>
        float.TryParse(s, out var result) ? result : @default;

    public static double TryParseDouble(this string s, double @default = default) =>
        double.TryParse(s, out var result) ? result : @default;

    public static decimal TryParseDecimal(this string s, decimal @default = default) =>
        decimal.TryParse(s, out var result) ? result : @default;

    public static bool TryParseBool(this string s, bool @default = default) =>
        bool.TryParse(s, out var result) ? result : @default;

    public static DateTime TryParseDateTime(this string s, DateTime @default = default) =>
        DateTime.TryParse(s, out var result) ? result : @default;

    public static DateTimeOffset TryParseDateTimeOffset(this string s, DateTimeOffset @default = default) =>
        DateTimeOffset.TryParse(s, out var result) ? result : @default;

    public static TEnum TryParseEnum<TEnum>(this string? value, TEnum @default = default)
        where TEnum : struct, Enum =>
        Enum.TryParse(value, out TEnum result) ? result : @default;
}