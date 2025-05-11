#if !NET48
namespace Aoxe.Extensions.UnitTest;

public class DateRangeExtensionsTests
{
    public static IEnumerable<object[]> EachDayToTestCases =>
        new List<object[]>
        {
            // Normal forward range
            new object[]
            {
                new DateOnly(2023, 1, 1),
                new DateOnly(2023, 1, 3),
                new[] { 1, 2, 3 },
                3
            },
            // Single day
            new object[] { new DateOnly(2023, 2, 28), new DateOnly(2023, 2, 28), new[] { 28 }, 1 },
            // Cross-month
            new object[]
            {
                new DateOnly(2023, 1, 31),
                new DateOnly(2023, 2, 2),
                new[] { 31, 1, 2 },
                3
            },
            // Leap year
            new object[]
            {
                new DateOnly(2024, 2, 28),
                new DateOnly(2024, 3, 1),
                new[] { 28, 29, 1 },
                3
            }
        };

    public static IEnumerable<object[]> EachMonthToTestCases =>
        new List<object[]>
        {
            // Normal month range
            new object[]
            {
                new DateOnly(2023, 2, 15),
                new DateOnly(2023, 4, 10),
                new[] { 2, 3, 4 },
                3
            },
            // Single month
            new object[] { new DateOnly(2023, 5, 1), new DateOnly(2023, 5, 31), new[] { 5 }, 1 },
            // Cross-year
            new object[]
            {
                new DateOnly(2023, 12, 25),
                new DateOnly(2024, 2, 10),
                new[] { 12, 1, 2 },
                3
            },
            // Non-first-day start
            new object[]
            {
                new DateOnly(2023, 3, 15),
                new DateOnly(2023, 5, 1),
                new[] { 3, 4, 5 },
                3
            }
        };

    #region EachDayTo Tests

    [Theory]
    [MemberData(nameof(EachDayToTestCases))]
    public void EachDayTo_ValidRanges_ReturnsCorrectDates(
        DateOnly start,
        DateOnly end,
        int[] expectedDays,
        int expectedCount
    )
    {
        // Act
        var result = start.EachDayTo(end).ToList();

        // Assert
        Assert.Equal(expectedCount, result.Count);
        Assert.Equal(start, result.First());
        Assert.Equal(end, result.Last());

        for (int i = 0; i < expectedDays.Length; i++)
        {
            Assert.Equal(expectedDays[i], result[i].Day);
        }
    }

    [Fact]
    public void EachDayTo_ReverseDates_ReturnsEmpty()
    {
        // Arrange
        var laterDate = new DateOnly(2023, 5, 1);
        var earlierDate = new DateOnly(2023, 4, 30);

        // Act
        var result = laterDate.EachDayTo(earlierDate);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void EachDayTo_LargeRange_HandlesCorrectly()
    {
        // Arrange
        var start = new DateOnly(2020, 1, 1);
        var end = new DateOnly(2020, 12, 31);
        const int expectedDays = 366; // 2020 is a leap year

        // Act
        var result = start.EachDayTo(end).ToList();

        // Assert
        Assert.Equal(expectedDays, result.Count);
        Assert.Equal(start, result.First());
        Assert.Equal(end, result.Last());
    }

    #endregion

    #region EachMonthTo Tests

    [Theory]
    [MemberData(nameof(EachMonthToTestCases))]
    public void EachMonthTo_ValidRanges_ReturnsCorrectMonths(
        DateOnly start,
        DateOnly end,
        int[] expectedMonths,
        int expectedCount
    )
    {
        // Act
        var result = start.EachMonthTo(end).ToList();

        // Assert
        Assert.Equal(expectedCount, result.Count);
        Assert.Equal(start.Year, result.First().Year);
        Assert.Equal(end.Year, result.Last().Year);

        for (int i = 0; i < expectedMonths.Length; i++)
        {
            Assert.Equal(expectedMonths[i], result[i].Month);
            Assert.Equal(1, result[i].Day);
        }
    }

    [Fact]
    public void EachMonthTo_ReverseDates_ReturnsEmpty()
    {
        // Arrange
        var laterDate = new DateOnly(2023, 5, 1);
        var earlierDate = new DateOnly(2023, 4, 30);

        // Act
        var result = laterDate.EachMonthTo(earlierDate);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void EachMonthTo_LeapYearFebruary_HandlesCorrectly()
    {
        // Arrange
        var start = new DateOnly(2024, 2, 29);
        var end = new DateOnly(2024, 3, 1);

        // Act
        var result = start.EachMonthTo(end).ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(new DateOnly(2024, 2, 1), result[0]);
        Assert.Equal(new DateOnly(2024, 3, 1), result[1]);
    }

    [Fact]
    public void EachMonthTo_MultiYearRange_HandlesCorrectly()
    {
        // Arrange
        var start = new DateOnly(2022, 11, 15);
        var end = new DateOnly(2023, 2, 28);

        // Act
        var result = start.EachMonthTo(end).ToList();

        // Assert
        Assert.Equal(4, result.Count);
        Assert.Equal(new DateOnly(2022, 11, 1), result[0]);
        Assert.Equal(new DateOnly(2022, 12, 1), result[1]);
        Assert.Equal(new DateOnly(2023, 1, 1), result[2]);
        Assert.Equal(new DateOnly(2023, 2, 1), result[3]);
    }

    #endregion
}
#endif
