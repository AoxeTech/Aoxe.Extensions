namespace Zaabee.Extensions;

public static partial class ZaabeeExtension
{
    public static IEnumerable<DateTime> EachSecondTo(this DateTime from, DateTime to)
    {
        for (var dateTime = new DateTime(from.Year, from.Month, from.Day, from.Hour, from.Minute, from.Second);
             dateTime <= to;
             dateTime = dateTime.AddSeconds(1))
            yield return dateTime;
    }

    public static IEnumerable<DateTime> EachMinuteTo(this DateTime from, DateTime to)
    {
        for (var dateTime = new DateTime(from.Year, from.Month, from.Day, from.Hour, from.Minute, 0);
             dateTime <= to;
             dateTime = dateTime.AddMinutes(1))
            yield return dateTime;
    }

    public static IEnumerable<DateTime> EachHourTo(this DateTime from, DateTime to)
    {
        for (var dateTime = new DateTime(from.Year, from.Month, from.Day, from.Hour, 0, 0);
             dateTime <= to;
             dateTime = dateTime.AddHours(1))
            yield return dateTime;
    }

    public static IEnumerable<DateTime> EachDayTo(this DateTime from, DateTime to)
    {
        for (var dateTime = new DateTime(from.Year, from.Month, from.Day);
             dateTime <= to;
             dateTime = dateTime.AddDays(1))
            yield return dateTime;
    }

    public static IEnumerable<DateTime> EachMonthTo(this DateTime from, DateTime to)
    {
        for (var dateTime = new DateTime(from.Year, from.Month, 1);
             dateTime <= to;
             dateTime = dateTime.AddMonths(1))
            yield return dateTime;
    }
}