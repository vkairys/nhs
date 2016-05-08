using System.Collections.Generic;
using System.Linq;

namespace Nhs.Filters
{
    public class DrugTypeFilter : IFilter<Prescription>
    {
        private static readonly string[] DrugTypeNames =
        {
            "Gastro-Intestinal System",
            "Cardiovascular System",
            "Respiratory system",
            "Central nervous system",
            "Infections",
            "Endocrine system",
            "Obstertrics, Gynaecology & Urology",
            "Malignant Disease & Immunosuppression",
            "Nutrition & Blood",
            "Musculoskeletal & Joint Diseases",
            "Ophthalmology",
            "Ear, Nose & Oropharynx",
            "Dermatology",
            "Immunology & Vaccines",
            "Anaesthetics"
        };
        private readonly Dictionary<string, byte> _drugs;
        private readonly double[] _drugTypes = new double[DrugTypeNames.Length];

        public DrugTypeFilter(Dictionary<string, byte> drugs)
        {
            _drugs = drugs;
        }

        public IEnumerable<DrugType> DrugTypes
        {
            get
            {
                var total = _drugTypes.Sum();
                return _drugTypes.Select((d, i) => new DrugType
                {
                    Name = DrugTypeNames[i],
                    Count = d / total
                });
            }
        }

        public void Execute(Prescription prescription)
        {
            if (_drugs.ContainsKey(prescription.BNFName))
            {
                var drugType = _drugs[prescription.BNFName];
                if (drugType < _drugTypes.Length)
                {
                    _drugTypes[drugType]++;
                }
            }
        }
    }
}
