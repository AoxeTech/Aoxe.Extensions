using System;
using Xunit;

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

        [Fact]
        public void IsNumericTypeTest()
        {
            Assert.True(typeof(sbyte).IsNumericType());
            Assert.True(typeof(byte).IsNumericType());
            Assert.True(typeof(short).IsNumericType());
            Assert.True(typeof(int).IsNumericType());
            Assert.True(typeof(long).IsNumericType());
            Assert.True(typeof(float).IsNumericType());
            Assert.True(typeof(double).IsNumericType());
            Assert.True(typeof(decimal).IsNumericType());
            Assert.True(typeof(ushort).IsNumericType());
            Assert.True(typeof(uint).IsNumericType());
            Assert.True(typeof(ulong).IsNumericType());
            Assert.True(typeof(TestEnum).IsNumericType());
            
            Assert.False(typeof(bool).IsNumericType());
            Assert.False(typeof(char).IsNumericType());
            Assert.False(typeof(string).IsNumericType());
            Assert.False(typeof(TestModel).IsNumericType());

            Type nullType = null;
            Assert.False(nullType.IsNumericType());
        }
    }
}