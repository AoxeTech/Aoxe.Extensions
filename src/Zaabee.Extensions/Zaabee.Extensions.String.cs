namespace Zaabee.Extensions;

public static partial class ZaabeeExtension
{
    private static readonly Encoding Utf8Encoding = Encoding.UTF8;

    public static string TrimStart(this string target, string? trimString)
    {
        if (string.IsNullOrEmpty(trimString)) return target;

        var result = target;
        while (result.StartsWith(trimString!))
            result = result.Substring(trimString!.Length);

        return result;
    }

    public static string TrimEnd(this string target, string? trimString)
    {
        if (string.IsNullOrEmpty(trimString)) return target;

        var result = target;
        while (result.EndsWith(trimString!))
            result = result.Substring(0, result.Length - trimString!.Length);

        return result;
    }

    public static bool IsNullOrEmpty(this string? value) =>
        string.IsNullOrEmpty(value);

    public static bool IsNullOrWhiteSpace(this string? value) =>
        string.IsNullOrWhiteSpace(value);

    public static string StringJoin<T>(this IEnumerable<T> values, string separator) =>
        string.Join(separator, values);

    #region Bytes

    public static byte[] ToUtf8Bytes(this string value) =>
        Encoding.UTF8.GetBytes(value);

    public static byte[] ToAsciiBytes(this string value) =>
        Encoding.ASCII.GetBytes(value);

    public static byte[] ToBigEndianUnicodeBytes(this string value) =>
        Encoding.BigEndianUnicode.GetBytes(value);

    public static byte[] ToDefaultBytes(this string value) =>
        Encoding.Default.GetBytes(value);

    public static byte[] ToUtf32Bytes(this string value) =>
        Encoding.UTF32.GetBytes(value);

    public static byte[] ToUnicodeBytes(this string value) =>
        Encoding.Unicode.GetBytes(value);

    public static byte[] ToBytes(this string value, Encoding? encoding = null) =>
        (encoding ?? Utf8Encoding).GetBytes(value);

    #endregion

    #region Parse

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

    #endregion

    #region TryParse

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

    public static TEnum TryParseEnum<TEnum>(this string value, TEnum @default = default)
        where TEnum : struct, Enum =>
        Enum.TryParse(value, out TEnum result) ? result : @default;

    #endregion

    #region Base64

    public static string ToBase64String(this string s, Encoding? encoding = null) =>
        Convert.ToBase64String(s.ToBytes(encoding));

    public static byte[] ToBase64Bytes(this string s, Encoding? encoding = null) =>
        s.ToBase64String(encoding).ToBytes(encoding);

    public static byte[] FromBase64ToBytes(this string s) =>
        Convert.FromBase64String(s);

    public static string FromBase64ToString(this string s, Encoding? encoding = null) =>
        s.FromBase64ToBytes().GetString(encoding);

    #endregion

    public static string Format(this string format, params object[] args) =>
        string.Format(format, args);

    public static string GetLetterOrDigit(this string source) =>
        new string(source.Where(char.IsLetterOrDigit).ToArray());

    public static string? TryReplace(this string? str, string oldValue, string? newValue) =>
        string.IsNullOrEmpty(str) ? str : str!.Replace(oldValue, newValue);

    public static int ToInt(this string value, NumerationSystem numerationSystem) =>
        value.ToInt((int)numerationSystem);

    public static long ToLong(this string value, NumerationSystem numerationSystem) =>
        value.ToLong((int)numerationSystem);

    public static int ToInt(this string value, int fromBase)
    {
        if (value.IsNullOrWhiteSpace()) return default;

        var isMinus = false;
        if (value[0] is '-')
        {
            isMinus = true;
            value = new string(value.Skip(1).ToArray());
        }

        if (value.Any(c => !char.IsLetterOrDigit(c)))
            throw new ArgumentException("The string can only contain letter or digit.", nameof(value));

        if (fromBase <= 36) value = value.ToLower();

        var result = value
            .Select((t, i) => Consts.LetterAndDigit.IndexOf(t) * (int)Math.Pow(fromBase, value.Length - i - 1))
            .Sum();

        result = isMinus ? 0 - result : result;
        return result;
    }

    public static long ToLong(this string value, int fromBase)
    {
        if (value.IsNullOrWhiteSpace()) return default;

        var isMinus = false;
        if (value[0] is '-')
        {
            isMinus = true;
            value = new string(value.Skip(1).ToArray());
        }

        if (value.Any(c => !char.IsLetterOrDigit(c)))
            throw new ArgumentException("The string can only contain letter or digit.", nameof(value));

        if (fromBase <= 36) value = value.ToLower();

        var result = value
            .Select((t, i) => Consts.LetterAndDigit.IndexOf(t) * (long)Math.Pow(fromBase, value.Length - i - 1))
            .Sum();

        result = isMinus ? 0 - result : result;
        return result;
    }
}