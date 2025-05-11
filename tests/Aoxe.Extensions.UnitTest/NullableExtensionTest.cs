namespace Aoxe.Extensions.UnitTest;

public class ObjectExtensionsTests
{
    public class IsNullTests
    {
        [Fact]
        public void IsNull_WithNullReference_ReturnsTrue()
        {
            string? value = null;
            Assert.True(value.IsNull());
        }

        [Fact]
        public void IsNull_WithNonNullReference_ReturnsFalse()
        {
            string value = "test";
            Assert.False(value.IsNull());
        }

        [Fact]
        public void IsNull_WithNullableValueNull_ReturnsTrue()
        {
            int? value = null;
            Assert.True(value.IsNull());
        }

        [Fact]
        public void IsNull_WithNullableValueNotNull_ReturnsFalse()
        {
            int? value = 5;
            Assert.False(value.IsNull());
        }
    }

    public class IsNotNullTests
    {
        [Fact]
        public void IsNotNull_WithNullReference_ReturnsFalse()
        {
            string? value = null;
            Assert.False(value.IsNotNull());
        }

        [Fact]
        public void IsNotNull_WithNonNullReference_ReturnsTrue()
        {
            string value = "test";
            Assert.True(value.IsNotNull());
        }
    }

    public class IsNullOrDefaultTests
    {
        [Theory]
        [InlineData(null, true)] // Null string
        [InlineData("", false)] // Non-null string (default for string is null)
        [InlineData("test", false)] // Non-default string
        public void IsNullOrDefault_StringTests(string? value, bool expected)
        {
            Assert.Equal(expected, value.IsNullOrDefault());
        }

        [Theory]
        [InlineData(null, true)] // Null int?
        [InlineData(0, true)] // Default int value
        [InlineData(5, false)] // Non-default int value
        public void IsNullOrDefault_NullableIntTests(int? value, bool expected)
        {
            Assert.Equal(expected, value.IsNullOrDefault());
        }

        [Fact]
        public void IsNullOrDefault_DateTimeDefault_ReturnsTrue()
        {
            DateTime? value = null;
            Assert.True(value.IsNullOrDefault());
        }

        [Fact]
        public void IsNullOrDefault_DateTimeNow_ReturnsFalse()
        {
            DateTime? value = DateTime.Now;
            Assert.False(value.IsNullOrDefault());
        }
    }

    public class IsNullOrEmptyTests
    {
        [Fact]
        public void IsNullOrEmpty_WithNullEnumerable_ReturnsTrue()
        {
            IEnumerable<int>? value = null;
            Assert.True(value.IsNullOrEmpty());
        }

        [Fact]
        public void IsNullOrEmpty_WithEmptyList_ReturnsTrue()
        {
            var value = new List<int>();
            Assert.True(value.IsNullOrEmpty());
        }

        [Fact]
        public void IsNullOrEmpty_WithPopulatedArray_ReturnsFalse()
        {
            var value = new[] { 1, 2, 3 };
            Assert.False(value.IsNullOrEmpty());
        }

        [Fact]
        public void IsNullOrEmpty_WithNonEmptyCollection_ReturnsFalse()
        {
            var value = new Queue<string>(["test"]);
            Assert.False(value.IsNullOrEmpty());
        }
    }

    public class IfNullTests
    {
        [Fact]
        public void IfNull_WithNullReference_ReturnsDefaultValue()
        {
            string? value = null;
            var defaultValue = "default";
            Assert.Equal(defaultValue, value.IfNull(defaultValue));
        }

        [Fact]
        public void IfNull_WithNonNullReference_ReturnsOriginalValue()
        {
            string value = "original";
            var defaultValue = "default";
            Assert.Equal(value, value.IfNull(defaultValue));
        }

        [Fact]
        public void IfNull_WithNullableValueNull_ReturnsDefaultValue()
        {
            int? value = null;
            var defaultValue = 10;
            Assert.Equal(defaultValue, value.IfNull(defaultValue));
        }

        [Fact]
        public void IfNull_WithNullableValueNotNull_ReturnsOriginalValue()
        {
            int? value = 5;
            var defaultValue = 10;
            Assert.Equal(value, value.IfNull(defaultValue));
        }

        [Fact]
        public void IfNull_WithValueType_ReturnsOriginalValue()
        {
            int value = 7;
            var defaultValue = 10;
            Assert.Equal(value, value.IfNull(defaultValue));
        }
    }
}
