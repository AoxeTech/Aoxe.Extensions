namespace Aoxe.Extensions;

/// <summary>
/// Provides extension methods for generating date/time ranges at various intervals.
/// </summary>
public static partial class AoxeExtension
{
    public static IEnumerable<DateTime> EachSecondTo(this DateTime from, DateTime to)
    {
        var current = from.TruncateToSecond();
        var end = to.TruncateToSecond();

        while (current <= end)
        {
            yield return current;
            if (current.IsMax())
                yield break;
            current = current.AddSeconds(1);
        }
    }

    public static IEnumerable<DateTime> EachMinuteTo(this DateTime from, DateTime to)
    {
        var current = from.TruncateToMinute();
        var end = to.TruncateToMinute();

        while (current <= end)
        {
            yield return current;
            if (current.IsMax())
                yield break;
            current = current.AddMinutes(1);
        }
    }

    public static IEnumerable<DateTime> EachHourTo(this DateTime from, DateTime to)
    {
        var current = from.TruncateToHour();
        var end = to.TruncateToHour();

        while (current <= end)
        {
            yield return current;
            if (current.IsMax())
                yield break;
            current = current.AddHours(1);
        }
    }

    public static IEnumerable<DateTime> EachDayTo(this DateTime from, DateTime to)
    {
        var current = from.TruncateToDay();
        var end = to.TruncateToDay();

        while (current <= end)
        {
            yield return current;
            if (current.IsMax())
                yield break;
            current = current.AddDays(1);
        }
    }

    public static IEnumerable<DateTime> EachMonthTo(this DateTime from, DateTime to)
    {
        var current = from.TruncateToMonth();
        var end = to.TruncateToMonth();

        while (current <= end)
        {
            yield return current;
            if (current.IsMax())
                yield break;
            current = current.AddMonths(1);
        }
    }

    public static IEnumerable<DateTime> EachYearTo(this DateTime from, DateTime to)
    {
        var current = from.TruncateToYear();
        var end = to.TruncateToYear();

        while (current <= end)
        {
            yield return current;
            if (current.IsMax())
                yield break;
            current = current.AddYears(1);
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

    public static DateTime TruncateToYear(this DateTime dto) =>
        new(dto.Year, 1, 1, 0, 0, 0, dto.Kind);

    public static bool IsMax(this DateTime dto) => dto == DateTime.MaxValue;

    public static bool IsMin(this DateTime dto) => dto == DateTime.MinValue;
}
