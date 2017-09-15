using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AqlaEvents
{
    public class EventDescriptionParser
    {
        //                                        +     3     8   (               097          )                        123-45 -67
        readonly Regex _phoneRegex = new Regex(@"\+{0,1}3{0,1}8{0,1}[\s]*[-(]{0,1}(?<op>0\d{2})[\s]*[-)]{0,1}[\s]*(?<n>(\d\s*-{0,1}\s*){7})($|[\s\n\w])");

        readonly Regex _onlyNumbers = new Regex(@"\D");

        public IReadOnlyList<string> ParsePhones(string description)
        {
            return _phoneRegex.Matches(description).OfType<Match>().Where(x => x.Success)
                .Select(x => _onlyNumbers.Replace((x.Groups["op"].Value + x.Groups["n"].Value).Replace(" ", ""), "")).ToArray();
        }

        static Regex[] MakeCurrencyRegexes(string[] symbolBothSidesRegexes, string[] symbolSuffixRegexes)
        {
            string endings = @"($|[\s\n\.\,])";
            return symbolBothSidesRegexes.SelectMany(s => MakeCurrencySymbolRegexes(s, endings)).Concat(
                symbolSuffixRegexes.Select(name => new Regex(@"\D(?<m>\d+) *" + name + endings))).ToArray();
        }

        static Regex[] MakeCurrencySymbolRegexes(string symbolRegexp, string endings)
        {
            return new[]
            {
                new Regex(@"\D(?<m>\d+) *" + symbolRegexp + endings, RegexOptions.IgnoreCase),
                new Regex(@"\D" + symbolRegexp + @" *(?<m>\d+) *" + endings, RegexOptions.IgnoreCase),
            };
        }

        readonly Regex[] _dollarRegexes = MakeCurrencyRegexes(new[] { @"\$", "usd" }, new[] { "долларов", "долл", "dollars", "dollar" });
        readonly Regex[] _euroRegexes = MakeCurrencyRegexes(new[] { @"\€", "eur" }, new[] { "євро", "евро", "euro" });
        readonly Regex[] _uahRegexes = MakeCurrencyRegexes(new[] { "uah" }, new[] { "гривень", "гривен", "грн" });

        public IReadOnlyList<MoneyParseResult> ParseMoney(string description)
        {
            return GetMoneyResults(MakeMoneyMatches(description, _uahRegexes), "грн")
                .Concat(GetMoneyResults(MakeMoneyMatches(description, _dollarRegexes), "$"))
                .Concat(GetMoneyResults(MakeMoneyMatches(description, _euroRegexes), "€"))
                .ToArray();
        }

        static Match[] MakeMoneyMatches(string description, Regex[] regexes)
        {
            return regexes.SelectMany(x => x.Matches(description).OfType<Match>().ToArray()).ToArray();
        }

        static MoneyParseResult[] GetMoneyResults(Match[] matches, string currency)
        {
            return matches.Select(x => new MoneyParseResult(int.Parse(x.Groups["m"].Value, CultureInfo.InvariantCulture), currency)).ToArray();
        }
    }
}