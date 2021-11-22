namespace Zaabee.Extensions;

public static partial class ZaabeeExtension
{
    private static readonly ConcurrentDictionary<Type, object?> ValueTypeCache = new();

    public static object? GetDefaultValue(this Type type) =>
        type.IsValueType
            ? ValueTypeCache.GetOrAdd(type, Activator.CreateInstance)
            : null;

    public static bool IsNumericType(this Type? type) =>
        Type.GetTypeCode(type) switch
        {
            TypeCode.Byte => true,
            TypeCode.SByte => true,
            TypeCode.UInt16 => true,
            TypeCode.UInt32 => true,
            TypeCode.UInt64 => true,
            TypeCode.Int16 => true,
            TypeCode.Int32 => true,
            TypeCode.Int64 => true,
            TypeCode.Decimal => true,
            TypeCode.Double => true,
            TypeCode.Single => true,
            _ => false
        };
}