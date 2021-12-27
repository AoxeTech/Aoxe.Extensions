namespace Zaabee.Extensions;

public static partial class ZaabeeExtension
{
    public static IEnumerable<DateTimeOffset> EachSecondTo(this DateTimeOffset from, DateTimeOffset to)
    {
        for (var dateTime =
                 new DateTimeOffset(from.Year, from.Month, from.Day, from.Hour, from.Minute, from.Second, from.Offset);
             dateTime <= to;
             dateTime = dateTime.AddSeconds(1))
            yield return dateTime;
    }

    public static IEnumerable<DateTimeOffset> EachMinuteTo(this DateTimeOffset from, DateTimeOffset to)
    {
        for (var dateTime =
                 new DateTimeOffset(from.Year, from.Month, from.Day, from.Hour, from.Minute, 0, from.Offset);
             dateTime <= to;
             dateTime = dateTime.AddMinutes(1))
            yield return dateTime;
    }

    public static IEnumerable<DateTimeOffset> EachHourTo(this DateTimeOffset from, DateTimeOffset to)
    {
        for (var dateTime =
                 new DateTimeOffset(from.Year, from.Month, from.Day, from.Hour, 0, 0, from.Offset);
             dateTime <= to;
             dateTime = dateTime.AddHours(1))
            yield return dateTime;
    }

    public static IEnumerable<DateTimeOffset> EachDayTo(this DateTimeOffset from, DateTimeOffset to)
    {
        for (var dateTime =
                 new DateTimeOffset(from.Year, from.Month, from.Day, 0, 0, 0, from.Offset);
             dateTime <= to;
             dateTime = dateTime.AddDays(1))
            yield return dateTime;
    }

    public static IEnumerable<DateTimeOffset> EachMonthTo(this DateTimeOffset from, DateTimeOffset to)
    {
        for (var dateTime =
                 new DateTimeOffset(from.Year, from.Month, 1, 0, 0, 0, from.Offset);
             dateTime <= to;
             dateTime = dateTime.AddMonths(1))
            yield return dateTime;
    }
}