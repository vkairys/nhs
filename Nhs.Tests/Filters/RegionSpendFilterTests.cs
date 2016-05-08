using System.Collections.Generic;
using System.Linq;
using Nhs.Filters;
using NUnit.Framework;

namespace Nhs.Tests.Filters
{
    [TestFixture]
    public class RegionSpendFilterTests
    {
        [Test]
        public void RegionSpend()
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

            var asf = new RegionSpendFilter(practices);
            asf.Execute(prescription);
            Assert.AreEqual(1.20, asf.Regions.First(r => r.Region == "North East").Spent);
        }

        [Test]
        public void MultiAreaSpend()
        {
            var practices = new Dictionary<string, string>
            {
                {"A81001","TS18 1HU"},
                {"A82035","TS18 8HW"}
            };
            var prescription = new Prescription
            {
                Practice = "A81001",
                ActCost = 6
            };
            var prescription2 = new Prescription
            {
                Practice = "A82035",
                ActCost = 2
            };

            var asf = new RegionSpendFilter(practices);
            asf.Execute(prescription);
            asf.Execute(prescription2);
            Assert.AreEqual(4, asf.Regions.First(r => r.Region == "North East").Spent);
        }
    }
}