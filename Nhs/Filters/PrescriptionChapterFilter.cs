using System.Collections.Generic;

namespace Nhs.Filters
{
    public class PrescriptionChapterFilter : IFilter<PrescriptionCost>
    {
        public Dictionary<string, byte> Prescriptions { get; set; }

        public PrescriptionChapterFilter()
        {
            Prescriptions = new Dictionary<string, byte>();
        }

        public void Execute(PrescriptionCost prescription)
        {
            if (!Prescriptions.ContainsKey(prescription.BnfChemicalName))
            {
                Prescriptions.Add(prescription.BnfChemicalName, prescription.BnfChapter);
            }
        }
    }
}
