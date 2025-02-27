namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    /// <summary>
    /// Parses a string representation of a number into an sbyte
    /// </summary>
    /// <param name="value">String to parse</param>
    /// <param name="styles">Number styles for parsing</param>
    /// <param name="provider">Culture-specific format provider</param>
    /// <returns>Parsed sbyte value</returns>
    /// <exception cref="ArgumentNullException">Thrown when input is null</exception>
    /// <exception cref="FormatException">Thrown when format is invalid</exception>
    public static sbyte ParseSbyte(
        this string value,
        NumberStyles styles = NumberStyles.Integer,
        IFormatProvider? provider = null
    ) =>
        sbyte.Parse(
            value ?? throw new ArgumentNullException(nameof(value)),
            styles,
            provider ?? CultureInfo.InvariantCulture
        );

    // Similar XML documentation for other numeric types...

    /// <summary>
    /// Parses a string representation of a number into a byte
    /// </summary>
    public static byte ParseByte(
        this string value,
        NumberStyles styles = NumberStyles.Integer,
        IFormatProvider? provider = null
    ) =>
        byte.Parse(
            value ?? throw new ArgumentNullException(nameof(value)),
            styles,
            provider ?? CultureInfo.InvariantCulture
        );

    /// <summary>
    /// Parses a string representation of a number into a short
    /// </summary>
    public static short ParseShort(
        this string value,
        NumberStyles styles = NumberStyles.Integer,
        IFormatProvider? provider = null
    ) =>
        short.Parse(
            value ?? throw new ArgumentNullException(nameof(value)),
            styles,
            provider ?? CultureInfo.InvariantCulture
        );

    /// <summary>
    /// Parses a string representation of a number into a ushort
    /// </summary>
    public static ushort ParseUshort(
        this string value,
        NumberStyles styles = NumberStyles.Integer,
        IFormatProvider? provider = null
    ) =>
        ushort.Parse(
            value ?? throw new ArgumentNullException(nameof(value)),
            styles,
            provider ?? CultureInfo.InvariantCulture
        );

    /// <summary>
    /// Parses a string representation of a number into an int
    /// </summary>
    public static int ParseInt(
        this string value,
        NumberStyles styles = NumberStyles.Integer,
        IFormatProvider? provider = null
    ) =>
        int.Parse(
            value ?? throw new ArgumentNullException(nameof(value)),
            styles,
            provider ?? CultureInfo.InvariantCulture
        );

    /// <summary>
    /// Parses a string representation of a number into a uint
    /// </summary>
    public static uint ParseUint(
        this string value,
        NumberStyles styles = NumberStyles.Integer,
        IFormatProvider? provider = null
    ) =>
        uint.Parse(
            value ?? throw new ArgumentNullException(nameof(value)),
            styles,
            provider ?? CultureInfo.InvariantCulture
        );

    /// <summary>
    /// Parses a string representation of a number into a long
    /// </summary>
    public static long ParseLong(
        this string value,
        NumberStyles styles = NumberStyles.Integer,
        IFormatProvider? provider = null
    ) =>
        long.Parse(
            value ?? throw new ArgumentNullException(nameof(value)),
            styles,
            provider ?? CultureInfo.InvariantCulture
        );

    /// <summary>
    /// Parses a string representation of a number into a ulong
    /// </summary>
    public static ulong ParseUlong(
        this string value,
        NumberStyles styles = NumberStyles.Integer,
        IFormatProvider? provider = null
    ) =>
        ulong.Parse(
            value ?? throw new ArgumentNullException(nameof(value)),
            styles,
            provider ?? CultureInfo.InvariantCulture
        );

    /// <summary>
    /// Parses a string representation of a number into a float
    /// </summary>
    public static float ParseFloat(
        this string value,
        NumberStyles styles = NumberStyles.Float | NumberStyles.AllowThousands,
        IFormatProvider? provider = null
    ) =>
        float.Parse(
            value ?? throw new ArgumentNullException(nameof(value)),
            styles,
            provider ?? CultureInfo.InvariantCulture
        );

    /// <summary>
    /// Parses a string representation of a number into a double
    /// </summary>
    public static double ParseDouble(
        this string value,
        NumberStyles styles = NumberStyles.Float | NumberStyles.AllowThousands,
        IFormatProvider? provider = null
    ) =>
        double.Parse(
            value ?? throw new ArgumentNullException(nameof(value)),
            styles,
            provider ?? CultureInfo.InvariantCulture
        );

    /// <summary>
    /// Parses a string representation of a number into a decimal
    /// </summary>
    public static decimal ParseDecimal(
        this string value,
        NumberStyles styles = NumberStyles.Number,
        IFormatProvider? provider = null
    ) =>
        decimal.Parse(
            value ?? throw new ArgumentNullException(nameof(value)),
            styles,
            provider ?? CultureInfo.InvariantCulture
        );

    /// <summary>
    /// Parses a string representation into a boolean value
    /// </summary>
    public static bool ParseBool(this string value) =>
        bool.Parse(value ?? throw new ArgumentNullException(nameof(value)));

    /// <summary>
    /// Parses a string representation of a date and time
    /// </summary>
    public static DateTime ParseDateTime(
        this string value,
        IFormatProvider? provider = null,
        DateTimeStyles styles = DateTimeStyles.None
    ) =>
        DateTime.Parse(
            value ?? throw new ArgumentNullException(nameof(value)),
            provider ?? CultureInfo.CurrentCulture,
            styles
        );

    /// <summary>
    /// Parses a string representation of a date and time offset
    /// </summary>
    public static DateTimeOffset ParseDateTimeOffset(
        this string value,
        IFormatProvider? provider = null,
        DateTimeStyles styles = DateTimeStyles.None
    ) =>
        DateTimeOffset.Parse(
            value ?? throw new ArgumentNullException(nameof(value)),
            provider ?? CultureInfo.CurrentCulture,
            styles
        );

    /// <summary>
    /// Parses a string representation of an enum value
    /// </summary>
    public static TEnum ParseEnum<TEnum>(this string value, bool ignoreCase = false)
        where TEnum : struct, Enum =>
        (TEnum)
            Enum.Parse(
                typeof(TEnum),
                value ?? throw new ArgumentNullException(nameof(value)),
                ignoreCase
            );

    /// <summary>
    /// Parses a string representation of an enum value
    /// </summary>
    public static object ParseEnum(this string value, Type enumType, bool ignoreCase = false) =>
        Enum.Parse(
            enumType ?? throw new ArgumentNullException(nameof(enumType)),
            value ?? throw new ArgumentNullException(nameof(value)),
            ignoreCase
        );
}
