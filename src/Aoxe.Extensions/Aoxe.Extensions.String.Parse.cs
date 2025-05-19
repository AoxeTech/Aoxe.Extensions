namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    public static sbyte ParseSbyte(
        this string value,
        NumberStyles styles = NumberStyles.Integer,
        IFormatProvider? provider = null
    )
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));
        return sbyte.Parse(value, styles, provider ?? CultureInfo.InvariantCulture);
    }

    public static byte ParseByte(
        this string value,
        NumberStyles styles = NumberStyles.Integer,
        IFormatProvider? provider = null
    )
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));
        return byte.Parse(value, styles, provider ?? CultureInfo.InvariantCulture);
    }

    public static short ParseShort(
        this string value,
        NumberStyles styles = NumberStyles.Integer,
        IFormatProvider? provider = null
    )
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));
        return short.Parse(value, styles, provider ?? CultureInfo.InvariantCulture);
    }

    public static ushort ParseUshort(
        this string value,
        NumberStyles styles = NumberStyles.Integer,
        IFormatProvider? provider = null
    )
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));
        return ushort.Parse(value, styles, provider ?? CultureInfo.InvariantCulture);
    }

    public static int ParseInt(
        this string value,
        NumberStyles styles = NumberStyles.Integer,
        IFormatProvider? provider = null
    )
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));
        return int.Parse(value, styles, provider ?? CultureInfo.InvariantCulture);
    }

    public static uint ParseUint(
        this string value,
        NumberStyles styles = NumberStyles.Integer,
        IFormatProvider? provider = null
    )
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));
        return uint.Parse(value, styles, provider ?? CultureInfo.InvariantCulture);
    }

    public static long ParseLong(
        this string value,
        NumberStyles styles = NumberStyles.Number,
        IFormatProvider? provider = null
    )
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));
        return long.Parse(value, styles, provider ?? CultureInfo.InvariantCulture);
    }

    public static ulong ParseUlong(
        this string value,
        NumberStyles styles = NumberStyles.Number,
        IFormatProvider? provider = null
    )
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));
        return ulong.Parse(value, styles, provider ?? CultureInfo.InvariantCulture);
    }

    public static float ParseFloat(
        this string value,
        NumberStyles styles = NumberStyles.Float | NumberStyles.AllowThousands,
        IFormatProvider? provider = null
    )
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));
        return float.Parse(value, styles, provider ?? CultureInfo.InvariantCulture);
    }

    public static double ParseDouble(
        this string value,
        NumberStyles styles = NumberStyles.Float | NumberStyles.AllowThousands,
        IFormatProvider? provider = null
    )
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));
        return double.Parse(value, styles, provider ?? CultureInfo.InvariantCulture);
    }

    public static decimal ParseDecimal(
        this string value,
        NumberStyles styles = NumberStyles.Number,
        IFormatProvider? provider = null
    )
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));
        return decimal.Parse(value, styles, provider ?? CultureInfo.InvariantCulture);
    }

    public static bool ParseBool(this string value)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));
        return bool.Parse(value);
    }

    public static DateTime ParseDateTime(
        this string value,
        IFormatProvider? provider = null,
        DateTimeStyles styles = DateTimeStyles.None
    )
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));
        return DateTime.Parse(value, provider ?? CultureInfo.CurrentCulture, styles);
    }

    public static DateTimeOffset ParseDateTimeOffset(
        this string value,
        IFormatProvider? provider = null,
        DateTimeStyles styles = DateTimeStyles.None
    )
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));
        return DateTimeOffset.Parse(value, provider ?? CultureInfo.CurrentCulture, styles);
    }

    public static TEnum ParseEnum<TEnum>(this string value, bool ignoreCase = false)
        where TEnum : struct, Enum
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));
        return (TEnum)Enum.Parse(typeof(TEnum), value, ignoreCase);
    }

    public static object ParseEnum(this string value, Type enumType, bool ignoreCase = false)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));
        return Enum.Parse(enumType, value, ignoreCase);
    }
}
