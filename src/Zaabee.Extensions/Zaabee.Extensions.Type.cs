namespace Zaabee.Extensions;

public static partial class ZaabeeExtension
{
    public static object? CreateInstance(this Type type, params object?[]? args) =>
        Activator.CreateInstance(type, args);

    public static bool IsNullableType(this Type? type) =>
        type is not null
        && type.IsGenericType
        && type.GetGenericTypeDefinition() == typeof(Nullable<>);

    public static bool IsNumericType(this Type type) =>
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