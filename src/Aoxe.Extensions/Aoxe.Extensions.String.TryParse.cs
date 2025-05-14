namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    public static sbyte TryParseSbyte(
        this string? value,
        sbyte defaultValue = 0,
        NumberStyles styles = NumberStyles.Integer,
        IFormatProvider? provider = null
    ) =>
        sbyte.TryParse(value, styles, provider ?? CultureInfo.InvariantCulture, out var result)
            ? result
            : defaultValue;

    public static byte TryParseByte(
        this string? value,
        byte defaultValue = 0,
        NumberStyles styles = NumberStyles.Integer,
        IFormatProvider? provider = null
    ) =>
        byte.TryParse(value, styles, provider ?? CultureInfo.InvariantCulture, out var result)
            ? result
            : defaultValue;

    public static short TryParseShort(
        this string? value,
        short defaultValue = 0,
        NumberStyles styles = NumberStyles.Integer,
        IFormatProvider? provider = null
    ) =>
        short.TryParse(value, styles, provider ?? CultureInfo.InvariantCulture, out var result)
            ? result
            : defaultValue;

    public static ushort TryParseUshort(
        this string? value,
        ushort defaultValue = 0,
        NumberStyles styles = NumberStyles.Integer,
        IFormatProvider? provider = null
    ) =>
        ushort.TryParse(value, styles, provider ?? CultureInfo.InvariantCulture, out var result)
            ? result
            : defaultValue;

    public static int TryParseInt(
        this string? value,
        int defaultValue = 0,
        NumberStyles styles = NumberStyles.Integer,
        IFormatProvider? provider = null
    ) =>
        int.TryParse(value, styles, provider ?? CultureInfo.InvariantCulture, out var result)
            ? result
            : defaultValue;

    public static uint TryParseUint(
        this string? value,
        uint defaultValue = 0,
        NumberStyles styles = NumberStyles.Integer,
        IFormatProvider? provider = null
    ) =>
        uint.TryParse(value, styles, provider ?? CultureInfo.InvariantCulture, out var result)
            ? result
            : defaultValue;

    public static long TryParseLong(
        this string? value,
        long defaultValue = 0,
        NumberStyles styles = NumberStyles.Integer,
        IFormatProvider? provider = null
    ) =>
        long.TryParse(value, styles, provider ?? CultureInfo.InvariantCulture, out var result)
            ? result
            : defaultValue;

    public static ulong TryParseUlong(
        this string? value,
        ulong defaultValue = 0,
        NumberStyles styles = NumberStyles.Integer,
        IFormatProvider? provider = null
    ) =>
        ulong.TryParse(value, styles, provider ?? CultureInfo.InvariantCulture, out var result)
            ? result
            : defaultValue;

    public static float TryParseFloat(
        this string? value,
        float defaultValue = 0,
        NumberStyles styles = NumberStyles.Float | NumberStyles.AllowThousands,
        IFormatProvider? provider = null
    ) =>
        float.TryParse(value, styles, provider ?? CultureInfo.InvariantCulture, out var result)
            ? result
            : defaultValue;

    public static double TryParseDouble(
        this string? value,
        double defaultValue = 0,
        NumberStyles styles = NumberStyles.Float | NumberStyles.AllowThousands,
        IFormatProvider? provider = null
    ) =>
        double.TryParse(value, styles, provider ?? CultureInfo.InvariantCulture, out var result)
            ? result
            : defaultValue;

    public static decimal TryParseDecimal(
        this string? value,
        decimal defaultValue = 0,
        NumberStyles styles = NumberStyles.Number,
        IFormatProvider? provider = null
    ) =>
        decimal.TryParse(value, styles, provider ?? CultureInfo.InvariantCulture, out var result)
            ? result
            : defaultValue;

    public static bool TryParseBool(this string? value, bool defaultValue = false) =>
        value is not null && (bool.TryParse(value, out var result) ? result : defaultValue);

    public static DateTime TryParseDateTime(
        this string? value,
        DateTime defaultValue = default,
        DateTimeStyles styles = DateTimeStyles.None,
        IFormatProvider? provider = null
    ) =>
        DateTime.TryParse(value, provider ?? CultureInfo.CurrentCulture, styles, out var result)
            ? result
            : defaultValue;

    public static DateTimeOffset TryParseDateTimeOffset(
        this string? value,
        DateTimeOffset defaultValue = default,
        DateTimeStyles styles = DateTimeStyles.None,
        IFormatProvider? provider = null
    ) =>
        DateTimeOffset.TryParse(
            value,
            provider ?? CultureInfo.CurrentCulture,
            styles,
            out var result
        )
            ? result
            : defaultValue;

    public static TEnum TryParseEnum<TEnum>(
        this string? value,
        TEnum defaultValue = default,
        bool ignoreCase = false
    )
        where TEnum : struct, Enum =>
        Enum.TryParse(value, ignoreCase, out TEnum result) ? result : defaultValue;
}
