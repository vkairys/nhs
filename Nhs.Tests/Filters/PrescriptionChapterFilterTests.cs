using System.Linq;
using Nhs.Filters;
using NUnit.Framework;

namespace Nhs.Tests.Filters
{
    [TestFixture]
    public class PrescriptionChapterFilterTests
    {
        [Test]
        public void FilterChapter()
        {
            var prescriptionCost = new PrescriptionCost
            {
                BnfChapter = 1,
                BnfChemicalName = "Peppermint Oil"
            };
            var pcf = new PrescriptionChapterFilter();
            pcf.Execute(prescriptionCost);

            var prescription = pcf.Prescriptions.First();
            Assert.AreEqual("Peppermint Oil", prescription.Key);
            Assert.AreEqual(1, prescription.Value);
        }
    }
}