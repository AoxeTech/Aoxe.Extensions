#if !NETSTANDARD2_0 && !NETCOREAPP3_1
namespace Zaabee.Extensions;

public static partial class ZaabeeExtension
{
    public static IEnumerable<DateOnly> EachDayTo(this DateOnly dateFrom, DateOnly dateTo)
    {
        for (var date = dateFrom; date <= dateTo; date = date.AddDays(1))
            yield return date;
    }

    public static IEnumerable<DateOnly> EachMonthTo(this DateOnly dateFrom, DateOnly dateTo)
    {
        for (var date = new DateOnly(dateFrom.Year, dateFrom.Month, 1); date <= dateTo; date = date.AddMonths(1))
            yield return date;
    }
}
#endif