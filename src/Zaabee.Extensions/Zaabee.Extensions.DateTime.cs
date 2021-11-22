namespace Zaabee.Extensions;

public static partial class ZaabeeExtension
{
    public static IEnumerable<DateTime> EachSecondTo(this DateTime timeFrom, DateTime timeTo)
    {
        var dateTime = new DateTime(timeTo.Year, timeTo.Month, timeTo.Day,
            timeTo.Hour, timeTo.Minute, timeTo.Second);
        for (var time = new DateTime(timeFrom.Year, timeFrom.Month, timeFrom.Day,
                 timeFrom.Hour, timeFrom.Minute, timeFrom.Second);
             time <= dateTime;
             time = time.AddSeconds(1))
            yield return time;
    }

    public static IEnumerable<DateTime> EachMinuteTo(this DateTime timeFrom, DateTime timeTo)
    {
        var dateTime = new DateTime(timeTo.Year, timeTo.Month, timeTo.Day,
            timeTo.Hour, timeTo.Minute, 0);
        for (var time = new DateTime(timeFrom.Year, timeFrom.Month, timeFrom.Day,
                 timeFrom.Hour, timeFrom.Minute, 0);
             time <= dateTime;
             time = time.AddMinutes(1))
            yield return time;
    }

    public static IEnumerable<DateTime> EachHourTo(this DateTime timeFrom, DateTime timeTo)
    {
        var dateTime = new DateTime(timeTo.Year, timeTo.Month, timeTo.Day,
            timeTo.Hour, 0, 0);
        for (var time = new DateTime(timeFrom.Year, timeFrom.Month, timeFrom.Day,
                 timeFrom.Hour, 0, 0);
             time <= dateTime;
             time = time.AddHours(1))
            yield return time;
    }

    public static IEnumerable<DateTime> EachDayTo(this DateTime dateFrom, DateTime dateTo)
    {
        for (var date = dateFrom.Date; date.Date <= dateTo.Date; date = date.AddDays(1))
            yield return date;
    }

    public static IEnumerable<DateTime> EachMonthTo(this DateTime dateFrom, DateTime dateTo)
    {
        for (var date = dateFrom.Date;
             date.Date <= dateTo.Date || date.Month == dateTo.Month;
             date = date.AddMonths(1))
            yield return date;
    }
}