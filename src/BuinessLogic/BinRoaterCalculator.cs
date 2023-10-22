using System.Globalization;
namespace BuinessLogic
{
    public static class BinRoaterCalculator
    {
        public static Housemate GetTurn(this IEnumerable<Housemate> housemates, int offset)
        {
            var weeknum = ISOWeek.GetWeekOfYear(DateTime.Now);
            var index = (weeknum + offset) % housemates.Count();

            return housemates.ElementAt(index);
        }

        public static DateTime IdToTurnDate(this Housemate housemate, Housemate current)
        {
            DateTime today = DateTime.Today;
            int daysUntilThursday = ((int)DayOfWeek.Thursday - (int)today.DayOfWeek + 7) % 7;

            var offset = 0;
            if (daysUntilThursday >= 4)
            {
                offset = 7;
            }

            DateTime nextThursday = today.AddDays(daysUntilThursday - offset);

            var weekOffset = 7 * (housemate.ID - current.ID);
            if (weekOffset < 0)
            {
                weekOffset = 7*(6-current.ID)+weekOffset + 7 * current.ID;
            }

            var adjustedDayOfTheYear = nextThursday.DayOfYear + weekOffset;

            return GetDateFromDayOfYear(today.Year, adjustedDayOfTheYear);
        }


        static DateTime GetDateFromDayOfYear(int year, int dayOfYear)
        {
            if (IsLeapYear(year))
            {
                if (dayOfYear < 1 || dayOfYear > 366)
                {
                    return DateTime.MinValue; // Invalid day of the year for a leap year
                }
            }
            else
            {
                if (dayOfYear < 1 || dayOfYear > 365)
                {
                    return DateTime.MinValue; // Invalid day of the year for a non-leap year
                }
            }

            return new DateTime(year, 1, 1).AddDays(dayOfYear - 1);
        }

        static bool IsLeapYear(int year)
        {
            return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
        }

        public static string GetWhichBins(bool switchDay)
        {
            var weeknum = ISOWeek.GetWeekOfYear(DateTime.Now);
            if (switchDay) weeknum += 1;

            return (weeknum % 2 != 0) ? "all bins" : "just recylcing";

        }
    }
}
