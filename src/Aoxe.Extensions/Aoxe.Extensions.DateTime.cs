namespace Aoxe.Extensions;

/// <summary>
/// Provides extension methods for generating date/time ranges at various intervals.
/// </summary>
public static partial class AoxeExtension
{
    /// <summary>
    /// Generates a sequence of consecutive seconds between two dates inclusive.
    /// </summary>
    /// <param name="from">Starting DateTime (truncated to second precision)</param>
    /// <param name="to">Ending DateTime (inclusive)</param>
    /// <returns>Sequence of DateTime values at 1-second intervals</returns>
    /// <remarks>
    /// <para>Behavior:</para>
    /// <list type="bullet">
    /// <item><description>Starts at <paramref name="from"/> with milliseconds cleared</description></item>
    /// <item><description>Includes <paramref name="to"/> if within sequence</description></item>
    /// <item><description>Handles both UTC and local DateTime kinds</description></item>
    /// <item><description>Returns empty sequence if <paramref name="from"/> > <paramref name="to"/></description></item>
    /// </list>
    /// <example>
    /// <code>
    /// var start = new DateTime(2023, 1, 1, 12, 30, 45);
    /// var end = start.AddSeconds(2);
    /// var seconds = start.EachSecondTo(end); // 3 elements
    /// </code>
    /// </example>
    /// </remarks>
    public static IEnumerable<DateTime> EachSecondTo(this DateTime from, DateTime to)
    {
        if (from > to)
            yield break;

        var current = from.TruncateToSecond();

        while (current <= to)
        {
            yield return current;
            current = current.AddSeconds(1);
        }
    }

    /// <summary>
    /// Generates a sequence of consecutive minutes between two dates inclusive.
    /// </summary>
    /// <param name="from">Starting DateTime (truncated to minute precision)</param>
    /// <param name="to">Ending DateTime (inclusive)</param>
    /// <returns>Sequence of DateTime values at 1-minute intervals</returns>
    /// <remarks>
    /// <para>Truncates seconds and milliseconds to zero</para>
    /// <para>Example output for 12:30:45 to 12:32:00:</para>
    /// <list type="bullet">
    /// <item><description>12:30:00</description></item>
    /// <item><description>12:31:00</description></item>
    /// <item><description>12:32:00</description></item>
    /// </list>
    /// </remarks>
    public static IEnumerable<DateTime> EachMinuteTo(this DateTime from, DateTime to)
    {
        if (from > to)
            yield break;

        var current = from.TruncateToMinute();

        while (current <= to)
        {
            yield return current;
            current = current.AddMinutes(1);
        }
    }

    /// <summary>
    /// Generates a sequence of consecutive hours between two dates inclusive.
    /// </summary>
    /// <param name="from">Starting DateTime (truncated to hour precision)</param>
    /// <param name="to">Ending DateTime (inclusive)</param>
    /// <returns>Sequence of DateTime values at 1-hour intervals</returns>
    /// <remarks>
    /// Preserves DateTimeKind and resets minutes/seconds to zero
    /// </remarks>
    public static IEnumerable<DateTime> EachHourTo(this DateTime from, DateTime to)
    {
        if (from > to)
            yield break;

        var current = from.TruncateToHour();

        while (current <= to)
        {
            yield return current;
            current = current.AddHours(1);
        }
    }

    /// <summary>
    /// Generates a sequence of consecutive days between two dates inclusive.
    /// </summary>
    /// <param name="from">Starting DateTime (truncated to day precision)</param>
    /// <param name="to">Ending DateTime (inclusive)</param>
    /// <returns>Sequence of DateTime values at 1-day intervals</returns>
    /// <remarks>
    /// <para>Time component set to 00:00:00 for all dates</para>
    /// <para>Handles daylight saving time transitions appropriately</para>
    /// </remarks>
    public static IEnumerable<DateTime> EachDayTo(this DateTime from, DateTime to)
    {
        if (from > to)
            yield break;

        var current = from.TruncateToDay();

        while (current <= to)
        {
            yield return current;
            current = current.AddDays(1);
        }
    }

    /// <summary>
    /// Generates a sequence of month-start dates covering the period between two dates.
    /// </summary>
    /// <param name="from">Starting DateTime (truncated to first day of month)</param>
    /// <param name="to">Ending DateTime (inclusive)</param>
    /// <returns>Sequence of DateTime values at 1-month intervals</returns>
    /// <remarks>
    /// <para>Always starts on the 1st of <paramref name="from"/>'s month</para>
    /// <para>Example: from=2023-01-15, to=2023-03-10 returns:</para>
    /// <list type="bullet">
    /// <item><description>2023-01-01</description></item>
    /// <item><description>2023-02-01</description></item>
    /// <item><description>2023-03-01</description></item>
    /// </list>
    /// </remarks>
    public static IEnumerable<DateTime> EachMonthTo(this DateTime from, DateTime to)
    {
        if (from > to)
            yield break;

        var current = from.TruncateToMonth();

        while (current <= to)
        {
            yield return current;
            current = current.AddMonths(1);
        }
    }

    public static DateTime TruncateToSecond(this DateTime dto) =>
        new(dto.Year, dto.Month, dto.Day, dto.Hour, dto.Minute, dto.Second, dto.Kind);

    public static DateTime TruncateToMinute(this DateTime dto) =>
        new(dto.Year, dto.Month, dto.Day, dto.Hour, dto.Minute, 0, dto.Kind);

    public static DateTime TruncateToHour(this DateTime dto) =>
        new(dto.Year, dto.Month, dto.Day, dto.Hour, 0, 0, dto.Kind);

    public static DateTime TruncateToDay(this DateTime dto) =>
        new(dto.Year, dto.Month, dto.Day, 0, 0, 0, dto.Kind);

    public static DateTime TruncateToMonth(this DateTime dto) =>
        new(dto.Year, dto.Month, 1, 0, 0, 0, dto.Kind);
}
