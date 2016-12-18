using System.Collections.Generic;
using System.IO;
using Nhs.Filters;

namespace Nhs
{
    public class NhsProcessor : INhsProcessor
    {
        private readonly DataReader _dataReader = new DataReader();

        public PracticeResult ProcessPractice(StreamReader streamReader)
        {
            var practiceCountFilter = new PracticeCountFilter(new[]
            {
                "E", "EC", "N", "NW", "SE", "SW", "W", "WC" //London postcodes
            });

            var practicePostcodesFilter = new PracticePostcodesFilter();

            _dataReader.ExecuteFilters(streamReader, new IFilter<Practice>[] { practiceCountFilter, practicePostcodesFilter });
            return new PracticeResult
            {
                Practices = practicePostcodesFilter.Practices,
                Total = practiceCountFilter.Total
            };
        }

        public PrescriptionCostResult ProcessPrescriptionCost(StreamReader streamReader)
        {
            var prescriptionChapterFilter = new PrescriptionChapterFilter();

            _dataReader.ExecuteFilters(streamReader, new IFilter<PrescriptionCost>[] { prescriptionChapterFilter });

            return new PrescriptionCostResult
            {
                Prescriptions = prescriptionChapterFilter.Prescriptions
            };
        }

        public PrescriptionResult ProcessPrescription(StreamReader streamReader, Dictionary<string, string> prescriptionPostCodes,
            Dictionary<string, byte> prescriptionsTypes)
        {
            var drugTypeFilter = new DrugTypeFilter(prescriptionsTypes);
            var prescriptionAverageActFilter = new PrescriptionAverageActFilter("Peppermint Oil");
            var postcodeSpendFilter = new PostcodeSpendFilter(prescriptionPostCodes);
            var regionSpendFilter = new RegionSpendFilter(prescriptionPostCodes);

            _dataReader.ExecuteFilters(streamReader, new IFilter<Prescription>[] { prescriptionAverageActFilter, postcodeSpendFilter, regionSpendFilter, drugTypeFilter });
            

            return new PrescriptionResult
            {
                Cost = prescriptionAverageActFilter.Cost,
                DrugTypes = drugTypeFilter.DrugTypes,
                PostCodes = postcodeSpendFilter.PostCodes,
                Regions = regionSpendFilter.Regions
            };
        }
    }
}
