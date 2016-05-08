using System.Collections.Generic;
using System.Linq;
using Nhs.Filters;
using NUnit.Framework;

namespace Nhs.Tests.Filters
{
    [TestFixture]
    public class PostcodeSpendFilterTests
    {
        [Test]
        public void CountPostcodeSpend()
        {
            var practices = new Dictionary<string, string>
            {
                {"A81001","TS18 1HU"}
            };
            var prescription = new Prescription
            {
                Practice = "A81001",
                ActCost = 1.20m
            };
            var psf = new PostcodeSpendFilter(practices);
            psf.Execute(prescription);

            var postcode = psf.PostCodes.First();
            Assert.AreEqual("TS18 1HU", postcode.Name);
            Assert.AreEqual(1.20m, postcode.Spent);
        }

        [Test]
        public void MultiPrescriptionsSpend()
        {
            var practices = new Dictionary<string, string>
            {
                {"A81001","TS18 1HU"}
            };
            var prescription = new Prescription
            {
                Practice = "A81001",
                ActCost = 1.20m
            };
            var prescription2 = new Prescription
            {
                Practice = "A81001",
                ActCost = 1.30m
            };
            var psf = new PostcodeSpendFilter(practices);
            psf.Execute(prescription);
            psf.Execute(prescription2);

            var postcode = psf.PostCodes.First();
            Assert.AreEqual("TS18 1HU", postcode.Name);
            Assert.AreEqual(2.50m, postcode.Spent);
        }
    }
}
