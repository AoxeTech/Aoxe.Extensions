namespace Zaabee.Extensions.UnitTest;

public class DateTimeOffsetExtensionTest
{
    [Fact]
    public void EachSecondToTest()
    {
        var dateFrom = DateTimeOffset.Now;
        var compareTime = new DateTimeOffset(dateFrom.Year, dateFrom.Month, dateFrom.Day,
            dateFrom.Hour, dateFrom.Minute, dateFrom.Second, DateTimeOffset.Now.Offset);
        var dateTo = DateTimeOffset.Now.AddDays(1).AddMinutes(-1);
        var results = dateFrom.EachSecondTo(dateTo);
        foreach (var result in results)
        {
            Assert.Equal(compareTime, result);
            compareTime = compareTime.AddSeconds(1);
        }
    }

    [Fact]
    public void EachMinuteToTest()
    {
        var dateFrom = DateTimeOffset.Now;
        var compareTime = new DateTimeOffset(dateFrom.Year, dateFrom.Month, dateFrom.Day,
            dateFrom.Hour, dateFrom.Minute, 0, DateTimeOffset.Now.Offset);
        var dateTo = DateTimeOffset.Now.AddDays(1).AddMinutes(-1);
        var results = dateFrom.EachMinuteTo(dateTo);
        foreach (var result in results)
        {
            Assert.Equal(compareTime, result);
            compareTime = compareTime.AddMinutes(1);
        }
    }

    [Fact]
    public void EachHourToTest()
    {
        var dateFrom = DateTimeOffset.Now;
        var compareTime = new DateTimeOffset(dateFrom.Year, dateFrom.Month, dateFrom.Day,
            dateFrom.Hour, 0, 0, DateTimeOffset.Now.Offset);
        var dateTo = DateTimeOffset.Now.AddDays(1).AddMinutes(-1);
        var results = dateFrom.EachHourTo(dateTo);
        foreach (var result in results)
        {
            Assert.Equal(compareTime, result);
            compareTime = compareTime.AddHours(1);
        }
    }

    [Fact]
    public void EachDayToTest()
    {
        var dateFrom = DateTimeOffset.Now;
        var dateTo = DateTimeOffset.Now.AddDays(7).AddHours(-1);
        var results = dateFrom.EachDayTo(dateTo);
        foreach (var result in results)
        {
            Assert.Equal(dateFrom.Date, result);
            dateFrom = dateFrom.AddDays(1);
        }
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-7)]
    [InlineData(-1024)]
    public void EachDayDateToLessThanDateFromTest(int days)
    {
        var dateFrom = DateTimeOffset.Now;
        var dateTo = DateTimeOffset.Now.AddDays(days);
        var results = dateFrom.EachDayTo(dateTo);
        Assert.Empty(results);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(7)]
    [InlineData(1024)]
    public void EachMonthToTest(int months)
    {
        var dateFrom = DateTimeOffset.Now;
        var dateTo = DateTimeOffset.Now.AddMonths(months).AddDays(-1);
        var results = dateFrom.EachMonthTo(dateTo);
        foreach (var result in results)
        {
            Assert.Equal(dateFrom.Date, result);
            dateFrom = dateFrom.AddMonths(1);
        }
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-7)]
    [InlineData(-1024)]
    public void EachMonthDateToLessThanDateFromTest(int months)
    {
        var dateFrom = DateTimeOffset.Now;
        var dateTo = DateTimeOffset.Now.AddMonths(months);
        var results = dateFrom.EachMonthTo(dateTo);
        Assert.Empty(results);
    }
}