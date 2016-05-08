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
            var prescripctionsTypes = new Dictionary<string, byte>
            {
                {"Peppermint Oil", 0}
            };

            var prescripction = new Prescription
            {
                BNFName = "Peppermint Oil"
            };

            var dtf = new DrugTypeFilter(prescripctionsTypes);
            dtf.Execute(prescripction);

            Assert.AreEqual(1, dtf.DrugTypes.First().Count);
        }
    }
}
