namespace Aoxe.Extensions.UnitTest;

public class DateTimeOffsetRangeExtensionsTests
{
    private readonly TimeSpan _offset = TimeSpan.FromHours(8);
    private readonly TimeSpan _utcOffset = TimeSpan.Zero;

    #region EachSecondTo Tests

    [Fact]
    public void EachSecondTo_NormalRange_ReturnsCorrectSequence()
    {
        // Arrange
        var start = new DateTimeOffset(2023, 1, 1, 12, 30, 45, 500, _offset);
        var end = start.AddSeconds(2);

        // Act
        var result = start.EachSecondTo(end).ToList();

        // Assert
        Assert.Equal(3, result.Count);
        Assert.Equal(start.TruncateToSecond(), result[0]);
        Assert.Equal(start.TruncateToSecond().AddSeconds(1), result[1]);
        Assert.Equal(end.TruncateToSecond(), result[2]);
        Assert.All(result, d => Assert.Equal(_offset, d.Offset));
    }

    [Fact]
    public void EachSecondTo_CrossTimezone_HandlesOffsetProperly()
    {
        // Arrange
        var start = new DateTimeOffset(2023, 1, 1, 23, 59, 59, 999, _utcOffset);
        var end = new DateTimeOffset(2023, 1, 2, 0, 0, 1, 0, _offset);

        // Act
        var result = start.EachSecondTo(end).ToList();

        // Assert
        Assert.Equal(3, result.Count);
        Assert.Equal(new DateTimeOffset(2023, 1, 1, 23, 59, 59, _utcOffset), result[0]);
        Assert.Equal(new DateTimeOffset(2023, 1, 2, 0, 0, 0, _offset), result[1]);
        Assert.Equal(end.TruncateToSecond(), result[2]);
    }

    #endregion

    #region EachMinuteTo Tests

    [Fact]
    public void EachMinuteTo_PartialMinutes_TruncatesCorrectly()
    {
        // Arrange
        var start = new DateTimeOffset(2023, 3, 1, 14, 25, 30, 500, _offset);
        var end = new DateTimeOffset(2023, 3, 1, 14, 28, 15, 0, _offset);

        // Act
        var result = start.EachMinuteTo(end).ToList();

        // Assert
        Assert.Equal(4, result.Count);
        Assert.Equal(new DateTimeOffset(2023, 3, 1, 14, 25, 0, _offset), result[0]);
        Assert.Equal(new DateTimeOffset(2023, 3, 1, 14, 28, 0, _offset), result[3]);
        Assert.All(result, d => Assert.Equal(0, d.Second));
    }

    #endregion

    #region EachHourTo Tests

    [Fact]
    public void EachHourTo_DaylightSavingTransition_HandlesCorrectly()
    {
        // Arrange (DST end in Pacific Time)
        var start = new DateTimeOffset(2023, 11, 5, 0, 30, 0, TimeSpan.FromHours(-7));
        var end = start.AddHours(3);

        // Act
        var result = start.EachHourTo(end).ToList();

        // Assert
        var expected = new[]
        {
            new DateTimeOffset(2023, 11, 5, 0, 0, 0, TimeSpan.FromHours(-7)),
            new DateTimeOffset(2023, 11, 5, 1, 0, 0, TimeSpan.FromHours(-7)),
            new DateTimeOffset(2023, 11, 5, 2, 0, 0, TimeSpan.FromHours(-8)), // Fall back
            new DateTimeOffset(2023, 11, 5, 3, 0, 0, TimeSpan.FromHours(-8))
        };

        Assert.Equal(expected, result);
    }

    #endregion

    #region EachDayTo Tests

    [Fact]
    public void EachDayTo_LeapYearFebruary_PreservesOffset()
    {
        // Arrange
        var start = new DateTimeOffset(2024, 2, 28, 10, 0, 0, _offset);
        var end = new DateTimeOffset(2024, 3, 2, 8, 0, 0, _offset);

        // Act
        var result = start.EachDayTo(end).ToList();

        // Assert
        Assert.Equal(4, result.Count);
        Assert.Equal(new DateTimeOffset(2024, 2, 28, 0, 0, 0, _offset), result[0]);
        Assert.Equal(new DateTimeOffset(2024, 2, 29, 0, 0, 0, _offset), result[1]);
        Assert.Equal(new DateTimeOffset(2024, 3, 1, 0, 0, 0, _offset), result[2]);
        Assert.Equal(new DateTimeOffset(2024, 3, 2, 0, 0, 0, _offset), result[3]);
    }

    #endregion

    #region EachMonthTo Tests

    [Fact]
    public void EachMonthTo_CrossYearRange_WithDifferentOffsets()
    {
        // Arrange
        var start = new DateTimeOffset(2023, 12, 15, 0, 0, 0, TimeSpan.FromHours(-5));
        var end = new DateTimeOffset(2024, 2, 10, 0, 0, 0, TimeSpan.FromHours(2));

        // Act
        var result = start.EachMonthTo(end).ToList();

        // Assert
        var expected = new[]
        {
            new DateTimeOffset(2023, 12, 1, 0, 0, 0, TimeSpan.FromHours(-5)),
            new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.FromHours(-5)),
            new DateTimeOffset(2024, 2, 1, 0, 0, 0, TimeSpan.FromHours(2))
        };

        Assert.Equal(expected, result);
    }

    #endregion

    #region Edge Case Tests

    [Fact]
    public void AllMethods_ReverseRange_ReturnEmpty()
    {
        // Arrange
        var later = DateTimeOffset.Now;
        var earlier = later.AddMonths(-1);

        // Act & Assert
        Assert.Empty(later.EachSecondTo(earlier));
        Assert.Empty(later.EachMinuteTo(earlier));
        Assert.Empty(later.EachHourTo(earlier));
        Assert.Empty(later.EachDayTo(earlier));
        Assert.Empty(later.EachMonthTo(earlier));
    }

    [Fact]
    public void AllMethods_SameStartEnd_ReturnSingleItem()
    {
        // Arrange
        var timestamp = new DateTimeOffset(2023, 6, 15, 12, 0, 0, _offset);

        // Act & Assert
        Assert.Single(timestamp.EachSecondTo(timestamp));
        Assert.Single(timestamp.EachMinuteTo(timestamp));
        Assert.Single(timestamp.EachHourTo(timestamp));
        Assert.Single(timestamp.EachDayTo(timestamp));
        Assert.Single(timestamp.EachMonthTo(timestamp));
    }

    #endregion

    #region Truncation Tests

    [Fact]
    public void TruncateMethods_VerifyPrecision()
    {
        // Arrange
        var original = new DateTimeOffset(2023, 7, 4, 12, 34, 56, 789, _offset);

        // Act & Assert
        Assert.Equal(
            new DateTimeOffset(2023, 7, 4, 12, 34, 56, _offset),
            original.TruncateToSecond()
        );
        Assert.Equal(
            new DateTimeOffset(2023, 7, 4, 12, 34, 0, _offset),
            original.TruncateToMinute()
        );
        Assert.Equal(new DateTimeOffset(2023, 7, 4, 12, 0, 0, _offset), original.TruncateToHour());
        Assert.Equal(new DateTimeOffset(2023, 7, 4, 0, 0, 0, _offset), original.TruncateToDay());
    }

    #endregion
}
