namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    /// <summary>
    /// Creates an instance of the specified type using the parameterless constructor
    /// </summary>
    /// <param name="type">The type to create</param>
    /// <param name="args">Constructor arguments</param>
    /// <returns>A new instance of the specified type</returns>
    /// <exception cref="ArgumentNullException">Thrown when type is null</exception>
    public static object? CreateInstance(this Type type, params object?[]? args)
    {
        if (type is null)
            throw new ArgumentNullException(nameof(type));

        return Activator.CreateInstance(type, args);
    }

    /// <summary>
    /// Determines if the specified type is a nullable value type
    /// </summary>
    /// <param name="type">The type to check</param>
    /// <returns>True if the type is a nullable value type</returns>
    public static bool IsNullableType(this Type? type) =>
        type?.IsGenericType is true && type.GetGenericTypeDefinition() == typeof(Nullable<>);

    /// <summary>
    /// Determines if the specified type is a numeric type
    /// </summary>
    /// <param name="type">The type to check</param>
    /// <returns>True if the type is a built-in numeric type</returns>
    public static bool IsNumericType(this Type type) =>
        Type.GetTypeCode(Nullable.GetUnderlyingType(type) ?? type) switch
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
