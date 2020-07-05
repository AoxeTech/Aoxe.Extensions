using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Reflection;

namespace Zaabee.Extensions
{
    public static class EnumExtension
    {
        private static readonly ConcurrentDictionary<Enum, string> DescriptionCache =
            new ConcurrentDictionary<Enum, string>();

        private static readonly ConcurrentDictionary<Tuple<Enum, string>, string> DescriptionsCache =
            new ConcurrentDictionary<Tuple<Enum, string>, string>();

        public static string GetDescription(this Enum enumerationValue) =>
            DescriptionCache.GetOrAdd(enumerationValue, key =>
            {
                var field = key.GetType().GetField(key.ToString());
                return field is null ? key.GetDescriptions() : GetDescription(field);
            });

        public static string GetDescriptions(this Enum enumerationValue, string separator = ",") =>
            DescriptionsCache.GetOrAdd(new Tuple<Enum, string>(enumerationValue, separator), _ =>
            {
                var names = enumerationValue.ToString().Split(',');
                var res = new string[names.Length];
                var type = enumerationValue.GetType();
                for (var i = 0; i < names.Length; i++)
                {
                    var field = type.GetField(names[i].Trim());
                    if (field is null) continue;
                    res[i] = GetDescription(field);
                }

                return res.StringJoin(separator);
            });

        private static string GetDescription(MemberInfo field)
        {
            var att = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute), false);
            return att is null ? field.Name : ((DescriptionAttribute) att).Description;
        }
    }
}