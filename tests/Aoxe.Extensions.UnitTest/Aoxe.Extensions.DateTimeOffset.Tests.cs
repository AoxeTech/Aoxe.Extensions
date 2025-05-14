namespace Aoxe.Extensions.UnitTest;

public class DateTimeOffsetRangeExtensionsTests
{
    private readonly DateTimeOffset _testDate = new DateTimeOffset(
        2023,
        10,
        15,
        14,
        30,
        45,
        500,
        TimeSpan.FromHours(2)
    );

    #region Truncation Tests

    [Fact]
    public void TruncateToSecond_RemovesMilliseconds()
    {
        // Act
        var result = _testDate.TruncateToSecond();

        // Assert
        Assert.Equal(new DateTimeOffset(2023, 10, 15, 14, 30, 45, TimeSpan.FromHours(2)), result);
        Assert.Equal(0, result.Millisecond);
    }

    [Fact]
    public void TruncateToMinute_ResetsSeconds()
    {
        // Act
        var result = _testDate.TruncateToMinute();

        // Assert
        Assert.Equal(new DateTimeOffset(2023, 10, 15, 14, 30, 0, TimeSpan.FromHours(2)), result);
        Assert.Equal(0, result.Second);
    }

    [Fact]
    public void TruncateToHour_ResetsMinutesAndSeconds()
    {
        // Act
        var result = _testDate.TruncateToHour();

        // Assert
        Assert.Equal(new DateTimeOffset(2023, 10, 15, 14, 0, 0, TimeSpan.FromHours(2)), result);
        Assert.Equal(0, result.Minute);
    }

    [Fact]
    public void TruncateToDay_ResetsTimeToMidnight()
    {
        // Act
        var result = _testDate.TruncateToDay();

        // Assert
        Assert.Equal(new DateTimeOffset(2023, 10, 15, 0, 0, 0, TimeSpan.FromHours(2)), result);
        Assert.Equal(0, result.Hour);
    }

    [Fact]
    public void TruncateToMonth_SetsFirstDayOfMonth()
    {
        // Act
        var result = _testDate.TruncateToMonth();

        // Assert
        Assert.Equal(new DateTimeOffset(2023, 10, 1, 0, 0, 0, TimeSpan.FromHours(2)), result);
        Assert.Equal(1, result.Day);
    }

    #endregion

    #region EachSecondTo Tests

    [Fact]
    public void EachSecondTo_GeneratesCorrectSequence()
    {
        // Arrange
        var from = new DateTimeOffset(2023, 10, 15, 14, 30, 45, TimeSpan.Zero);
        var to = from.AddSeconds(2);

        // Act
        var results = from.EachSecondTo(to).ToList();

        // Assert
        Assert.Equal(3, results.Count);
        Assert.Equal(from, results[0]);
        Assert.Equal(from.AddSeconds(1), results[1]);
        Assert.Equal(to, results[2]);
    }

    [Fact]
    public void EachSecondTo_WhenEndIsEarlier_ReturnsEmpty()
    {
        // Arrange
        var from = new DateTimeOffset(2023, 10, 15, 14, 30, 45, TimeSpan.Zero);
        var to = from.AddSeconds(-1);

        // Act
        var results = from.EachSecondTo(to).ToList();

        // Assert
        Assert.Empty(results);
    }

    #endregion

    #region EachMinuteTo Tests

    [Fact]
    public void EachMinuteTo_ResetsSecondsToZero()
    {
        // Arrange
        var from = new DateTimeOffset(2023, 10, 15, 14, 30, 15, TimeSpan.Zero);
        var to = from.AddMinutes(2);

        // Act
        var results = from.EachMinuteTo(to).ToList();

        // Assert
        Assert.Equal(3, results.Count);
        Assert.All(results, dto => Assert.Equal(0, dto.Second));
        Assert.Equal(from.TruncateToMinute(), results[0]);
        Assert.Equal(from.TruncateToMinute().AddMinutes(1), results[1]);
    }

    #endregion

    #region EachHourTo Tests

    [Fact]
    public void EachHourTo_ResetsMinutesAndSeconds()
    {
        // Arrange
        var from = new DateTimeOffset(2023, 10, 15, 14, 30, 15, TimeSpan.Zero);
        var to = from.AddHours(2);

        // Act
        var results = from.EachHourTo(to).ToList();

        // Assert
        Assert.Equal(3, results.Count);
        Assert.All(
            results,
            dto =>
            {
                Assert.Equal(0, dto.Minute);
                Assert.Equal(0, dto.Second);
            }
        );
    }

    #endregion

    #region EachDayTo Tests

    [Fact]
    public void EachDayTo_GeneratesMidnightTimes()
    {
        // Arrange
        var from = new DateTimeOffset(2023, 10, 15, 14, 30, 0, TimeSpan.Zero);
        var to = from.AddDays(2);

        // Act
        var results = from.EachDayTo(to).ToList();

        // Assert
        Assert.Equal(3, results.Count);
        Assert.All(
            results,
            dto =>
            {
                Assert.Equal(0, dto.Hour);
                Assert.Equal(0, dto.Minute);
                Assert.Equal(0, dto.Second);
            }
        );
    }

    #endregion

    #region EachMonthTo Tests

    [Fact]
    public void EachMonthTo_HandlesMonthBoundaries()
    {
        // Arrange
        var from = new DateTimeOffset(2023, 1, 31, 0, 0, 0, TimeSpan.Zero);
        var to = new DateTimeOffset(2023, 3, 1, 0, 0, 0, TimeSpan.Zero);

        // Act
        var results = from.EachMonthTo(to).ToList();

        // Assert
        Assert.Equal(3, results.Count);
        Assert.Equal(new DateTimeOffset(2023, 1, 1, 0, 0, 0, TimeSpan.Zero), results[0]);
        Assert.Equal(new DateTimeOffset(2023, 2, 1, 0, 0, 0, TimeSpan.Zero), results[1]);
        Assert.Equal(new DateTimeOffset(2023, 3, 1, 0, 0, 0, TimeSpan.Zero), results[2]);
    }

    [Fact]
    public void EachMonthTo_CrossYearBoundary()
    {
        // Arrange
        var from = new DateTimeOffset(2023, 12, 15, 0, 0, 0, TimeSpan.Zero);
        var to = new DateTimeOffset(2024, 1, 15, 0, 0, 0, TimeSpan.Zero);

        // Act
        var results = from.EachMonthTo(to).ToList();

        // Assert
        Assert.Equal(2, results.Count);
        Assert.Equal(2023, results[0].Year);
        Assert.Equal(12, results[0].Month);
        Assert.Equal(2024, results[1].Year);
        Assert.Equal(1, results[1].Month);
    }

    #endregion

    #region EachYearTo Tests

    [Fact]
    public void EachYearTo_HandlesYearBoundaries()
    {
        // Arrange
        var from = new DateTimeOffset(2023, 1, 31, 0, 0, 0, TimeSpan.Zero);
        var to = new DateTimeOffset(2023, 3, 1, 0, 0, 0, TimeSpan.Zero);

        // Act
        var results = from.EachYearTo(to).ToList();

        // Assert
        Assert.Single(results);
        Assert.Equal(new DateTimeOffset(2023, 1, 1, 0, 0, 0, TimeSpan.Zero), results[0]);
    }

    [Fact]
    public void EachYearTo_CrossYearBoundary()
    {
        // Arrange
        var from = new DateTimeOffset(2023, 12, 15, 0, 0, 0, TimeSpan.Zero);
        var to = new DateTimeOffset(2024, 1, 15, 0, 0, 0, TimeSpan.Zero);

        // Act
        var results = from.EachYearTo(to).ToList();

        // Assert
        Assert.Equal(2, results.Count);
        Assert.Equal(2023, results[0].Year);
        Assert.Equal(2024, results[1].Year);
    }

    #endregion

    #region Time Zone Tests

    [Fact]
    public void Methods_PreserveOffset()
    {
        // Arrange
        var offset = TimeSpan.FromHours(-5);
        var from = new DateTimeOffset(2023, 10, 15, 14, 30, 0, offset);

        // Act
        var secondTruncated = from.TruncateToSecond();
        var hourSequence = from.EachHourTo(from.AddHours(2)).First();
        var daySequence = from.EachDayTo(from.AddDays(1)).First();

        // Assert
        Assert.Equal(offset, secondTruncated.Offset);
        Assert.Equal(offset, hourSequence.Offset);
        Assert.Equal(offset, daySequence.Offset);
    }

    #endregion
}
