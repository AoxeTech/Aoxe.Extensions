namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    public static sbyte ParseSbyte(
        this string value,
        NumberStyles styles = NumberStyles.Integer,
        IFormatProvider? provider = null
    ) => sbyte.Parse(value, styles, provider ?? CultureInfo.InvariantCulture);

    public static byte ParseByte(
        this string value,
        NumberStyles styles = NumberStyles.Integer,
        IFormatProvider? provider = null
    ) => byte.Parse(value, styles, provider ?? CultureInfo.InvariantCulture);

    public static short ParseShort(
        this string value,
        NumberStyles styles = NumberStyles.Integer,
        IFormatProvider? provider = null
    ) => short.Parse(value, styles, provider ?? CultureInfo.InvariantCulture);

    public static ushort ParseUshort(
        this string value,
        NumberStyles styles = NumberStyles.Integer,
        IFormatProvider? provider = null
    ) => ushort.Parse(value, styles, provider ?? CultureInfo.InvariantCulture);

    public static int ParseInt(
        this string value,
        NumberStyles styles = NumberStyles.Integer,
        IFormatProvider? provider = null
    ) => int.Parse(value, styles, provider ?? CultureInfo.InvariantCulture);

    public static uint ParseUint(
        this string value,
        NumberStyles styles = NumberStyles.Integer,
        IFormatProvider? provider = null
    ) => uint.Parse(value, styles, provider ?? CultureInfo.InvariantCulture);

    public static long ParseLong(
        this string value,
        NumberStyles styles = NumberStyles.Integer,
        IFormatProvider? provider = null
    ) => long.Parse(value, styles, provider ?? CultureInfo.InvariantCulture);

    public static ulong ParseUlong(
        this string value,
        NumberStyles styles = NumberStyles.Integer,
        IFormatProvider? provider = null
    ) => ulong.Parse(value, styles, provider ?? CultureInfo.InvariantCulture);

    public static float ParseFloat(
        this string value,
        NumberStyles styles = NumberStyles.Float | NumberStyles.AllowThousands,
        IFormatProvider? provider = null
    ) => float.Parse(value, styles, provider ?? CultureInfo.InvariantCulture);

    public static double ParseDouble(
        this string value,
        NumberStyles styles = NumberStyles.Float | NumberStyles.AllowThousands,
        IFormatProvider? provider = null
    ) => double.Parse(value, styles, provider ?? CultureInfo.InvariantCulture);

    public static decimal ParseDecimal(
        this string value,
        NumberStyles styles = NumberStyles.Number,
        IFormatProvider? provider = null
    ) => decimal.Parse(value, styles, provider ?? CultureInfo.InvariantCulture);

    public static bool ParseBool(this string value) => bool.Parse(value);

    public static DateTime ParseDateTime(
        this string value,
        IFormatProvider? provider = null,
        DateTimeStyles styles = DateTimeStyles.None
    ) => DateTime.Parse(value, provider ?? CultureInfo.CurrentCulture, styles);

    public static DateTimeOffset ParseDateTimeOffset(
        this string value,
        IFormatProvider? provider = null,
        DateTimeStyles styles = DateTimeStyles.None
    ) => DateTimeOffset.Parse(value, provider ?? CultureInfo.CurrentCulture, styles);

    public static TEnum ParseEnum<TEnum>(this string value, bool ignoreCase = false)
        where TEnum : struct, Enum => (TEnum)Enum.Parse(typeof(TEnum), value, ignoreCase);

    public static object ParseEnum(this string value, Type enumType, bool ignoreCase = false) =>
        Enum.Parse(enumType, value, ignoreCase);
}
