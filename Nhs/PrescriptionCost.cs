using CsvHelper.Configuration;

namespace Nhs
{
    public class PrescriptionCost
    {
        public string DrugName { get; set; }
        public string BnfChemicalName { get; set; }
        public byte BnfChapter { get; set; }
        public byte BnfSection { get; set; }
        public string BnfSectionName { get; set; }
        public byte BnfParagraph { get; set; }
        public byte BnfSubParagraph { get; set; }
        public string BnfSubParagraphName { get; set; }
        public byte StandardQuantityUnitId { get; set; }
        public byte PrepClassId { get; set; }
        public double Items { get; set; }
        public double Qty { get; set; }
        public double Owc2Items { get; set; }
        public decimal NIC { get; set; }
        public decimal NICPerItem { get; set; }
        public decimal NICPerQty { get; set; }
        public decimal QtyPerItem { get; set; }
    }

    public sealed class PrescriptionCostClassMap : CsvClassMap<PrescriptionCost>
    {
        public PrescriptionCostClassMap()
        {
            Map(m => m.DrugName).Index(0);
            Map(m => m.BnfChemicalName).Index(1);
            Map(m => m.BnfChapter).Index(2);
        }
    }
}
