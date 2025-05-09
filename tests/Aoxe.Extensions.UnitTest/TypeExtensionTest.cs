namespace Aoxe.Extensions.UnitTest;

public class TypeExtensionTest
{
    [Fact]
    public void TestReferenceTypeCreateInstance()
    {
        Assert.NotNull(typeof(object).CreateInstance());
        Assert.NotNull(typeof(TestModel).CreateInstance());
    }

    [Theory]
    [InlineData(default(bool), typeof(bool))]
    [InlineData(default(short), typeof(short))]
    [InlineData(default(ushort), typeof(ushort))]
    [InlineData(default(int), typeof(int))]
    [InlineData(default(uint), typeof(uint))]
    [InlineData(default(long), typeof(long))]
    [InlineData(default(ulong), typeof(ulong))]
    [InlineData(default(float), typeof(float))]
    [InlineData(default(double), typeof(double))]
    [InlineData(default(TestEnum), typeof(TestEnum))]
    [InlineData(default(char), typeof(char))]
    public void TestValueTypeCreateInstance(object? value, Type type)
    {
        Assert.Equal(value, type.CreateInstance());
    }

    [Fact]
    public void TestNullableValueTypeCreateInstance()
    {
        Assert.Equal(default(bool?), typeof(bool?).CreateInstance());
        Assert.Equal(default(short?), typeof(short?).CreateInstance());
        Assert.Equal(default(ushort?), typeof(ushort?).CreateInstance());
        Assert.Equal(default(int?), typeof(int?).CreateInstance());
        Assert.Equal(default(uint?), typeof(uint?).CreateInstance());
        Assert.Equal(default(long?), typeof(long?).CreateInstance());
        Assert.Equal(default(ulong?), typeof(ulong?).CreateInstance());
        Assert.Equal(default(float?), typeof(float?).CreateInstance());
        Assert.Equal(default(double?), typeof(double?).CreateInstance());
        Assert.Equal(default(decimal?), typeof(decimal?).CreateInstance());
        Assert.Equal(default(TestEnum?), typeof(TestEnum?).CreateInstance());
        Assert.Equal(default(char?), typeof(char?).CreateInstance());
    }

    [Theory]
    [InlineData(typeof(sbyte), false)]
    [InlineData(typeof(byte), false)]
    [InlineData(typeof(short), false)]
    [InlineData(typeof(int), false)]
    [InlineData(typeof(long), false)]
    [InlineData(typeof(float), false)]
    [InlineData(typeof(double), false)]
    [InlineData(typeof(decimal), false)]
    [InlineData(typeof(ushort), false)]
    [InlineData(typeof(uint), false)]
    [InlineData(typeof(ulong), false)]
    [InlineData(typeof(TestEnum), false)]
    [InlineData(typeof(bool), false)]
    [InlineData(typeof(char), false)]
    [InlineData(typeof(string), false)]
    [InlineData(typeof(TestModel), false)]
    [InlineData(typeof(sbyte?), true)]
    [InlineData(typeof(byte?), true)]
    [InlineData(typeof(short?), true)]
    [InlineData(typeof(int?), true)]
    [InlineData(typeof(long?), true)]
    [InlineData(typeof(float?), true)]
    [InlineData(typeof(double?), true)]
    [InlineData(typeof(decimal?), true)]
    [InlineData(typeof(ushort?), true)]
    [InlineData(typeof(uint?), true)]
    [InlineData(typeof(ulong?), true)]
    [InlineData(typeof(TestEnum?), true)]
    [InlineData(typeof(bool?), true)]
    [InlineData(typeof(char?), true)]
    [InlineData(null, false)]
    public void IsNullableTypeTest(Type? type, bool result)
    {
        Assert.Equal(type.IsNullableType(), result);
    }

    [Theory]
    [InlineData(typeof(sbyte), true)]
    [InlineData(typeof(byte), true)]
    [InlineData(typeof(short), true)]
    [InlineData(typeof(ushort), true)]
    [InlineData(typeof(int), true)]
    [InlineData(typeof(uint), true)]
    [InlineData(typeof(long), true)]
    [InlineData(typeof(ulong), true)]
    [InlineData(typeof(float), true)]
    [InlineData(typeof(double), true)]
    [InlineData(typeof(decimal), true)]
    [InlineData(typeof(TestEnum), true)]
    [InlineData(typeof(bool), false)]
    [InlineData(typeof(char), false)]
    [InlineData(typeof(string), false)]
    [InlineData(typeof(TestModel), false)]
    public void IsNumericTypeTest(Type type, bool result)
    {
        Assert.Equal(type.IsNumericType(), result);
    }
}
