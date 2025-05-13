using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Reflection;

namespace Aoxe.Extensions;

/// <summary>
/// Provides extension methods for working with enumerated types (enums) and their descriptions.
/// </summary>
public static partial class AoxeExtension
{
    private static readonly ConcurrentDictionary<Enum, string> DescriptionCache = new();
    private static readonly ConcurrentDictionary<Tuple<Enum, string>, string> DescriptionsCache =
        new();

    /// <summary>
    /// Gets the description of an enum value using its <see cref="DescriptionAttribute"/>.
    /// </summary>
    /// <param name="enumerationValue">The enum value to get the description for.</param>
    /// <returns>
    /// The description from the <see cref="DescriptionAttribute"/> if present;
    /// otherwise, the enum name(s) for combined flags or the string representation.
    /// </returns>
    /// <remarks>
    /// <para>Behavior:</para>
    /// <list type="bullet">
    /// <item><description>For single enum values: Returns the Description attribute or field name</description></item>
    /// <item><description>For combined flags: Returns comma-separated descriptions of individual flags</description></item>
    /// <item><description>Results are cached for performance</description></item>
    /// </list>
    /// <example>
    /// <code>
    /// [Description("Active State")]
    /// Active,
    ///
    /// var description = Status.Active.GetDescription(); // "Active State"
    /// </code>
    /// </example>
    /// </remarks>
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

    /// <summary>
    /// Gets the descriptions of an enum value's individual flags using <see cref="DescriptionAttribute"/>.
    /// </summary>
    /// <param name="enumerationValue">The enum value to get descriptions for.</param>
    /// <param name="separator">The string separator to use between descriptions.</param>
    /// <returns>
    /// A string containing the descriptions of all set flags joined by the specified separator.
    /// </returns>
    /// <remarks>
    /// <para>Key features:</para>
    /// <list type="bullet">
    /// <item><description>Handles both single values and combined flags</description></item>
    /// <item><description>Trims whitespace from enum names</description></item>
    /// <item><description>Uses field name if no Description attribute exists</description></item>
    /// <item><description>Results are cached per separator</description></item>
    /// </list>
    /// <example>
    /// <code>
    /// [Flags]
    /// enum Permissions {
    ///     [Description("Read Access")] Read,
    ///     [Description("Write Access")] Write
    /// }
    ///
    /// var value = Permissions.Read | Permissions.Write;
    /// var result = value.GetDescriptions("; "); // "Read Access; Write Access"
    /// </code>
    /// </example>
    /// </remarks>
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
