namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    /// <summary>
    /// Attempts to parse a string representation of a number into an sbyte
    /// </summary>
    /// <param name="value">The string to parse</param>
    /// <param name="defaultValue">Value to return if parsing fails</param>
    /// <param name="styles">Number styles to use for parsing</param>
    /// <param name="provider">Culture-specific format provider</param>
    /// <returns>Parsed sbyte or defaultValue if parsing fails</returns>
    public static sbyte TryParseSbyte(
        this string? value,
        sbyte defaultValue = 0,
        NumberStyles styles = NumberStyles.Integer,
        IFormatProvider? provider = null
    ) =>
        sbyte.TryParse(value, styles, provider ?? CultureInfo.InvariantCulture, out var result)
            ? result
            : defaultValue;

    /// <summary>
    /// Attempts to parse a string representation of a number into a byte
    /// </summary>
    /// <param name="value">The string to parse</param>
    /// <param name="defaultValue">Value to return if parsing fails</param>
    /// <param name="styles">Number styles to use for parsing</param>
    /// <param name="provider">Culture-specific format provider</param>
    /// <returns>Parsed byte or defaultValue if parsing fails</returns>
    public static byte TryParseByte(
        this string? value,
        byte defaultValue = 0,
        NumberStyles styles = NumberStyles.Integer,
        IFormatProvider? provider = null
    ) =>
        byte.TryParse(value, styles, provider ?? CultureInfo.InvariantCulture, out var result)
            ? result
            : defaultValue;

    /// <summary>
    /// Attempts to parse a string representation of a number into a short
    /// </summary>
    /// <param name="value">The string to parse</param>
    /// <param name="defaultValue">Value to return if parsing fails</param>
    /// <param name="styles">Number styles to use for parsing</param>
    /// <param name="provider">Culture-specific format provider</param>
    /// <returns>Parsed short or defaultValue if parsing fails</returns>
    public static short TryParseShort(
        this string? value,
        short defaultValue = 0,
        NumberStyles styles = NumberStyles.Integer,
        IFormatProvider? provider = null
    ) =>
        short.TryParse(value, styles, provider ?? CultureInfo.InvariantCulture, out var result)
            ? result
            : defaultValue;

    /// <summary>
    /// Attempts to parse a string representation of a number into a ushort
    /// </summary>
    /// <param name="value">The string to parse</param>
    /// <param name="defaultValue">Value to return if parsing fails</param>
    /// <param name="styles">Number styles to use for parsing</param>
    /// <param name="provider">Culture-specific format provider</param>
    /// <returns>Parsed ushort or defaultValue if parsing fails</returns>
    public static ushort TryParseUshort(
        this string? value,
        ushort defaultValue = 0,
        NumberStyles styles = NumberStyles.Integer,
        IFormatProvider? provider = null
    ) =>
        ushort.TryParse(value, styles, provider ?? CultureInfo.InvariantCulture, out var result)
            ? result
            : defaultValue;

    /// <summary>
    /// Attempts to parse a string representation of a number into an int
    /// </summary>
    /// <param name="value">The string to parse</param>
    /// <param name="defaultValue">Value to return if parsing fails</param>
    /// <param name="styles">Number styles to use for parsing</param>
    /// <param name="provider">Culture-specific format provider</param>
    /// <returns>Parsed int or defaultValue if parsing fails</returns>
    public static int TryParseInt(
        this string? value,
        int defaultValue = 0,
        NumberStyles styles = NumberStyles.Integer,
        IFormatProvider? provider = null
    ) =>
        int.TryParse(value, styles, provider ?? CultureInfo.InvariantCulture, out var result)
            ? result
            : defaultValue;

    /// <summary>
    /// Attempts to parse a string representation of a number into a uint
    /// </summary>
    /// <param name="value">The string to parse</param>
    /// <param name="defaultValue">Value to return if parsing fails</param>
    /// <param name="styles">Number styles to use for parsing</param>
    /// <param name="provider">Culture-specific format provider</param>
    /// <returns>Parsed uint or defaultValue if parsing fails</returns>
    public static uint TryParseUint(
        this string? value,
        uint defaultValue = 0,
        NumberStyles styles = NumberStyles.Integer,
        IFormatProvider? provider = null
    ) =>
        uint.TryParse(value, styles, provider ?? CultureInfo.InvariantCulture, out var result)
            ? result
            : defaultValue;

    /// <summary>
    /// Attempts to parse a string representation of a number into a long
    /// </summary>
    /// <param name="value">The string to parse</param>
    /// <param name="defaultValue">Value to return if parsing fails</param>
    /// <param name="styles">Number styles to use for parsing</param>
    /// <param name="provider">Culture-specific format provider</param>
    /// <returns>Parsed long or defaultValue if parsing fails</returns>
    public static long TryParseLong(
        this string? value,
        long defaultValue = 0,
        NumberStyles styles = NumberStyles.Integer,
        IFormatProvider? provider = null
    ) =>
        long.TryParse(value, styles, provider ?? CultureInfo.InvariantCulture, out var result)
            ? result
            : defaultValue;

    /// <summary>
    /// Attempts to parse a string representation of a number into a ulong
    /// </summary>
    /// <param name="value">The string to parse</param>
    /// <param name="defaultValue">Value to return if parsing fails</param>
    /// <param name="styles">Number styles to use for parsing</param>
    /// <param name="provider">Culture-specific format provider</param>
    /// <returns>Parsed ulong or defaultValue if parsing fails</returns>
    public static ulong TryParseUlong(
        this string? value,
        ulong defaultValue = 0,
        NumberStyles styles = NumberStyles.Integer,
        IFormatProvider? provider = null
    ) =>
        ulong.TryParse(value, styles, provider ?? CultureInfo.InvariantCulture, out var result)
            ? result
            : defaultValue;

    /// <summary>
    /// Attempts to parse a string representation of a number into a float
    /// </summary>
    /// <param name="value">The string to parse</param>
    /// <param name="defaultValue">Value to return if parsing fails</param>
    /// <param name="styles">Number styles to use for parsing</param>
    /// <param name="provider">Culture-specific format provider</param>
    /// <returns>Parsed float or defaultValue if parsing fails</returns>
    public static float TryParseFloat(
        this string? value,
        float defaultValue = 0,
        NumberStyles styles = NumberStyles.Float | NumberStyles.AllowThousands,
        IFormatProvider? provider = null
    ) =>
        float.TryParse(value, styles, provider ?? CultureInfo.InvariantCulture, out var result)
            ? result
            : defaultValue;

    /// <summary>
    /// Attempts to parse a string representation of a number into a double
    /// </summary>
    /// <param name="value">The string to parse</param>
    /// <param name="defaultValue">Value to return if parsing fails</param>
    /// <param name="styles">Number styles to use for parsing</param>
    /// <param name="provider">Culture-specific format provider</param>
    /// <returns>Parsed double or defaultValue if parsing fails</returns>
    public static double TryParseDouble(
        this string? value,
        double defaultValue = 0,
        NumberStyles styles = NumberStyles.Float | NumberStyles.AllowThousands,
        IFormatProvider? provider = null
    ) =>
        double.TryParse(value, styles, provider ?? CultureInfo.InvariantCulture, out var result)
            ? result
            : defaultValue;

    /// <summary>
    /// Attempts to parse a string representation of a number into a decimal
    /// </summary>
    /// <param name="value">The string to parse</param>
    /// <param name="defaultValue">Value to return if parsing fails</param>
    /// <param name="styles">Number styles to use for parsing</param>
    /// <param name="provider">Culture-specific format provider</param>
    /// <returns>Parsed decimal or defaultValue if parsing fails</returns>
    public static decimal TryParseDecimal(
        this string? value,
        decimal defaultValue = 0,
        NumberStyles styles = NumberStyles.Number,
        IFormatProvider? provider = null
    ) =>
        decimal.TryParse(value, styles, provider ?? CultureInfo.InvariantCulture, out var result)
            ? result
            : defaultValue;

    /// <summary>
    /// Attempts to parse a string representation into a boolean value
    /// </summary>
    /// <param name="value">The string to parse</param>
    /// <param name="defaultValue">Value to return if parsing fails</param>
    /// <returns>Parsed boolean or defaultValue if parsing fails</returns>
    public static bool TryParseBool(this string? value, bool defaultValue = false) =>
        bool.TryParse(value, out var result) ? result : defaultValue;

    /// <summary>
    /// Attempts to parse a string representation of a date and time
    /// </summary>
    /// <param name="value">The string to parse</param>
    /// <param name="defaultValue">Value to return if parsing fails</param>
    /// <param name="styles">DateTimeStyles to use for parsing</param>
    /// <param name="provider">Culture-specific format provider</param>
    /// <returns>Parsed DateTime or defaultValue if parsing fails</returns>
    public static DateTime TryParseDateTime(
        this string? value,
        DateTime defaultValue = default,
        DateTimeStyles styles = DateTimeStyles.None,
        IFormatProvider? provider = null
    ) =>
        DateTime.TryParse(value, provider ?? CultureInfo.CurrentCulture, styles, out var result)
            ? result
            : defaultValue;

    /// <summary>
    /// Attempts to parse a string representation of a date and time offset
    /// </summary>
    /// <param name="value">The string to parse</param>
    /// <param name="defaultValue">Value to return if parsing fails</param>
    /// <param name="styles">DateTimeStyles to use for parsing</param>
    /// <param name="provider">Culture-specific format provider</param>
    /// <returns>Parsed DateTimeOffset or defaultValue if parsing fails</returns>
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

    /// <summary>
    /// Attempts to parse a string representation of an enum value
    /// </summary>
    /// <typeparam name="TEnum">Enum type to parse</typeparam>
    /// <param name="value">The string to parse</param>
    /// <param name="defaultValue">Value to return if parsing fails</param>
    /// <param name="ignoreCase">Whether to ignore case when parsing</param>
    /// <returns>Parsed enum value or defaultValue if parsing fails</returns>
    public static TEnum TryParseEnum<TEnum>(
        this string? value,
        TEnum defaultValue = default,
        bool ignoreCase = false
    )
        where TEnum : struct, Enum =>
        Enum.TryParse(value, ignoreCase, out TEnum result) ? result : defaultValue;
}
