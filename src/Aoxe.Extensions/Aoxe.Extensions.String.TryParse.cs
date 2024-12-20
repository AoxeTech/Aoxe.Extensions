namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    public static sbyte TryParseSbyte(this string s, sbyte @default = 0) =>
        sbyte.TryParse(s, out var result) ? result : @default;

    public static byte TryParseByte(this string s, byte @default = 0) =>
        byte.TryParse(s, out var result) ? result : @default;

    public static short TryParseShort(this string s, short @default = 0) =>
        short.TryParse(s, out var result) ? result : @default;

    public static ushort TryParseUshort(this string s, ushort @default = 0) =>
        ushort.TryParse(s, out var result) ? result : @default;

    public static int TryParseInt(this string s, int @default = 0) =>
        int.TryParse(s, out var result) ? result : @default;

    public static uint TryParseUint(this string s, uint @default = 0) =>
        uint.TryParse(s, out var result) ? result : @default;

    public static long TryParseLong(this string s, long @default = 0) =>
        long.TryParse(s, out var result) ? result : @default;

    public static ulong TryParseUlong(this string s, ulong @default = 0) =>
        ulong.TryParse(s, out var result) ? result : @default;

    public static float TryParseFloat(this string s, float @default = 0) =>
        float.TryParse(s, out var result) ? result : @default;

    public static double TryParseDouble(this string s, double @default = 0) =>
        double.TryParse(s, out var result) ? result : @default;

    public static decimal TryParseDecimal(this string s, decimal @default = 0) =>
        decimal.TryParse(s, out var result) ? result : @default;

    public static bool TryParseBool(this string s, bool @default = false) =>
        bool.TryParse(s, out var result) ? result : @default;

    public static DateTime TryParseDateTime(this string s, DateTime @default = default) =>
        DateTime.TryParse(s, out var result) ? result : @default;

    public static DateTimeOffset TryParseDateTimeOffset(
        this string s,
        DateTimeOffset @default = default
    ) => DateTimeOffset.TryParse(s, out var result) ? result : @default;

    public static TEnum TryParseEnum<TEnum>(this string? value, TEnum @default = default)
        where TEnum : struct, Enum => Enum.TryParse(value, out TEnum result) ? result : @default;
}
