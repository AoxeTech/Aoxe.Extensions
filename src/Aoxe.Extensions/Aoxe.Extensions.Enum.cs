namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    private static readonly ConcurrentDictionary<Enum, string> DescriptionCache = new();
    private static readonly ConcurrentDictionary<Tuple<Enum, string>, string> DescriptionsCache =
        new();

    public static string GetDescription(this Enum enumerationValue) =>
        DescriptionCache.GetOrAdd(
            enumerationValue,
            key =>
            {
                var field = key.GetType().GetField(key.ToString());
                return field?.GetCustomAttribute<DescriptionAttribute>()?.Description
                    ?? key.GetDescriptions();
            }
        );

    public static string GetDescriptions(this Enum enumerationValue, string separator = ", ") =>
        DescriptionsCache.GetOrAdd(
            Tuple.Create(enumerationValue, separator),
            _ => ProcessDescriptions(enumerationValue, separator)
        );

    private static string ProcessDescriptions(Enum enumerationValue, string separator)
    {
        var names = enumerationValue.ToString().Split(',');
        var type = enumerationValue.GetType();
        var descriptions = new string[names.Length];

        for (var i = 0; i < names.Length; i++)
        {
            var field = type.GetField(names[i].Trim());
            descriptions[i] =
                field?.GetCustomAttribute<DescriptionAttribute>()?.Description
                ?? field?.Name
                ?? names[i].Trim();
        }

        return string.Join(separator, descriptions);
    }
}
