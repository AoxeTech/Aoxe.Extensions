namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionsTypeTests
{
    #region CreateInstance Tests

    private class TestClass { }

    private struct TestStruct { }

    private class ParameterizedClass
    {
        public int Value { get; }

        public ParameterizedClass(int value) => Value = value;
    }

    [Fact]
    public void CreateInstance_ParameterlessConstructor_ReturnsInstance()
    {
        // Arrange
        var type = typeof(TestClass);

        // Act
        var instance = type.CreateInstance();

        // Assert
        Assert.NotNull(instance);
        Assert.IsType<TestClass>(instance);
    }

    [Fact]
    public void CreateInstance_WithParameters_ReturnsInitializedObject()
    {
        // Arrange
        var type = typeof(ParameterizedClass);
        const int expectedValue = 42;

        // Act
        var instance = (ParameterizedClass)type.CreateInstance(expectedValue)!;

        // Assert
        Assert.NotNull(instance);
        Assert.Equal(expectedValue, instance.Value);
    }

    [Fact]
    public void CreateInstance_StructType_ReturnsDefaultInstance()
    {
        // Arrange
        var type = typeof(TestStruct);

        // Act
        var instance = type.CreateInstance();

        // Assert
        Assert.IsType<TestStruct>(instance);
        Assert.Equal(default(TestStruct), instance);
    }

    [Fact]
    public void CreateInstance_NullType_ThrowsArgumentNullException()
    {
        // Arrange
        Type? type = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => type!.CreateInstance());
    }

    [Fact]
    public void CreateInstance_InvalidArguments_ThrowsMissingMethodException()
    {
        // Arrange
        var type = typeof(TestClass);

        // Act & Assert
        Assert.Throws<MissingMethodException>(() => type.CreateInstance("invalid argument"));
    }

    #endregion

    #region IsNullableType Tests

    [Fact]
    public void IsNullableType_WithNullableInt_ReturnsTrue()
    {
        // Arrange
        var type = typeof(int?);

        // Act & Assert
        Assert.True(type.IsNullableType());
    }

    [Fact]
    public void IsNullableType_WithNonNullableValueType_ReturnsFalse()
    {
        // Arrange
        var type = typeof(DateTime);

        // Act & Assert
        Assert.False(type.IsNullableType());
    }

    [Fact]
    public void IsNullableType_WithReferenceType_ReturnsFalse()
    {
        // Arrange
        var type = typeof(string);

        // Act & Assert
        Assert.False(type.IsNullableType());
    }

    [Fact]
    public void IsNullableType_WithGenericCollection_ReturnsFalse()
    {
        // Arrange
        var type = typeof(List<string>);

        // Act & Assert
        Assert.False(type.IsNullableType());
    }

    [Fact]
    public void IsNullableType_WithNullType_ReturnsFalse()
    {
        // Arrange
        Type? type = null;

        // Act & Assert
        Assert.False(type.IsNullableType());
    }

    #endregion

    #region IsNumericType Tests

    [Theory]
    [InlineData(typeof(byte), true)]
    [InlineData(typeof(sbyte), true)]
    [InlineData(typeof(short), true)]
    [InlineData(typeof(ushort), true)]
    [InlineData(typeof(int), true)]
    [InlineData(typeof(uint), true)]
    [InlineData(typeof(long), true)]
    [InlineData(typeof(ulong), true)]
    [InlineData(typeof(float), true)]
    [InlineData(typeof(double), true)]
    [InlineData(typeof(decimal), true)]
    [InlineData(typeof(int?), true)]
    [InlineData(typeof(double?), true)]
    [InlineData(typeof(string), false)]
    [InlineData(typeof(DateTime), false)]
    [InlineData(typeof(bool), false)]
    [InlineData(typeof(object), false)]
    [InlineData(typeof(TestEnum), true)]
    public void IsNumericType_ValidatesVariousTypes(Type type, bool expected)
    {
        // Act
        var result = type.IsNumericType();

        // Assert
        Assert.Equal(expected, result);
    }

    private enum TestEnum
    {
        Value
    }

    [Fact]
    public void IsNumericType_WithNullableDecimal_ReturnsTrue()
    {
        // Arrange
        var type = typeof(decimal?);

        // Act & Assert
        Assert.True(type.IsNumericType());
    }

    [Fact]
    public void IsNumericType_WithUnderlyingNullableType_ReturnsTrue()
    {
        // Arrange
        var type = typeof(int?);
        var underlyingType = Nullable.GetUnderlyingType(type);

        // Act & Assert
        Assert.True(underlyingType!.IsNumericType());
    }

    #endregion
}
