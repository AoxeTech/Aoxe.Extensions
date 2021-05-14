using System;
using System.Collections.Generic;

namespace Zaabee.Extensions
{
    public static partial class ZaabeeExtension
    {
        public static IEnumerable<DateTimeOffset> EachSecondTo(this DateTimeOffset timeFrom, DateTimeOffset timeTo)
        {
            var dateTime = new DateTimeOffset(timeTo.Year, timeTo.Month, timeTo.Day,
                timeTo.Hour, timeTo.Minute, timeTo.Second, timeTo.Offset);
            for (var time = new DateTimeOffset(timeFrom.Year, timeFrom.Month, timeFrom.Day,
                    timeFrom.Hour, timeFrom.Minute, timeFrom.Second, timeFrom.Offset);
                time <= dateTime;
                time = time.AddSeconds(1))
                yield return time;
        }

        public static IEnumerable<DateTimeOffset> EachMinuteTo(this DateTimeOffset timeFrom, DateTimeOffset timeTo)
        {
            var dateTime = new DateTimeOffset(timeTo.Year, timeTo.Month, timeTo.Day,
                timeTo.Hour, timeTo.Minute, 0, timeTo.Offset);
            for (var time = new DateTimeOffset(timeFrom.Year, timeFrom.Month, timeFrom.Day,
                    timeFrom.Hour, timeFrom.Minute, 0, timeFrom.Offset);
                time <= dateTime;
                time = time.AddMinutes(1))
                yield return time;
        }

        public static IEnumerable<DateTimeOffset> EachHourTo(this DateTimeOffset timeFrom, DateTimeOffset timeTo)
        {
            var dateTime = new DateTimeOffset(timeTo.Year, timeTo.Month, timeTo.Day,
                timeTo.Hour, 0, 0, timeTo.Offset);
            for (var time = new DateTimeOffset(timeFrom.Year, timeFrom.Month, timeFrom.Day,
                    timeFrom.Hour, 0, 0, timeFrom.Offset);
                time <= dateTime;
                time = time.AddHours(1))
                yield return time;
        }

        public static IEnumerable<DateTimeOffset> EachDayTo(this DateTimeOffset dateFrom, DateTimeOffset dateTo)
        {
            for (var date = dateFrom.Date; date <= dateTo.Date; date = date.AddDays(1))
                yield return date;
        }

        public static IEnumerable<DateTimeOffset> EachMonthTo(this DateTimeOffset dateFrom, DateTimeOffset dateTo)
        {
            for (var date = dateFrom.Date;
                date <= dateTo.Date || date.Month == dateTo.Month;
                date = date.AddMonths(1))
                yield return date;
        }
    }
}