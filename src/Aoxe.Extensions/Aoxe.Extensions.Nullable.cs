namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    public static bool IsNull<T>(this T? param) => param is null;

    public static bool IsNotNull<T>(this T? param) => param is not null;

    public static bool IsNullOrDefault<T>(this T? param) => param is null || param.Equals(default);

    public static bool IsNullOrEmpty<T>(this IEnumerable<T?>? src) => src is null || !src.Any();

    public static T IfNull<T>(this T? value, T defaultValue) =>
        value is null ? defaultValue : value;
}
