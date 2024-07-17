namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
#if NETSTANDARD2_0
    public static dynamic DictionaryToDynamic(this IDictionary<string, object> dictionary)
#else
    public static dynamic DictionaryToDynamic(this IDictionary<string, object?> dictionary)
#endif
    {
        var expandoObj = new ExpandoObject();
#if NETSTANDARD2_0
        var expandoObjCollection = (ICollection<KeyValuePair<string, object>>)expandoObj;
#else
        var expandoObjCollection = (ICollection<KeyValuePair<string, object?>>)expandoObj;
#endif
        expandoObjCollection.AddRange(dictionary);
        dynamic eoDynamic = expandoObj;
        return eoDynamic;
    }

    public static T ToGeneric<T>(this IDictionary<string, object?> source)
        where T : class, new()
    {
        var someObject = new T();
        var someObjectType = someObject.GetType();

        foreach (var keyValuePare in source)
        {
            var targetProperty = someObjectType.GetProperty(keyValuePare.Key);
            if (targetProperty is null)
                continue;

            if (targetProperty.PropertyType == keyValuePare.Value?.GetType())
                targetProperty.SetValue(someObject, keyValuePare.Value);
            else
            {
                var parseMethod = targetProperty
                    .PropertyType
                    .GetMethod(
                        "TryParse",
                        BindingFlags.Public | BindingFlags.Static,
                        null,
                        new[] { typeof(string), targetProperty.PropertyType.MakeByRefType() },
                        null
                    );

                if (parseMethod is null)
                    continue;
                var parameters = new[] { keyValuePare.Value, null };
                var success = (bool)(parseMethod.Invoke(null, parameters) ?? false);
                if (success)
                    targetProperty.SetValue(someObject, parameters[1]);
            }
        }

        return someObject;
    }
}
