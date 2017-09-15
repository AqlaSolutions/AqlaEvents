using System.Linq;
using NUnit.Framework;

namespace AqlaEvents.Tests
{
    [TestFixture]
    public class MoneyParseTests
    {
        public static readonly object[][] Source =
        {
            new object[] { "sf dgdfg 3tk340tkg 4t56 45646", new MoneyParseResult[] { } },
            new object[] { "sf dgdfg 3tk340tkg 123 грн  4t56 097 1534567 456 грн", new[] { new MoneyParseResult(123, "грн"), new MoneyParseResult(456, "грн") } },
            new object[] { "sf dgdfg 3tk340tkg $123  4t56 097 1534567 456 USD", new[] { new MoneyParseResult(123, "$"), new MoneyParseResult(456, "$") } },
        };

        [Test]
        [TestCaseSource(nameof(Source))]
        public void Execute(string text, MoneyParseResult[] results)
        {
            var descParser = new EventDescriptionParser();
            Assert.That(descParser.ParseMoney(text), Is.EqualTo(results));
        }
    }
}