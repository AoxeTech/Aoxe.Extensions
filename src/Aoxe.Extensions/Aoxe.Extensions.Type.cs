namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    public static object? CreateInstance(this Type type, params object?[]? args)
    {
        if (type == null)
            throw new ArgumentNullException(nameof(type));
        return Activator.CreateInstance(type, args);
    }

    public static bool IsNullableType(this Type? type) =>
        type?.IsGenericType is true && type.GetGenericTypeDefinition() == typeof(Nullable<>);

    public static bool IsNumericType(this Type type)
    {
        if (type == null)
            throw new ArgumentNullException(nameof(type));
        return Type.GetTypeCode(Nullable.GetUnderlyingType(type) ?? type) switch
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
}
