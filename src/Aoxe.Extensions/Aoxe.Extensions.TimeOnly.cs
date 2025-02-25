#if !NETSTANDARD2_0
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
    /// <remarks>Returns empty sequence if end time is earlier than start time</remarks>
    public static IEnumerable<TimeOnly> EachSecondTo(this TimeOnly from, TimeOnly to)
    {
        var difference = to - from;
        if (difference < TimeSpan.Zero)
            yield break;

        // Calculate total full seconds between times
        var totalSeconds = (int)difference.TotalSeconds;

        for (var i = 0; i <= totalSeconds; i++)
        {
            var result = from.AddSeconds(i);
            yield return new TimeOnly(result.Hour, result.Minute, result.Second);
        }
    }

    /// <summary>
    /// Generates a sequence of TimeOnly values at 1-minute intervals between two times (inclusive)
    /// </summary>
    /// <param name="from">Starting time (inclusive)</param>
    /// <param name="to">Ending time (inclusive)</param>
    /// <returns>Sequence of TimeOnly values every minute from start to end</returns>
    /// <remarks>
    /// - Returns empty sequence if end time is earlier than start time
    /// - Seconds are always set to 00 in the returned values
    /// </remarks>
    public static IEnumerable<TimeOnly> EachMinuteTo(this TimeOnly from, TimeOnly to)
    {
        var difference = to - from;
        if (difference < TimeSpan.Zero)
            yield break;

        // Calculate total full minutes between times
        var totalMinutes = (int)difference.TotalMinutes;

        for (var i = 0; i <= totalMinutes; i++)
        {
            var result = from.AddMinutes(i);
            yield return new TimeOnly(result.Hour, result.Minute, 0);
        }
    }

    /// <summary>
    /// Generates a sequence of TimeOnly values at 1-hour intervals between two times (inclusive)
    /// </summary>
    /// <param name="from">Starting time (inclusive)</param>
    /// <param name="to">Ending time (inclusive)</param>
    /// <returns>Sequence of TimeOnly values every hour from start to end</returns>
    /// <remarks>
    /// - Returns empty sequence if end time is earlier than start time
    /// - Minutes and seconds are always set to 00 in the returned values
    /// </remarks>
    public static IEnumerable<TimeOnly> EachHourTo(this TimeOnly from, TimeOnly to)
    {
        var difference = to - from;
        if (difference < TimeSpan.Zero)
            yield break;

        // Calculate total full hours between times
        var totalHours = (int)difference.TotalHours;

        for (var i = 0; i <= totalHours; i++)
        {
            var result = from.AddHours(i);
            yield return new TimeOnly(result.Hour, 0, 0);
        }
    }
}
#endif
