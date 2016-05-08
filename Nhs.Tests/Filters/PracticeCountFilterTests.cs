using Nhs.Filters;
using NUnit.Framework;

namespace Nhs.Tests.Filters
{
    [TestFixture]
    public class PracticeCountFilterTests
    {
        [Test]
        public void CountPractice()
        {
            var prescription = new Practice
            {
                PostCode = "NW10 7NS"
            };
            var pcf = new PracticeCountFilter(new[] { "NW" });
            pcf.Execute(prescription);

            Assert.AreEqual(1, pcf.Total);
        }

        [Test]
        public void SkipUnmatched()
        {
            var practice = new Practice
            {
                PostCode = "NW10 7NS"
            };
            var practice2 = new Practice
            {
                PostCode = "NE11 0NB"
            };
            var pcf = new PracticeCountFilter(new[] { "NW" });
            pcf.Execute(practice);
            pcf.Execute(practice2);

            Assert.AreEqual(1, pcf.Total);
        }
    }
}
