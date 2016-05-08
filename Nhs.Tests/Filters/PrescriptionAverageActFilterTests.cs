using Nhs.Filters;
using NUnit.Framework;

namespace Nhs.Tests.Filters
{
    [TestFixture]
    public class PrescriptionAverageActFilterTests
    {
        [Test]
        public void CountPrescription()
        {
            var prescription = new Prescription
            {
                BNFName = "Peppermint Oil",
                ActCost = 1.12m
            };
            var pcf = new PrescriptionAverageActFilter("Peppermint Oil");
            pcf.Execute(prescription);

            Assert.AreEqual(1.12m, pcf.Cost);
        }

        [Test]
        public void CountPrescriptionAverage()
        {
            var prescription = new Prescription
            {
                BNFName = "Peppermint Oil",
                ActCost = 4.4m
            };
            var prescription2 = new Prescription
            {
                BNFName = "Peppermint Oil",
                ActCost = 2.2m
            };
            var pcf = new PrescriptionAverageActFilter("Peppermint Oil");
            pcf.Execute(prescription);
            pcf.Execute(prescription2);

            Assert.AreEqual(3.3m, pcf.Cost);
        }
    }
}
