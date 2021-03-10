using System;
using Xunit;
using Zaabee.Extensions.UnitTest.Commons;

namespace Zaabee.Extensions.UnitTest
{
    public class TypeExtensionTest
    {
        [Fact]
        public void TestReferenceType()
        {
            Assert.Equal(default(object), typeof(object).GetDefaultValue());
            Assert.Equal(default(TestModel), typeof(TestModel).GetDefaultValue());
        }

        [Fact]
        public void TestValueType()
        {
            Assert.Equal(default(bool), typeof(bool).GetDefaultValue());
            Assert.Equal(default(short), typeof(short).GetDefaultValue());
            Assert.Equal(default(ushort), typeof(ushort).GetDefaultValue());
            Assert.Equal(default(int), typeof(int).GetDefaultValue());
            Assert.Equal(default(uint), typeof(uint).GetDefaultValue());
            Assert.Equal(default(long), typeof(long).GetDefaultValue());
            Assert.Equal(default(ulong), typeof(ulong).GetDefaultValue());
            Assert.Equal(default(float), typeof(float).GetDefaultValue());
            Assert.Equal(default(double), typeof(double).GetDefaultValue());
            Assert.Equal(default(decimal), typeof(decimal).GetDefaultValue());
            Assert.Equal(default(TestEnum), typeof(TestEnum).GetDefaultValue());
            Assert.Equal(default(char), typeof(char).GetDefaultValue());
        }

        [Theory]
        [InlineData(typeof(sbyte), true)]
        [InlineData(typeof(byte), true)]
        [InlineData(typeof(short), true)]
        [InlineData(typeof(int), true)]
        [InlineData(typeof(long), true)]
        [InlineData(typeof(float), true)]
        [InlineData(typeof(double), true)]
        [InlineData(typeof(decimal), true)]
        [InlineData(typeof(ushort), true)]
        [InlineData(typeof(uint), true)]
        [InlineData(typeof(ulong), true)]
        [InlineData(typeof(TestEnum), true)]
        [InlineData(typeof(bool), false)]
        [InlineData(typeof(char), false)]
        [InlineData(typeof(string), false)]
        [InlineData(typeof(TestModel), false)]
        public void IsNumericTypeTest(Type type, bool result)
        {
            Assert.Equal(type.IsNumericType(), result);

            Type nullType = null;
            Assert.False(nullType.IsNumericType());
        }
    }
}