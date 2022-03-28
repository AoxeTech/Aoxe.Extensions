#if !NETSTANDARD2_0 && !NETCOREAPP3_1
namespace Zaabee.Extensions;

public static partial class ZaabeeExtension
{
    public static TimeOnly AddSeconds(this TimeOnly time, double value) =>
        time.Add(TimeSpan.FromSeconds(value));

    public static IEnumerable<TimeOnly> EachSecondTo(this TimeOnly from, TimeOnly to)
    {
        var seconds = (to - from).Seconds;
        for (var i = 0; i <= seconds; i++)
        {
            var result = from.AddSeconds(i);
            yield return new TimeOnly(result.Hour, result.Minute, result.Second);
        }
    }

    public static IEnumerable<TimeOnly> EachMinuteTo(this TimeOnly from, TimeOnly to)
    {
        var minutes = (to - from).Minutes;
        for (var i = 0; i <= minutes; i++)
        {
            var result = from.AddMinutes(i);
            yield return new TimeOnly(result.Hour, result.Minute, 0);
        }
    }

    public static IEnumerable<TimeOnly> EachHourTo(this TimeOnly from, TimeOnly to)
    {
        var hours = (to - from).Hours;
        for (var i = 0; i <= hours; i++)
        {
            var result = from.AddHours(i);
            yield return new TimeOnly(result.Hour, 0, 0);
        }
    }
}
#endif