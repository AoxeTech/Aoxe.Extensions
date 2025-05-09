namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    public static bool IsNull<T>(this T? param) => param is null;

    public static bool IsNotNull<T>(this T? param) => param is not null;

    public static bool IsNullOrDefault<T>(this T? param)
    {
        if (param is null)
            return true;

        var underlyingType = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);

        var defaultValue = underlyingType.IsValueType
            ? Activator.CreateInstance(underlyingType)
            : null;

        return param.Equals(defaultValue);
    }

    public static bool IsNullOrEmpty<T>(this IEnumerable<T?>? src) => src?.Any() is not true;

    public static T IfNull<T>(this T? value, T defaultValue) =>
        value is null ? defaultValue : value;
}
