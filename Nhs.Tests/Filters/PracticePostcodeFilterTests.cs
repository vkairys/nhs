using System.Linq;
using Nhs.Filters;
using NUnit.Framework;

namespace Nhs.Tests.Filters
{
    [TestFixture]
    public class PracticePostcodeFilterTests
    {
        [Test]
        public void FilterPostcode()
        {
            var practice = new Practice
            {
                Id = "Y03265",
                PostCode = "NW10 7NS"
            };
            var ppf = new PracticePostcodesFilter();
            ppf.Execute(practice);

            var filteredPractice = ppf.Practices.First();
            Assert.AreEqual("Y03265", filteredPractice.Key);
            Assert.AreEqual("NW10 7NS", filteredPractice.Value);
        }
    }
}
