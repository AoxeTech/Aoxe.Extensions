#if NETCOREAPP
namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    public static TimeOnly AddSeconds(this TimeOnly time, double value) =>
        time.Add(TimeSpan.FromSeconds(value));

    public static IEnumerable<TimeOnly> EachSecondTo(this TimeOnly from, TimeOnly to)
    {
        var start = from.TruncateToSecond();
        var end = to.TruncateToSecond();

        yield return start;

        while (start != end)
        {
            start = start.AddSeconds(1);
            yield return start;
        }
    }

    public static IEnumerable<TimeOnly> EachMinuteTo(this TimeOnly from, TimeOnly to)
    {
        var start = from.TruncateToMinute();
        var end = to.TruncateToMinute();

        yield return start;

        while (start != end)
        {
            start = start.AddMinutes(1);
            yield return start;
        }
    }

    public static IEnumerable<TimeOnly> EachHourTo(this TimeOnly from, TimeOnly to)
    {
        var start = from.TruncateToHour();
        var end = to.TruncateToHour();

        yield return start;

        while (start != end)
        {
            start = start.AddHours(1);
            yield return start;
        }
    }

    public static TimeOnly TruncateToSecond(this TimeOnly dto) =>
        new(dto.Hour, dto.Minute, dto.Second);

    public static TimeOnly TruncateToMinute(this TimeOnly dto) => new(dto.Hour, dto.Minute, 0);

    public static TimeOnly TruncateToHour(this TimeOnly dto) => new(dto.Hour, 0, 0);
}
#endif
