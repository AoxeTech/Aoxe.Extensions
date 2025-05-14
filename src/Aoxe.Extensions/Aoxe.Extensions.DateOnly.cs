#if NETCOREAPP
namespace Aoxe.Extensions;

/// <summary>
/// Provides extension methods for <see cref="DateOnly"/> date range generation.
/// </summary>
public static partial class AoxeExtension
{
    public static IEnumerable<DateOnly> EachDayTo(this DateOnly dateFrom, DateOnly dateTo)
    {
        while (dateFrom <= dateTo)
        {
            yield return dateFrom;
            if (dateFrom.IsMax())
                yield break;
            dateFrom = dateFrom.AddDays(1);
        }
    }

    public static IEnumerable<DateOnly> EachMonthTo(this DateOnly dateFrom, DateOnly dateTo)
    {
        var current = dateFrom.TruncateToMonth();
        var endMonth = dateTo.TruncateToMonth();

        while (current <= endMonth)
        {
            yield return current;
            if (dateFrom.IsMax())
                yield break;
            current = current.AddMonths(1);
        }
    }

    public static IEnumerable<DateOnly> EachYearTo(this DateOnly dateFrom, DateOnly dateTo)
    {
        var current = dateFrom.TruncateToYear();
        var endMonth = dateTo.TruncateToYear();

        while (current <= endMonth)
        {
            yield return current;
            if (dateFrom.IsMax())
                yield break;
            current = current.AddYears(1);
        }
    }

    public static DateOnly TruncateToMonth(this DateOnly dto) => new(dto.Year, dto.Month, 1);

    public static DateOnly TruncateToYear(this DateOnly dto) => new(dto.Year, 1, 1);

    public static bool IsMax(this DateOnly dto) => dto == DateOnly.MaxValue;

    public static bool IsMin(this DateOnly dto) => dto == DateOnly.MinValue;
}
#endif
