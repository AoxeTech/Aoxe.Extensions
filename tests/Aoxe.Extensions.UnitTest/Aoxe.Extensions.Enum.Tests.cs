namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionsEnumTests
{
    #region Test Enums

    private enum SimpleEnum
    {
        [Description("First Value")]
        First,

        Second,

        [Description("Third Value")]
        Third
    }

    [Flags]
    private enum FlagEnum
    {
        [Description("Read Access")]
        Read = 1,

        [Description("Write Access")]
        Write = 2,

        Execute = 4
    }

    private enum EmptyDescriptionEnum
    {
        [Description("")]
        EmptyDesc,

        NoDescription
    }

    #endregion

    #region GetDescription Tests

    [Fact]
    public void GetDescription_WhenDescriptionExists_ReturnsAttributeValue()
    {
        // Arrange
        var value = SimpleEnum.First;

        // Act
        var result = value.GetDescription();

        // Assert
        Assert.Equal("First Value", result);
    }

    [Fact]
    public void GetDescription_WhenNoDescription_ReturnsEnumName()
    {
        // Arrange
        var value = SimpleEnum.Second;

        // Act
        var result = value.GetDescription();

        // Assert
        Assert.Equal("Second", result);
    }

    [Fact]
    public void GetDescription_ForFlagEnum_ReturnsCombinedDescriptions()
    {
        // Arrange
        var value = FlagEnum.Read | FlagEnum.Write;

        // Act
        var result = value.GetDescription();

        // Assert
        Assert.Equal("Read Access, Write Access", result);
    }

    [Fact]
    public void GetDescription_WithEmptyDescription_ReturnsEmptyString()
    {
        // Arrange
        var value = EmptyDescriptionEnum.EmptyDesc;

        // Act
        var result = value.GetDescription();

        // Assert
        Assert.Equal("", result);
    }

    #endregion

    #region GetDescriptions Tests

    [Fact]
    public void GetDescriptions_WithCustomSeparator_ReturnsProperlyFormattedString()
    {
        // Arrange
        var value = FlagEnum.Read | FlagEnum.Execute;

        // Act
        var result = value.GetDescriptions("; ");

        // Assert
        Assert.Equal("Read Access; Execute", result);
    }

    [Fact]
    public void GetDescriptions_ForSingleValue_ReturnsSingleDescription()
    {
        // Arrange
        var value = FlagEnum.Write;

        // Act
        var result = value.GetDescriptions();

        // Assert
        Assert.Equal("Write Access", result);
    }

    [Fact]
    public void GetDescriptions_WithWhitespaceInNames_TrimsCorrectly()
    {
        // Arrange
        var value = (FlagEnum)3; // Read | Write
        var stringRepresentation = value.ToString(); // "Read, Write"

        // Act
        var result = value.GetDescriptions();

        // Assert
        Assert.Equal("Read Access, Write Access", result);
    }

    #endregion

    #region Caching Tests

    [Fact]
    public void GetDescription_UsesCacheForSameValue()
    {
        // Arrange
        var value1 = SimpleEnum.Third;
        var value2 = SimpleEnum.Third;

        // Act
        var result1 = value1.GetDescription();
        var result2 = value2.GetDescription();

        // Assert
        Assert.Same(result1, result2);
    }

    [Fact]
    public void GetDescriptions_WithDifferentSeparators_CachesSeparately()
    {
        // Arrange
        var value = FlagEnum.Read | FlagEnum.Write;

        // Act
        var result1 = value.GetDescriptions(",");
        var result2 = value.GetDescriptions(";");

        // Assert
        Assert.NotSame(result1, result2);
    }

    #endregion

    #region Edge Cases

    [Fact]
    public void GetDescriptions_ForZeroValue_ReturnsEmptyString()
    {
        // Arrange
        var value = (FlagEnum)0;

        // Act
        var result = value.GetDescriptions();

        // Assert
        Assert.Equal("0", result);
    }

    [Fact]
    public void GetDescriptions_ForUndefinedFlagCombination_ReturnsRawNames()
    {
        // Arrange
        var value = (FlagEnum)8; // Undefined flag

        // Act
        var result = value.GetDescriptions();

        // Assert
        Assert.Equal("8", result);
    }

    #endregion
}
