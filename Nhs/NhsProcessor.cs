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
    }
}
