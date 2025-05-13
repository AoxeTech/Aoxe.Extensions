#if NETCOREAPP
namespace Aoxe.Extensions;

/// <summary>
/// Provides extension methods for <see cref="DateOnly"/> date range generation.
/// </summary>
public static partial class AoxeExtension
{
    /// <summary>
    /// Generates a sequence of consecutive days between two dates inclusive.
    /// </summary>
    /// <param name="dateFrom">The starting date (inclusive)</param>
    /// <param name="dateTo">The ending date (inclusive)</param>
    /// <returns>An <see cref="IEnumerable{DateOnly}"/> containing all dates from <paramref name="dateFrom"/> through <paramref name="dateTo"/>.</returns>
    /// <remarks>
    /// <para>This method handles both forward and reverse date ranges:</para>
    /// <list type="bullet">
    /// <item><description>If <paramref name="dateFrom"/> ≤ <paramref name="dateTo"/>, returns dates in chronological order</description></item>
    /// <item><description>If <paramref name="dateFrom"/> > <paramref name="dateTo"/>, returns an empty sequence</description></item>
    /// </list>
    /// <example>
    /// <code>
    /// var start = new DateOnly(2023, 1, 1);
    /// var end = new DateOnly(2023, 1, 3);
    /// var dates = start.EachDayTo(end); // Returns [2023-01-01, 2023-01-02, 2023-01-03]
    /// </code>
    /// </example>
    /// </remarks>
    public static IEnumerable<DateOnly> EachDayTo(this DateOnly dateFrom, DateOnly dateTo)
    {
        if (dateFrom > dateTo)
            yield break;

        for (var date = dateFrom; date <= dateTo; date = date.AddDays(1))
        {
            yield return date;
        }
    }

    /// <summary>
    /// Generates a sequence of month-start dates covering the period between two dates.
    /// </summary>
    /// <param name="dateFrom">The starting date (will be converted to first day of its month)</param>
    /// <param name="dateTo">The ending date (inclusive)</param>
    /// <returns>An <see cref="IEnumerable{DateOnly}"/> containing the first day of each month from <paramref name="dateFrom"/>'s month through <paramref name="dateTo"/>'s month.</returns>
    /// <remarks>
    /// <para>Key characteristics:</para>
    /// <list type="bullet">
    /// <item><description>Always starts with the first day of <paramref name="dateFrom"/>'s month</description></item>
    /// <item><description>Includes all subsequent month starts until <paramref name="dateTo"/>'s month</description></item>
    /// <item><description>Returns empty sequence if <paramref name="dateFrom"/> > <paramref name="dateTo"/></description></item>
    /// </list>
    /// <example>
    /// <code>
    /// var start = new DateOnly(2023, 2, 15);
    /// var end = new DateOnly(2023, 4, 10);
    /// var months = start.EachMonthTo(end); // Returns [2023-02-01, 2023-03-01, 2023-04-01]
    /// </code>
    /// </example>
    /// </remarks>
    public static IEnumerable<DateOnly> EachMonthTo(this DateOnly dateFrom, DateOnly dateTo)
    {
        var current = new DateOnly(dateFrom.Year, dateFrom.Month, 1);
        var endMonth = new DateOnly(dateTo.Year, dateTo.Month, 1);

        if (dateFrom > dateTo)
        {
            yield break;
        }
        while (current <= endMonth)
        {
            yield return current;
            current = current.AddMonths(1);
        }
    }
}
#endif
