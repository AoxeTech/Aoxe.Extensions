#if NETCOREAPP
namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    /// <summary>
    /// Adds a specified number of seconds to the TimeOnly value
    /// </summary>
    /// <param name="time">Base time value</param>
    /// <param name="value">Number of seconds to add (can be fractional)</param>
    /// <returns>New TimeOnly value representing the result of the addition</returns>
    public static TimeOnly AddSeconds(this TimeOnly time, double value) =>
        time.Add(TimeSpan.FromSeconds(value));

    /// <summary>
    /// Generates a sequence of TimeOnly values at 1-second intervals between two times (inclusive)
    /// </summary>
    /// <param name="from">Starting time (inclusive)</param>
    /// <param name="to">Ending time (inclusive)</param>
    /// <returns>Sequence of TimeOnly values every second from start to end</returns>
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

    /// <summary>
    /// Generates a sequence of TimeOnly values at 1-minute intervals between two times (inclusive)
    /// </summary>
    /// <param name="from">Starting time (inclusive)</param>
    /// <param name="to">Ending time (inclusive)</param>
    /// <returns>Sequence of TimeOnly values every minute from start to end</returns>
    /// <remarks>
    /// - Seconds are always set to 00 in the returned values
    /// </remarks>
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

    /// <summary>
    /// Generates a sequence of TimeOnly values at 1-hour intervals between two times (inclusive)
    /// </summary>
    /// <param name="from">Starting time (inclusive)</param>
    /// <param name="to">Ending time (inclusive)</param>
    /// <returns>Sequence of TimeOnly values every hour from start to end</returns>
    /// <remarks>
    /// - Minutes and seconds are always set to 00 in the returned values
    /// </remarks>
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
