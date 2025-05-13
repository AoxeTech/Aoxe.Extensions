namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionsStringTest
{
    #region TrimStart Tests

    /// <summary>
    /// Verify that TrimStart removes multiple leading occurrences
    /// </summary>
    [Fact]
    public void TrimStart_WhenMultipleLeadingOccurrences_RemovesAll()
    {
        // Arrange
        var input = "TestTestHello";

        // Act
        var result = input.TrimStart("Test");

        // Assert
        Assert.Equal("Hello", result);
    }

    /// <summary>
    /// Verify that TrimStart handles case-insensitive comparison
    /// </summary>
    [Fact]
    public void TrimStart_WhenUsingIgnoreCase_RemovesCaseInsensitive()
    {
        // Arrange
        var input = "TESTValue";

        // Act
        var result = input.TrimStart("test", StringComparison.OrdinalIgnoreCase);

        // Assert
        Assert.Equal("Value", result);
    }

    /// <summary>
    /// Verify that TrimStart returns original when trimString not found
    /// </summary>
    [Fact]
    public void TrimStart_WhenNoMatchFound_ReturnsOriginal()
    {
        // Arrange
        var input = "HelloWorld";

        // Act
        var result = input.TrimStart("Test");

        // Assert
        Assert.Equal(input, result);
    }

    #endregion

    #region TrimEnd Tests

    /// <summary>
    /// Verify that TrimEnd removes multiple trailing occurrences
    /// </summary>
    [Fact]
    public void TrimEnd_WhenMultipleTrailingOccurrences_RemovesAll()
    {
        // Arrange
        var input = "HelloWorldWorld";

        // Act
        var result = input.TrimEnd("World");

        // Assert
        Assert.Equal("Hello", result);
    }

    /// <summary>
    /// Verify that TrimEnd handles empty trimString correctly
    /// </summary>
    [Fact]
    public void TrimEnd_WhenTrimStringEmpty_ReturnsOriginal()
    {
        // Arrange
        var input = "Hello";

        // Act
        var result = input.TrimEnd("");

        // Assert
        Assert.Equal(input, result);
    }

    #endregion

    #region Truncate Tests

    /// <summary>
    /// Verify that Truncate shortens string with suffix when over maxLength
    /// </summary>
    [Fact]
    public void Truncate_WhenLongerThanMaxLength_ReturnsTruncatedWithSuffix()
    {
        // Arrange
        var input = "This is a long string";

        // Act
        var result = input.Truncate(10);

        // Assert
        Assert.Equal("This is...", result);
    }

    /// <summary>
    /// Verify that Truncate throws for non-positive maxLength
    /// </summary>
    [Fact]
    public void Truncate_WhenMaxLengthZero_ThrowsArgumentException()
    {
        // Arrange
        var input = "Test";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => input.Truncate(0));
    }

    /// <summary>
    /// Verify that Truncate handles suffix longer than maxLength
    /// </summary>
    [Fact]
    public void Truncate_WhenMaxLengthShorterThanSuffix_ReturnsPartialSuffix()
    {
        // Arrange
        var input = "Hello";

        // Act
        var result = input.Truncate(2, "...");

        // Assert
        Assert.Equal("..", result);
    }

    #endregion

    #region SafeReplace Tests

    /// <summary>
    /// Verify that SafeReplace replaces multiple occurrences
    /// </summary>
    [Fact]
    public void SafeReplace_WhenMultipleMatches_ReplacesAll()
    {
        // Arrange
        var input = "a1a2a3";

        // Act
        var result = input.SafeReplace("a", "X");

        // Assert
        Assert.Equal("X1X2X3", result);
    }

    /// <summary>
    /// Verify that SafeReplace handles case-sensitive replacement
    /// </summary>
    [Fact]
    public void SafeReplace_WhenCaseSensitive_OnlyReplacesExactMatches()
    {
        // Arrange
        var input = "Apple apple APPLE";

        // Act
        var result = input.SafeReplace("apple", "X", StringComparison.OrdinalIgnoreCase);

        // Assert
        Assert.Equal("X X X", result);
    }

    #endregion

    #region JoinToString Tests

    /// <summary>
    /// Verify that JoinToString returns default for empty collection
    /// </summary>
    [Fact]
    public void JoinToString_WhenEmptyCollection_ReturnsDefault()
    {
        // Arrange
        IEnumerable<string>? items = null;

        // Act
        var result = items.JoinToString(",", "N/A");

        // Assert
        Assert.Equal("N/A", result);
    }

    /// <summary>
    /// Verify that JoinToString handles normal collection
    /// </summary>
    [Fact]
    public void JoinToString_WhenValidCollection_ReturnsJoinedString()
    {
        // Arrange
        var items = new List<int> { 1, 2, 3 };

        // Act
        var result = items.JoinToString("-");

        // Assert
        Assert.Equal("1-2-3", result);
    }

    #endregion

    #region FilterLettersAndDigits Tests

    /// <summary>
    /// Verify that FilterLettersAndDigits removes special characters
    /// </summary>
    [Fact]
    public void FilterLettersAndDigits_WhenContainsSpecialChars_RemovesThem()
    {
        // Arrange
        var input = "A1!@#B2";

        // Act
        var result = input.FilterLettersAndDigits();

        // Assert
        Assert.Equal("A1B2", result);
    }

    #endregion

    #region Edge Case Tests

    /// <summary>
    /// Verify that all methods handle null input gracefully
    /// </summary>
    [Fact]
    public void Methods_WhenNullInput_HandleGracefully()
    {
        // Arrange
        string? nullString = null;

        // Act & Assert
        Assert.Null(nullString.TrimStart("test"));
        Assert.Null(nullString.Truncate(5));
        Assert.True(nullString.IsNullOrEmpty());
        Assert.True(nullString.IsNullOrWhiteSpace());
    }

    #endregion
}
