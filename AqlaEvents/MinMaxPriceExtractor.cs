using System.Linq;

namespace AqlaEvents
{
    public class MinMaxPriceExtractor
    {
        public void GetMinMaxPrice(MoneyParseResult[] parseResults, out int? min, out int? max, out string currency)
        {
            var results = (parseResults.Any(x => x.Currency == "грн")
                              ? parseResults.Where(x => x.Currency == "грн")
                              : parseResults.Any(x => x.Currency == "$")
                                  ? parseResults.Where(x => x.Currency == "$")
                                  : parseResults.GroupBy(x => x.Currency).FirstOrDefault())?.ToArray();
            if (results == null || results.Length == 0)
            {
                min = null;
                max = null;
                currency = null;
                return;
            }
            min = results.Min(x => x.Amount);
            max = results.Max(x => x.Amount);
            currency = results.First().Currency;
        }
    }
}