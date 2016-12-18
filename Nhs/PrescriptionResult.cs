using System.Collections.Generic;

namespace Nhs
{
    public class PrescriptionResult
    {
        public IEnumerable<DrugType> DrugTypes { get; set; }
        public decimal Cost { get; set; }
        public List<PostCodeSpent> PostCodes { get; set; }
        public IEnumerable<RegionPrescripctions> Regions { get; set; }

        public PrescriptionResult()
        {
            DrugTypes = new DrugType[0];
            PostCodes = new List<PostCodeSpent>();
            Regions = new List<RegionPrescripctions>();
        }
    }
}