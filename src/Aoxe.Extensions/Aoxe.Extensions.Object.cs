namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    public static T CastTo<T>(this object obj) => (T)obj;

#if NETSTANDARD2_0
    public static Dictionary<string, object> AsDictionary(
#else
    public static Dictionary<string, object?> AsDictionary(
#endif
        this object source,
        BindingFlags bindingAttr =
            BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance
    ) =>
        source
            .GetType()
            .GetProperties(bindingAttr)
            .ToDictionary(propInfo => propInfo.Name, propInfo => propInfo.GetValue(source, null));
}
