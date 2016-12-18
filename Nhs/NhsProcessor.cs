using System.Collections.Generic;
using System.IO;
using Nhs.Filters;

namespace Nhs
{
    public class NhsProcessor : INhsProcessor
    {
        private readonly ICsvSerializer _csvSerializer;

        public NhsProcessor(ICsvSerializer csvSerializer)
        {
            _csvSerializer = csvSerializer;
        }

        public PracticeResult ProcessPractice(TextReader textReader)
        {
            var practiceCountFilter = new PracticeCountFilter(new[]
            {
                "E", "EC", "N", "NW", "SE", "SW", "W", "WC" //London postcodes
            });

            var practicePostcodesFilter = new PracticePostcodesFilter();

            var pratices = _csvSerializer.DeserializePractices(textReader);

            Filter(pratices, practiceCountFilter, practicePostcodesFilter);

            return new PracticeResult
            {
                Practices = practicePostcodesFilter.Practices,
                Total = practiceCountFilter.Total
            };
        }

        public PrescriptionCostResult ProcessPrescriptionCost(TextReader textReader)
        {
            var prescriptionChapterFilter = new PrescriptionChapterFilter();

            var prescriptionCosts = _csvSerializer.DeserializePrescriptionCosts(textReader);

            Filter(prescriptionCosts, prescriptionChapterFilter);

            return new PrescriptionCostResult
            {
                Prescriptions = prescriptionChapterFilter.Prescriptions
            };
        }

        public PrescriptionResult ProcessPrescription(TextReader textReader, Dictionary<string, string> prescriptionPostCodes,
            Dictionary<string, byte> prescriptionsTypes)
        {
            var drugTypeFilter = new DrugTypeFilter(prescriptionsTypes);
            var prescriptionAverageActFilter = new PrescriptionAverageActFilter("Peppermint Oil");
            var postcodeSpendFilter = new PostcodeSpendFilter(prescriptionPostCodes);
            var regionSpendFilter = new RegionSpendFilter(prescriptionPostCodes);

            var prescriptions = _csvSerializer.DeserializePrescriptions(textReader);

            Filter(prescriptions, drugTypeFilter, prescriptionAverageActFilter, postcodeSpendFilter, regionSpendFilter);

            return new PrescriptionResult
            {
                Cost = prescriptionAverageActFilter.Cost,
                DrugTypes = drugTypeFilter.DrugTypes,
                PostCodes = postcodeSpendFilter.PostCodes,
                Regions = regionSpendFilter.Regions
            };
        }

        private void Filter<T>(IEnumerable<T> entities, params IFilter<T>[] filters)
        {
            foreach (var entity in entities)
            {
                foreach (var filter in filters)
                {
                    filter.Execute(entity);
                }
            }
        }
    }
}
