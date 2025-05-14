#if NETCOREAPP
namespace Aoxe.Extensions.UnitTest;

public class DateRangeExtensionsTests
{
    #region Truncation Tests

    [Fact]
    public void TruncateToMonth_ResetsDayToFirst()
    {
        // Arrange
        var date = new DateOnly(2023, 10, 15);

        // Act
        var result = date.TruncateToMonth();

        // Assert
        Assert.Equal(new DateOnly(2023, 10, 1), result);
    }

    [Fact]
    public void TruncateToYear_ResetsToJanuaryFirst()
    {
        // Arrange
        var date = new DateOnly(2023, 7, 4);

        // Act
        var result = date.TruncateToYear();

        // Assert
        Assert.Equal(new DateOnly(2023, 1, 1), result);
    }

    #endregion

    #region EachDayTo Tests

    [Fact]
    public void EachDayTo_GeneratesDailySequence()
    {
        // Arrange
        var from = new DateOnly(2023, 10, 1);
        var to = new DateOnly(2023, 10, 3);
        var expected = new[]
        {
            new DateOnly(2023, 10, 1),
            new DateOnly(2023, 10, 2),
            new DateOnly(2023, 10, 3)
        };

        // Act
        var result = from.EachDayTo(to).ToList();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void EachDayTo_WhenEndIsEarlier_ReturnsEmpty()
    {
        // Arrange
        var from = new DateOnly(2023, 10, 5);
        var to = new DateOnly(2023, 10, 1);

        // Act
        var result = from.EachDayTo(to).ToList();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void EachDayTo_LeapYear_HandlesFebruary()
    {
        // Arrange
        var from = new DateOnly(2020, 2, 28);
        var to = new DateOnly(2020, 3, 1);

        // Act
        var result = from.EachDayTo(to).ToList();

        // Assert
        Assert.Equal(3, result.Count);
        Assert.Contains(new DateOnly(2020, 2, 29), result);
    }

    #endregion

    #region EachMonthTo Tests

    [Fact]
    public void EachMonthTo_GeneratesFirstOfMonth()
    {
        // Arrange
        var from = new DateOnly(2023, 1, 15);
        var to = new DateOnly(2023, 3, 15);
        var expected = new[]
        {
            new DateOnly(2023, 1, 1),
            new DateOnly(2023, 2, 1),
            new DateOnly(2023, 3, 1)
        };

        // Act
        var result = from.EachMonthTo(to).ToList();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void EachMonthTo_CrossYearBoundary()
    {
        // Arrange
        var from = new DateOnly(2023, 12, 15);
        var to = new DateOnly(2024, 2, 15);
        var expected = new[]
        {
            new DateOnly(2023, 12, 1),
            new DateOnly(2024, 1, 1),
            new DateOnly(2024, 2, 1)
        };

        // Act
        var result = from.EachMonthTo(to).ToList();

        // Assert
        Assert.Equal(expected, result);
    }

    #endregion

    #region EachYearTo Tests

    [Fact]
    public void EachYearTo_GeneratesJanuaryFirst()
    {
        // Arrange
        var from = new DateOnly(2020, 5, 5);
        var to = new DateOnly(2023, 5, 5);
        var expected = new[]
        {
            new DateOnly(2020, 1, 1),
            new DateOnly(2021, 1, 1),
            new DateOnly(2022, 1, 1),
            new DateOnly(2023, 1, 1)
        };

        // Act
        var result = from.EachYearTo(to).ToList();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void EachYearTo_SingleYear_ReturnsOneItem()
    {
        // Arrange
        var date = new DateOnly(2023, 7, 4);

        // Act
        var result = date.EachYearTo(date).Single();

        // Assert
        Assert.Equal(new DateOnly(2023, 1, 1), result);
    }

    #endregion

    #region Edge Cases

    [Fact]
    public void Methods_HandleMaxDate()
    {
        // Arrange
        var maxDate = DateOnly.MaxValue;

        // Act & Assert
        Assert.Equal(maxDate, maxDate.EachDayTo(maxDate).Single());
        Assert.Equal(maxDate.TruncateToMonth(), maxDate.EachMonthTo(maxDate).Single());
    }

    [Fact]
    public void Methods_HandleMinDate()
    {
        // Arrange
        var minDate = DateOnly.MinValue;

        // Act & Assert
        Assert.Equal(minDate, minDate.EachDayTo(minDate).Single());
        Assert.Equal(minDate.TruncateToMonth(), minDate.EachMonthTo(minDate).Single());
    }

    #endregion
}
#endif
