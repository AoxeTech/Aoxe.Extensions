namespace Aoxe.Extensions;

public static partial class AoxeExtension
{
    public static IEnumerable<DateTimeOffset> EachSecondTo(
        this DateTimeOffset from,
        DateTimeOffset to
    )
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

    public static IEnumerable<DateTimeOffset> EachMinuteTo(
        this DateTimeOffset from,
        DateTimeOffset to
    )
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

    public static IEnumerable<DateTimeOffset> EachHourTo(
        this DateTimeOffset from,
        DateTimeOffset to
    )
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

    public static IEnumerable<DateTimeOffset> EachDayTo(this DateTimeOffset from, DateTimeOffset to)
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

    public static IEnumerable<DateTimeOffset> EachMonthTo(
        this DateTimeOffset from,
        DateTimeOffset to
    )
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

    public static IEnumerable<DateTimeOffset> EachYearTo(
        this DateTimeOffset from,
        DateTimeOffset to
    )
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

    public static DateTimeOffset TruncateToSecond(this DateTimeOffset dto) =>
        new(dto.Year, dto.Month, dto.Day, dto.Hour, dto.Minute, dto.Second, dto.Offset);

    public static DateTimeOffset TruncateToMinute(this DateTimeOffset dto) =>
        new(dto.Year, dto.Month, dto.Day, dto.Hour, dto.Minute, 0, dto.Offset);

    public static DateTimeOffset TruncateToHour(this DateTimeOffset dto) =>
        new(dto.Year, dto.Month, dto.Day, dto.Hour, 0, 0, dto.Offset);

    public static DateTimeOffset TruncateToDay(this DateTimeOffset dto) =>
        new(dto.Year, dto.Month, dto.Day, 0, 0, 0, dto.Offset);

    public static DateTimeOffset TruncateToMonth(this DateTimeOffset dto) =>
        new(dto.Year, dto.Month, 1, 0, 0, 0, dto.Offset);

    public static DateTimeOffset TruncateToYear(this DateTimeOffset dto) =>
        new(dto.Year, 1, 1, 0, 0, 0, dto.Offset);

    public static bool IsMax(this DateTimeOffset dto) => dto == DateTimeOffset.MaxValue;

    public static bool IsMin(this DateTimeOffset dto) => dto == DateTimeOffset.MinValue;
}
