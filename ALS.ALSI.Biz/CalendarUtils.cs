using System;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz
{
    public static class CalendarUtils
    {
        public static int GetWorkingDays(this DateTime current, DateTime finishDateExclusive, List<DateTime> excludedDates)
        {
            Func<int, bool> isWorkingDay = days =>
            {
                var currentDate = current.AddDays(days);
                var isNonWorkingDay =
                    currentDate.DayOfWeek == DayOfWeek.Saturday ||
                    currentDate.DayOfWeek == DayOfWeek.Sunday ||
                    excludedDates.Exists(excludedDate => excludedDate.Date.Equals(currentDate.Date));
                return !isNonWorkingDay;
            };

            return Enumerable.Range(0, (finishDateExclusive - current).Days).Count(isWorkingDay);
        }
    }
}
