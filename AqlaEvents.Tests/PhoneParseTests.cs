using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AqlaEvents.Tests
{
    [TestFixture]
    public class PhoneParseTests
    {
        public static readonly object[][] Source =
        {
            new object[] { "sf dgdfg 3tk340tkg 4t56 45646", new string[] { } },
            new object[] { "sf dgdfg 3tk340tkg 097 1234567 4t56 097 1534567 45646", new[] { "0971234567", "0971534567" } },
            new object[] { "sf dgdfg 3tk340tkg +38 097 123 45 67 4t56 +38 097 153 45 67 45646", new[] { "0971234567", "0971534567" } },
            new object[] { "8 (097) 12345-67 sf dgdfg 3tk340tkg 4t56 45646 8 (097) 15345-67", new[] { "0971234567", "0971534567" } },
        };

        [Test]
        [TestCaseSource(nameof(Source))]
        public void Execute(string text, string[] phones)
        {
            var descParser = new EventDescriptionParser();
            Assert.That(descParser.ParsePhones(text).ToArray(), Is.EqualTo(phones));
        }
    }
}