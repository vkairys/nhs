using System.Collections.Generic;
using System.Linq;
using Nhs.Filters;
using NUnit.Framework;

namespace Nhs.Tests.Filters
{
    [TestFixture]
    public class DrugTypeFilterTests
    {
        [Test]
        public void FilterDrugType()
        {
            var prescriptionsTypes = new Dictionary<string, byte>
            {
                {"Peppermint Oil", 0}
            };

            var prescription = new Prescription
            {
                BNFName = "Peppermint Oil"
            };

            var dtf = new DrugTypeFilter(prescriptionsTypes);
            dtf.Execute(prescription);

            Assert.AreEqual(1, dtf.DrugTypes.First().Count);
        }
    }
}
