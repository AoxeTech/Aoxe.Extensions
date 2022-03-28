#if !NET48 && !NETCOREAPP3_1
namespace Zaabee.Extensions.UnitTest;

public class TimeOnlyExtensionsTest
{
    [Theory]
    [InlineData(-1024)]
    [InlineData(0)]
    [InlineData(1024)]
    public void EachSecondToTest(int seconds)
    {
        var timeFrom = new TimeOnly(12, 0, 0);
        var timeTo = timeFrom.AddSeconds(seconds);
        var times = timeFrom.EachSecondTo(timeTo).ToList();
        foreach (var result in times)
        {
            Assert.Equal(timeFrom, result);
            timeFrom = timeFrom.AddSeconds(1);
        }
    }

    [Theory]
    [InlineData(-1024)]
    [InlineData(0)]
    [InlineData(1024)]
    public void EachMinuteToTest(int minutes)
    {
        var timeFrom = new TimeOnly(12, 0, 0);
        var timeTo = timeFrom.AddMinutes(minutes);
        var times = timeFrom.EachMinuteTo(timeTo).ToList();
        foreach (var result in times)
        {
            Assert.Equal(timeFrom, result);
            timeFrom = timeFrom.AddMinutes(1);
        }
    }

    [Theory]
    [InlineData(-11)]
    [InlineData(0)]
    [InlineData(11)]
    public void EachHourToTest(int hours)
    {
        var timeFrom = new TimeOnly(12, 0, 0);
        var timeTo = timeFrom.AddHours(hours);
        var times = timeFrom.EachHourTo(timeTo).ToList();
        foreach (var result in times)
        {
            Assert.Equal(timeFrom, result);
            timeFrom = timeFrom.AddHours(1);
        }
    }
}
#endif