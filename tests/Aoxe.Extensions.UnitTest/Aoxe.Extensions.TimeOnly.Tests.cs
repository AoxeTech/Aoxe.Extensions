#if NETCOREAPP
namespace Aoxe.Extensions.UnitTest;

public class AoxeExtensionsTimeOnlyTests
{
    #region AddSeconds Tests

    [Theory]
    [InlineData("12:30:45", 30, "12:31:15")]
    [InlineData("23:59:59", 2, "00:00:01")]
    [InlineData("10:00:00", 3600.5, "11:00:00.500")]
    [InlineData("15:30:00", -900, "15:15:00")]
    public void AddSeconds_ValidInput_ReturnsCorrectTime(
        string initialTime,
        double seconds,
        string expectedTime
    )
    {
        // Arrange
        var time = TimeOnly.Parse(initialTime);
        var expected = TimeOnly.Parse(expectedTime);

        // Act
        var result = time.AddSeconds(seconds);

        // Assert
        Assert.Equal(expected, result);
    }

    #endregion

    #region EachSecondTo Tests

    [Fact]
    public void EachSecondTo_ValidRange_GeneratesCorrectSequence()
    {
        // Arrange
        var from = new TimeOnly(10, 30, 15);
        var to = new TimeOnly(10, 30, 17);
        var expected = new[] { "10:30:15", "10:30:16", "10:30:17" };

        // Act
        var result = from.EachSecondTo(to).Select(t => t.ToString("HH:mm:ss")).ToArray();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void EachSecondTo_CrossMidnight_GeneratesCorrectSequence()
    {
        // Arrange
        var from = new TimeOnly(23, 59, 58);
        var to = new TimeOnly(0, 0, 2);
        var expected = new[] { "23:59:58", "23:59:59", "00:00:00", "00:00:01", "00:00:02" };

        // Act
        var result = from.EachSecondTo(to).Select(t => t.ToString("HH:mm:ss")).ToArray();

        // Assert
        Assert.Equal(expected, result);
    }

    #endregion

    #region EachMinuteTo Tests

    [Fact]
    public void EachMinuteTo_ValidRange_GeneratesZeroSecondTimes()
    {
        // Arrange
        var from = new TimeOnly(9, 15, 30);
        var to = new TimeOnly(9, 17, 0);
        var expected = new[] { "09:15:00", "09:16:00", "09:17:00" };

        // Act
        var result = from.EachMinuteTo(to).Select(t => t.ToString("HH:mm:ss")).ToArray();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void EachMinuteTo_SameMinute_ReturnsSingleItem()
    {
        // Arrange
        var time = new TimeOnly(14, 30, 45);

        // Act
        var result = time.EachMinuteTo(time).Single();

        // Assert
        Assert.Equal(new TimeOnly(14, 30, 0), result);
    }

    #endregion

    #region EachHourTo Tests

    [Fact]
    public void EachHourTo_NormalRange_GeneratesHourlyMarkers()
    {
        // Arrange
        var from = new TimeOnly(8, 15, 30);
        var to = new TimeOnly(11, 45, 0);
        var expected = new[] { "08:00:00", "09:00:00", "10:00:00", "11:00:00" };

        // Act
        var result = from.EachHourTo(to).Select(t => t.ToString("HH:mm:ss")).ToArray();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void EachHourTo_WrapAroundMidnight_GeneratesCorrectSequence()
    {
        // Arrange
        var from = new TimeOnly(22, 0, 0);
        var to = new TimeOnly(2, 0, 0);
        var expected = new[] { "22:00:00", "23:00:00", "00:00:00", "01:00:00", "02:00:00" };

        // Act
        var result = from.EachHourTo(to).Select(t => t.ToString("HH:mm:ss")).ToArray();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void EachHourTo_24HourRange_ReturnsAllHours()
    {
        // Arrange
        var from = new TimeOnly(0, 0, 0);
        var to = new TimeOnly(23, 59, 59);

        // Act
        var result = from.EachHourTo(to).ToList();

        // Assert
        Assert.Equal(24, result.Count);
        Assert.Equal(0, result.First().Second);
        Assert.Equal(0, result.First().Minute);
    }

    #endregion
}
#endif
