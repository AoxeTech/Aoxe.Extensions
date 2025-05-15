namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionsDateTimeTests
{
    #region EachSecondTo Tests

    [Fact]
    public void EachSecondTo_ValidRange_ReturnsCorrectSequence()
    {
        // Arrange
        var start = new DateTime(2023, 1, 1, 12, 30, 45, 500, DateTimeKind.Utc);
        var end = start.AddSeconds(2);

        // Act
        var result = start.EachSecondTo(end).ToList();

        // Assert
        Assert.Equal(3, result.Count);
        Assert.Equal(start.TruncateToSecond(), result[0]);
        Assert.Equal(start.TruncateToSecond().AddSeconds(1), result[1]);
        Assert.Equal(end.TruncateToSecond(), result[2]);
        Assert.All(result, d => Assert.Equal(DateTimeKind.Utc, d.Kind));
    }

    [Fact]
    public void EachSecondTo_ReverseRange_ReturnsEmpty()
    {
        // Arrange
        var later = DateTime.Now;
        var earlier = later.AddSeconds(-5);

        // Act
        var result = later.EachSecondTo(earlier);

        // Assert
        Assert.Empty(result);
    }

    #endregion

    #region EachMinuteTo Tests

    [Theory]
    [InlineData("2023-03-01 14:25:30", "2023-03-01 14:28:15", 4)]
    [InlineData("2023-03-01 14:30:00", "2023-03-01 14:30:00", 1)]
    public void EachMinuteTo_VariousRanges_ReturnsCorrectMinutes(
        string startStr,
        string endStr,
        int expectedCount
    )
    {
        // Arrange
        var start = DateTime.Parse(startStr);
        var end = DateTime.Parse(endStr);

        // Act
        var result = start.EachMinuteTo(end).ToList();

        // Assert
        Assert.Equal(expectedCount, result.Count);
        Assert.Equal(start.TruncateToMinute(), result.First());
        Assert.Equal(end.TruncateToMinute(), result.Last());
        Assert.All(result, d => Assert.Equal(0, d.Second));
    }

    #endregion

    #region EachHourTo Tests

    [Fact]
    public void EachHourTo_CrossDayRange_HandlesCorrectly()
    {
        // Arrange
        var start = new DateTime(2023, 3, 1, 22, 30, 0, DateTimeKind.Local);
        var end = new DateTime(2023, 3, 2, 2, 0, 0, DateTimeKind.Local);

        // Act
        var result = start.EachHourTo(end).ToList();

        // Assert
        var expected = new[]
        {
            new DateTime(2023, 3, 1, 22, 0, 0, DateTimeKind.Local),
            new DateTime(2023, 3, 1, 23, 0, 0, DateTimeKind.Local),
            new DateTime(2023, 3, 2, 0, 0, 0, DateTimeKind.Local),
            new DateTime(2023, 3, 2, 1, 0, 0, DateTimeKind.Local),
            new DateTime(2023, 3, 2, 2, 0, 0, DateTimeKind.Local)
        };

        Assert.Equal(expected, result);
    }

    #endregion

    #region EachDayTo Tests

    [Fact]
    public void EachDayTo_LeapYearFebruary_HandlesCorrectly()
    {
        // Arrange
        var start = new DateTime(2024, 2, 28, 10, 0, 0);
        var end = new DateTime(2024, 3, 2, 8, 0, 0);

        // Act
        var result = start.EachDayTo(end).ToList();

        // Assert
        Assert.Equal(4, result.Count);
        Assert.Equal(new DateTime(2024, 2, 28, 0, 0, 0), result[0]);
        Assert.Equal(new DateTime(2024, 2, 29, 0, 0, 0), result[1]);
        Assert.Equal(new DateTime(2024, 3, 1, 0, 0, 0), result[2]);
        Assert.Equal(new DateTime(2024, 3, 2, 0, 0, 0), result[3]);
    }

    #endregion

    #region EachMonthTo Tests

    [Theory]
    [InlineData("2023-01-15", "2023-03-10", 3)]
    [InlineData("2023-12-25", "2024-02-28", 3)]
    public void EachMonthTo_VariousRanges_ReturnsFirstDays(
        string startStr,
        string endStr,
        int expectedCount
    )
    {
        // Arrange
        var start = DateTime.Parse(startStr);
        var end = DateTime.Parse(endStr);

        // Act
        var result = start.EachMonthTo(end).ToList();

        // Assert
        Assert.Equal(expectedCount, result.Count);
        Assert.Equal(1, result.First().Day);
        Assert.Equal(1, result.Last().Day);
        Assert.All(result, d => Assert.Equal(0, d.Hour));
    }

    [Fact]
    public void EachMonthTo_EndBeforeStart_ReturnsEmpty()
    {
        // Arrange
        var start = new DateTime(2023, 5, 1);
        var end = new DateTime(2023, 4, 30);

        // Act
        var result = start.EachMonthTo(end);

        // Assert
        Assert.Empty(result);
    }

    #endregion

    #region EachYearTo Tests

    [Theory]
    [InlineData("2023-01-15", "2023-03-10", 1)]
    [InlineData("2023-12-25", "2024-02-28", 2)]
    public void EachYearTo_VariousRanges_ReturnsFirstDays(
        string startStr,
        string endStr,
        int expectedCount
    )
    {
        // Arrange
        var start = DateTime.Parse(startStr);
        var end = DateTime.Parse(endStr);

        // Act
        var result = start.EachYearTo(end).ToList();

        // Assert
        Assert.Equal(expectedCount, result.Count);
        Assert.Equal(1, result.First().Day);
        Assert.Equal(1, result.Last().Day);
        Assert.All(result, d => Assert.Equal(0, d.Hour));
    }

    [Fact]
    public void EachYearTo_EndBeforeStart_ReturnsEmpty()
    {
        // Arrange
        var start = new DateTime(2023, 5, 1);
        var end = new DateTime(2022, 4, 30);

        // Act
        var result = start.EachYearTo(end);

        // Assert
        Assert.Empty(result);
    }

    #endregion

    [Fact]
    public void IsMin_ReturnsTrueForMinDate()
    {
        // Arrange
        var minDate = DateTime.MinValue;

        // Act
        var result = minDate.IsMin();

        // Assert
        Assert.True(result);
    }
}
