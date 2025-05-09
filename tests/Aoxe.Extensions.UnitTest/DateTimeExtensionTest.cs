namespace Aoxe.Extensions.UnitTest;

public class DateTimeExtensionTest
{
    [Theory]
    [InlineData(-1024)]
    [InlineData(0)]
    [InlineData(1024)]
    public void EachSecondToTest(int seconds)
    {
        var dateFrom = new DateTime(1900, 1, 1);
        var dateTo = dateFrom.AddSeconds(seconds);
        var dates = dateFrom.EachSecondTo(dateTo);
        if (seconds < 0)
            Assert.Empty(dates);
        else
            Assert.Equal(seconds + 1, dates.Count());
        foreach (var result in dates)
        {
            Assert.Equal(dateFrom, result);
            dateFrom = dateFrom.AddSeconds(1);
        }
    }

    [Theory]
    [InlineData(-1024)]
    [InlineData(0)]
    [InlineData(1024)]
    public void EachMinuteToTest(int minutes)
    {
        var dateFrom = new DateTime(1900, 1, 1);
        var dateTo = dateFrom.AddMinutes(minutes);
        var dates = dateFrom.EachMinuteTo(dateTo);
        if (minutes < 0)
            Assert.Empty(dates);
        else
            Assert.Equal(minutes + 1, dates.Count());
        foreach (var result in dates)
        {
            Assert.Equal(dateFrom, result);
            dateFrom = dateFrom.AddMinutes(1);
        }
    }

    [Theory]
    [InlineData(-1024)]
    [InlineData(0)]
    [InlineData(1024)]
    public void EachHourToTest(int hours)
    {
        var dateFrom = new DateTime(1900, 1, 1);
        var dateTo = dateFrom.AddHours(hours);
        var dates = dateFrom.EachHourTo(dateTo);
        if (hours < 0)
            Assert.Empty(dates);
        else
            Assert.Equal(hours + 1, dates.Count());
        foreach (var result in dates)
        {
            Assert.Equal(dateFrom, result);
            dateFrom = dateFrom.AddHours(1);
        }
    }

    [Theory]
    [InlineData(-1024)]
    [InlineData(0)]
    [InlineData(1024)]
    public void EachDayToTest(int days)
    {
        var dateFrom = new DateTime(1900, 1, 1);
        var dateTo = dateFrom.AddDays(days);
        var dates = dateFrom.EachDayTo(dateTo);
        if (days < 0)
            Assert.Empty(dates);
        else
            Assert.Equal(days + 1, dates.Count());
        foreach (var result in dates)
        {
            Assert.Equal(dateFrom, result);
            dateFrom = dateFrom.AddDays(1);
        }
    }

    [Theory]
    [InlineData(-1024)]
    [InlineData(0)]
    [InlineData(1024)]
    public void EachMonthToTest(int months)
    {
        var dateFrom = new DateTime(1900, 1, 1);
        var dateTo = dateFrom.AddMonths(months);
        var dates = dateFrom.EachMonthTo(dateTo);
        if (months < 0)
            Assert.Empty(dates);
        else
            Assert.Equal(months + 1, dates.Count());
        foreach (var result in dates)
        {
            Assert.Equal(dateFrom, result);
            dateFrom = dateFrom.AddMonths(1);
        }
    }
}
