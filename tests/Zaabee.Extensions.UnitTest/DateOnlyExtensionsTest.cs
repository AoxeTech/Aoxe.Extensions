#if !NET48
namespace Zaabee.Extensions.UnitTest;

public class DateOnlyExtensionsTest
{
    [Theory]
    [InlineData(-1024)]
    [InlineData(0)]
    [InlineData(1024)]
    public void EachDayToTest(int days)
    {
        var dateFrom = new DateOnly(1900, 1, 1);
        var dateTo = dateFrom.AddDays(days);
        var dates = dateFrom.EachDayTo(dateTo).ToList();
        if (days < 0)
            Assert.Empty(dates);
        else
            Assert.Equal(days + 1, dates.Count);
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
        var dateFrom = new DateOnly(1900, 1, 1);
        var dateTo = dateFrom.AddMonths(months);
        var dates = dateFrom.EachMonthTo(dateTo).ToList();
        if (months < 0)
            Assert.Empty(dates);
        else
            Assert.Equal(months + 1, dates.Count);
        foreach (var result in dates)
        {
            Assert.Equal(dateFrom, result);
            dateFrom = dateFrom.AddMonths(1);
        }
    }
}
#endif
