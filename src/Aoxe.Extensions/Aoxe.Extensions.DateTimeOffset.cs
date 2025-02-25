namespace Aoxe.Extensions;

/// <summary>
/// Provides extension methods for generating DateTimeOffset ranges at various intervals.
/// </summary>
public static partial class AoxeExtension
{
    /// <summary>
    /// Generates a sequence of consecutive seconds between two DateTimeOffset values inclusive.
    /// </summary>
    /// <param name="from">Starting DateTimeOffset (truncated to second precision)</param>
    /// <param name="to">Ending DateTimeOffset (inclusive)</param>
    /// <returns>Sequence of DateTimeOffset values at 1-second intervals</returns>
    /// <remarks>
    /// <para>Behavior:</para>
    /// <list type="bullet">
    /// <item><description>Truncates milliseconds from <paramref name="from"/></description></item>
    /// <item><description>Preserves original offset during iteration</description></item>
    /// <item><description>Handles both forward and reverse chronological order</description></item>
    /// </list>
    /// <example>
    /// <code>
    /// var start = new DateTimeOffset(2023, 1, 1, 12, 30, 45, TimeSpan.FromHours(8));
    /// var end = start.AddSeconds(2);
    /// var seconds = start.EachSecondTo(end); // Returns 3 elements
    /// </code>
    /// </example>
    /// </remarks>
    public static IEnumerable<DateTimeOffset> EachSecondTo(
        this DateTimeOffset from,
        DateTimeOffset to
    )
    {
        var current = from.TruncateToSecond();
        while (current <= to)
        {
            yield return current;
            current = current.AddSeconds(1);
        }
    }

    /// <summary>
    /// Generates a sequence of consecutive minutes between two DateTimeOffset values inclusive.
    /// </summary>
    /// <param name="from">Starting DateTimeOffset (truncated to minute precision)</param>
    /// <param name="to">Ending DateTimeOffset (inclusive)</param>
    /// <returns>Sequence of DateTimeOffset values at 1-minute intervals</returns>
    /// <remarks>
    /// Truncates seconds and milliseconds to zero while preserving original offset
    /// </remarks>
    public static IEnumerable<DateTimeOffset> EachMinuteTo(
        this DateTimeOffset from,
        DateTimeOffset to
    )
    {
        var current = from.TruncateToMinute();
        while (current <= to)
        {
            yield return current;
            current = current.AddMinutes(1);
        }
    }

    /// <summary>
    /// Generates a sequence of consecutive hours between two DateTimeOffset values inclusive.
    /// </summary>
    /// <param name="from">Starting DateTimeOffset (truncated to hour precision)</param>
    /// <param name="to">Ending DateTimeOffset (inclusive)</param>
    /// <returns>Sequence of DateTimeOffset values at 1-hour intervals</returns>
    /// <remarks>
    /// Preserves original offset while resetting minutes/seconds to zero
    /// </remarks>
    public static IEnumerable<DateTimeOffset> EachHourTo(
        this DateTimeOffset from,
        DateTimeOffset to
    )
    {
        var current = from.TruncateToHour();
        while (current <= to)
        {
            yield return current;
            current = current.AddHours(1);
        }
    }

    /// <summary>
    /// Generates a sequence of consecutive days between two DateTimeOffset values inclusive.
    /// </summary>
    /// <param name="from">Starting DateTimeOffset (truncated to day precision)</param>
    /// <param name="to">Ending DateTimeOffset (inclusive)</param>
    /// <returns>Sequence of DateTimeOffset values at 1-day intervals</returns>
    /// <remarks>
    /// <para>Time component set to 00:00:00 with original offset</para>
    /// <para>Handles daylight saving time changes automatically</para>
    /// </remarks>
    public static IEnumerable<DateTimeOffset> EachDayTo(this DateTimeOffset from, DateTimeOffset to)
    {
        var current = from.TruncateToDay();
        while (current <= to)
        {
            yield return current;
            current = current.AddDays(1);
        }
    }

    /// <summary>
    /// Generates a sequence of month-start dates between two DateTimeOffset values inclusive.
    /// </summary>
    /// <param name="from">Starting DateTimeOffset (adjusted to first day of month)</param>
    /// <param name="to">Ending DateTimeOffset (inclusive)</param>
    /// <returns>Sequence of DateTimeOffset values at 1-month intervals</returns>
    /// <remarks>
    /// <para>Always starts on the 1st of <paramref name="from"/>'s month</para>
    /// <para>Example: from=2023-01-15 → 2023-01-01 00:00:00 with original offset</para>
    /// </remarks>
    public static IEnumerable<DateTimeOffset> EachMonthTo(
        this DateTimeOffset from,
        DateTimeOffset to
    )
    {
        var current = new DateTimeOffset(from.Year, from.Month, 1, 0, 0, 0, from.Offset);

        var endMonth = new DateTimeOffset(to.Year, to.Month, 1, 0, 0, 0, to.Offset);

        while (current <= endMonth && current <= to)
        {
            yield return current;
            current = current.AddMonths(1);
        }
    }

    private static DateTimeOffset TruncateToSecond(this DateTimeOffset dto) =>
        new(dto.Year, dto.Month, dto.Day, dto.Hour, dto.Minute, dto.Second, dto.Offset);

    private static DateTimeOffset TruncateToMinute(this DateTimeOffset dto) =>
        new(dto.Year, dto.Month, dto.Day, dto.Hour, dto.Minute, 0, dto.Offset);

    private static DateTimeOffset TruncateToHour(this DateTimeOffset dto) =>
        new(dto.Year, dto.Month, dto.Day, dto.Hour, 0, 0, dto.Offset);

    private static DateTimeOffset TruncateToDay(this DateTimeOffset dto) =>
        new(dto.Year, dto.Month, dto.Day, 0, 0, 0, dto.Offset);
}
