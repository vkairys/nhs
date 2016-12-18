using System.IO;
using Nhs.Filters;

namespace Nhs
{
    public class NhsProcessor : INhsProcessor
    {
        public PracticeResult ProcessPractice(StreamReader streamReader)
        {
            var practiceCountFilter = new PracticeCountFilter(new[]
            {
                "E", "EC", "N", "NW", "SE", "SW", "W", "WC" //London postcodes
            });

            var practicePostcodesFilter = new PracticePostcodesFilter();

            var dataReader = new DataReader();
            dataReader.ExecuteFilters(streamReader, new IFilter<Practice>[] { practiceCountFilter, practicePostcodesFilter });
            return new PracticeResult
            {
                Practices = practicePostcodesFilter.Practices,
                Total = practiceCountFilter.Total
            };
        }
    }
}
