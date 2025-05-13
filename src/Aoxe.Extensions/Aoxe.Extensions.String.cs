namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    private static readonly Encoding Utf8Encoding = Encoding.UTF8;

    /// <summary>
    /// Removes all leading occurrences of a specified string
    /// </summary>
    public static string? TrimStart(
        this string? target,
        string trimString,
        StringComparison comparison = StringComparison.Ordinal
    )
    {
        if (string.IsNullOrEmpty(target) || string.IsNullOrEmpty(trimString))
            return target;

        var result = target!;
        while (result.StartsWith(trimString, comparison))
        {
            result = result.Substring(trimString.Length);
        }
        return result;
    }

    /// <summary>
    /// Removes all trailing occurrences of a specified string
    /// </summary>
    public static string? TrimEnd(
        this string? target,
        string trimString,
        StringComparison comparison = StringComparison.Ordinal
    )
    {
        if (string.IsNullOrEmpty(target) || string.IsNullOrEmpty(trimString))
            return target;

        var result = target!;
        while (result.EndsWith(trimString, comparison))
        {
            result = result.Substring(0, result.Length - trimString.Length);
        }
        return result;
    }

    /// <summary>
    /// Truncates string to specified length with optional suffix
    /// </summary>
    public static string? Truncate(this string? value, int maxLength, string suffix = "...")
    {
        if (string.IsNullOrEmpty(value))
            return value;

        if (maxLength <= 0)
            throw new ArgumentException("Maximum length must be positive", nameof(maxLength));

        if (value!.Length <= maxLength)
            return value;

        if (maxLength <= suffix.Length)
            return suffix.Substring(0, maxLength);

        return value.Substring(0, maxLength - suffix.Length) + suffix;
    }

    /// <summary>
    /// Safely replaces occurrences of a specified string
    /// </summary>
    public static string? SafeReplace(
        this string? str,
        string oldValue,
        string newValue,
        StringComparison comparison = StringComparison.Ordinal
    )
    {
        if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(oldValue))
            return str;

        var sb = new StringBuilder();
        var previousIndex = 0;
        var index = str!.IndexOf(oldValue, comparison);

        while (index != -1)
        {
            sb.Append(str.Substring(previousIndex, index - previousIndex));
            sb.Append(newValue);
            previousIndex = index + oldValue.Length;
            index = str.IndexOf(oldValue, previousIndex, comparison);
        }

        sb.Append(str.Substring(previousIndex));
        return sb.ToString();
    }

    // Other methods remain the same as previous .NET Standard 2.0 compatible versions
    public static bool IsNullOrEmpty(this string? value) => string.IsNullOrEmpty(value);

    public static bool IsNullOrWhiteSpace(this string? value) => string.IsNullOrWhiteSpace(value);

    public static string JoinToString<T>(
        this IEnumerable<T>? values,
        string separator,
        string defaultValue = ""
    ) => values is not null && values.Any() ? string.Join(separator, values) : defaultValue;

    public static string FilterLettersAndDigits(this string source)
    {
        if (string.IsNullOrEmpty(source))
            return string.Empty;

        var sb = new StringBuilder(source.Length);
        foreach (var c in source.Where(char.IsLetterOrDigit))
        {
            sb.Append(c);
        }
        return sb.ToString();
    }
}
