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

        public static string GetWhichBins(bool switchDay)
        {
            var weeknum = ISOWeek.GetWeekOfYear(DateTime.Now);
            if (switchDay) weeknum += 1;

            return (weeknum % 2 != 0) ? "all bins" : "just recylcing";

        }
    }
}
