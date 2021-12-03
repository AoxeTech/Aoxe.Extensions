namespace Zaabee.Extensions;

public static partial class ZaabeeExtension
{
    private static readonly Encoding Utf8Encoding = Encoding.UTF8;

    public static string TrimStart(this string target, string? trimString)
    {
        if (string.IsNullOrEmpty(trimString)) return target;

        var result = target;
        while (result.StartsWith(trimString))
            result = result.Substring(trimString!.Length);

        return result;
    }

    public static string TrimEnd(this string target, string? trimString)
    {
        if (string.IsNullOrEmpty(trimString)) return target;

        var result = target;
        while (result.EndsWith(trimString))
            result = result.Substring(0, result.Length - trimString!.Length);

        return result;
    }

    public static bool IsNullOrEmpty(this string? value) =>
        string.IsNullOrEmpty(value);

    public static bool IsNullOrWhiteSpace(this string? value) =>
        string.IsNullOrWhiteSpace(value);

    public static string StringJoin<T>(this IEnumerable<T> values, string separator) =>
        string.Join(separator, values);

    public static string Format(this string format, params object[] args) =>
        string.Format(format, args);

    public static string GetLetterOrDigit(this string source) =>
        new string(source.Where(char.IsLetterOrDigit).ToArray());

    public static string? TryReplace(this string? str, string oldValue, string? newValue) =>
        string.IsNullOrEmpty(str) ? str : str!.Replace(oldValue, newValue);
}