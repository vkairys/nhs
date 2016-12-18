using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Nhs.Tests
{
    [TestFixture]
    public class CsvSerializerTests
    {
        private ICsvSerializer _csvSerializer;

        public CsvSerializerTests()
        {
            _csvSerializer = new CsvSerializer();

        }

        [Test]
        public void DeserializePrescriptions()
        {
            var records =
                @" SHA,PCT,PRACTICE,BNF CODE,BNF NAME                              ,ITEMS  ,NIC        ,ACT COST   ,PERIOD                                 
Q30,5D7,A86001,0703010F0,Combined Ethinylestradiol 30mcg         ,0000001,00000001.89,00000001.77,201109 ";
            var reader = new StringReader(records);

            var prescriptions = _csvSerializer.DeserializePrescriptions(reader);

            var prescription = prescriptions.First();
            Assert.That(prescription.SHA, Is.EqualTo("Q30"));
            Assert.That(prescription.PCT, Is.EqualTo("5D7"));
            Assert.That(prescription.Practice, Is.EqualTo("A86001"));
            Assert.That(prescription.BNFCode, Is.EqualTo("0703010F0"));
            Assert.That(prescription.BNFName, Is.EqualTo("Combined Ethinylestradiol 30mcg"));
            Assert.That(prescription.Items, Is.EqualTo(1));
            Assert.That(prescription.NIC, Is.EqualTo(1.89));
            Assert.That(prescription.ActCost, Is.EqualTo(1.77));
            Assert.That(prescription.Period, Is.EqualTo(201109));
        }

        [Test]
        public void DeserializePractices()
        {
            var records =
                @"201202,A81001,THE DENSHAM SURGERY                     ,THE HEALTH CENTRE        ,LAWSON STREET            ,STOCKTON                 ,CLEVELAND                ,TS18 1HU";
            var reader = new StringReader(records);

            var practices = _csvSerializer.DeserializePractices(reader);

            var practice = practices.First();
            Assert.That(practice.Period, Is.EqualTo("201202"));
            Assert.That(practice.Id, Is.EqualTo("A81001"));
            Assert.That(practice.Name, Is.EqualTo("THE DENSHAM SURGERY"));
            Assert.That(practice.Address1, Is.EqualTo("THE HEALTH CENTRE"));
            Assert.That(practice.Address2, Is.EqualTo("LAWSON STREET"));
            Assert.That(practice.Address3, Is.EqualTo("STOCKTON"));
            Assert.That(practice.Address4, Is.EqualTo("CLEVELAND"));
            Assert.That(practice.PostCode, Is.EqualTo("TS18 1HU"));

        }


        [Test]
        public void DeserializePrescriptionCosts()
        {
            var records =
                @"DRUG NAME,BNF CHEMICAL NAME,BNF CHAPTER,BNF SECTION,BNF SECTION NAME,BNF PARAGRAPH,BNF SUB PARAGRAPH,BNF SUB PARAGRAPH NAME,STANDARD QUANTITY UNIT ID,PREP CLASS ID,Items (thousands),Qty (thousands),Owc2 Items (thousands),NIC £ (thousands),NIC Per Item £,NIC Per Qty £,Qty Per Item
Altacite Plus_Susp 125mg/500mg/5ml S/F,Co-Simalcite (Simeticone/Hydrotalcite),1,1,Dyspep&Gastro-Oesophageal Reflux Disease,1,0,Antacids and Simeticone,3,3,1.891,""1,036.23"",0.719,7.09,3.75,0.01,547.98;";
            var reader = new StringReader(records);

            var prescriptionCosts = _csvSerializer.DeserializePrescriptionCosts(reader);

            var prescriptionCost = prescriptionCosts.First();
            Assert.That(prescriptionCost.DrugName, Is.EqualTo("Altacite Plus_Susp 125mg/500mg/5ml S/F"));
            Assert.That(prescriptionCost.BnfChemicalName, Is.EqualTo("Co-Simalcite (Simeticone/Hydrotalcite)"));
            Assert.That(prescriptionCost.BnfChapter, Is.EqualTo(1));
        }

    }
}