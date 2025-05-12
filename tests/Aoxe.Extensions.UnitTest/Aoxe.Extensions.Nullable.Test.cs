namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionsNullableTest
{
    #region IsNull Tests

    [Theory]
    [InlineData(null, true)]
    [InlineData("test", false)]
    [InlineData(0, false)]
    public void IsNull_ReturnsExpectedResult(object value, bool expected)
    {
        // Act
        var result = value.IsNull();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void IsNull_WithNullableValueType_WhenNull_ReturnsTrue()
    {
        // Arrange
        int? nullInt = null;

        // Act & Assert
        Assert.True(nullInt.IsNull());
    }

    #endregion

    #region IsNotNull Tests

    [Theory]
    [InlineData(null, false)]
    [InlineData("test", true)]
    [InlineData(0, true)]
    public void IsNotNull_ReturnsExpectedResult(object value, bool expected)
    {
        // Act
        var result = value.IsNotNull();

        // Assert
        Assert.Equal(expected, result);
    }

    #endregion

    #region IsNullOrDefault Tests

    [Theory]
    [InlineData(null, true)]
    [InlineData(0, true)]
    [InlineData(1, false)]
    public void IsNullOrDefault_WithInt32_ReturnsExpected(int? value, bool expected)
    {
        // Act
        var result = value.IsNullOrDefault();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void IsNullOrDefault_WithReferenceType_WhenNull_ReturnsTrue()
    {
        // Arrange
        string nullString = null;

        // Act & Assert
        Assert.True(nullString.IsNullOrDefault());
    }

    [Fact]
    public void IsNullOrDefault_WithDateTime_ReturnsExpected()
    {
        // Arrange
        var defaultDateTime = default(DateTime);
        var nonDefaultDateTime = DateTime.Now;

        // Act & Assert
        Assert.True(defaultDateTime.IsNullOrDefault());
        Assert.False(nonDefaultDateTime.IsNullOrDefault());
    }

    #endregion

    #region IsNullOrEmpty Tests

    [Theory]
    [InlineData(null, true)]
    [InlineData(new int[0], true)]
    [InlineData(new[] { 1 }, false)]
    public void IsNullOrEmpty_WithCollections_ReturnsExpected(
        IEnumerable<int> collection,
        bool expected
    )
    {
        // Act
        var result = collection.IsNullOrEmpty();

        // Assert
        Assert.Equal(expected, result);
    }

    #endregion

    #region IfNull Tests

    [Fact]
    public void IfNull_WhenValueIsNull_ReturnsDefault()
    {
        // Arrange
        string nullValue = null;
        const string defaultValue = "default";

        // Act
        var result = nullValue.IfNull(defaultValue);

        // Assert
        Assert.Equal(defaultValue, result);
    }

    [Fact]
    public void IfNull_WhenValueIsNotNull_ReturnsOriginal()
    {
        // Arrange
        const string value = "original";
        const string defaultValue = "default";

        // Act
        var result = value.IfNull(defaultValue);

        // Assert
        Assert.Equal(value, result);
    }

    [Fact]
    public void IfNull_WithValueType_ReturnsOriginal()
    {
        // Arrange
        int value = 42;
        int defaultValue = 0;

        // Act
        var result = value.IfNull(defaultValue);

        // Assert
        Assert.Equal(value, result);
    }

    #endregion

    #region Edge Cases

    [Fact]
    public void IsNullOrDefault_WithNullableDateTime_WhenNull_ReturnsTrue()
    {
        // Arrange
        DateTime? nullDateTime = null;

        // Act & Assert
        Assert.True(nullDateTime.IsNullOrDefault());
    }

    [Fact]
    public void IsNullOrDefault_WithNullableDateTime_WhenDefault_ReturnsTrue()
    {
        // Arrange
        DateTime? defaultDateTime = default;

        // Act & Assert
        Assert.True(defaultDateTime.IsNullOrDefault());
    }

    #endregion
}
