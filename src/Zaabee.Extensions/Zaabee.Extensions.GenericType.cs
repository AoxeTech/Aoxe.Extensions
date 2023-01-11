namespace Zaabee.Extensions;

public static partial class ZaabeeExtension
{
#if NETSTANDARD2_0
    public static Dictionary<string, object> ConvertToDictionary<T>(this T obj) =>
#else
    public static Dictionary<string, object?> ConvertToDictionary<T>(this T obj) =>
#endif
        obj?.GetType()
            .GetProperties()
            .ToDictionary(p => p.Name, p => p.GetValue(obj, null))
        ??
#if NETSTANDARD2_0
        new Dictionary<string, object>();
#else
        new Dictionary<string, object?>();
#endif
}