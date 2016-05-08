using System.IO;
using System.Linq;
using Nhs.Filters;

namespace Nhs
{
    public class Nhs
    {
        public NhsViewModel Execute(string practicePath, string prescriptionPath, string prescriptionCostPath)
        {
            var reader = new DataReader();

            var practiceCountFilter = new PracticeCountFilter(new[]
            {
                "E", "EC", "N", "NW", "SE", "SW", "W", "WC" //London postcodes
            });
            var practicePostcodesFilter = new PracticePostcodesFilter();
            using (var practices = File.OpenText(practicePath))
            {
                reader.ExecuteFilters(practices, new IFilter<Practice>[] { practiceCountFilter, practicePostcodesFilter });
            }

            var prescriptionChapterFilter = new PrescriptionChapterFilter();
            using (var prescriptionCosts = File.OpenText(prescriptionCostPath))
            {
                reader.ExecuteFilters(prescriptionCosts, new IFilter<PrescriptionCost>[] { prescriptionChapterFilter });
            }

            var drugTypeFilter = new DrugTypeFilter(prescriptionChapterFilter.Prescriptions);
            var prescriptionAverageActFilter = new PrescriptionAverageActFilter("Peppermint Oil");
            var postcodeSpendFilter = new PostcodeSpendFilter(practicePostcodesFilter.Practices);
            var regionSpendFilter = new RegionSpendFilter(practicePostcodesFilter.Practices);
            using (var prescriptions = File.OpenText(prescriptionPath))
            {
                reader.ExecuteFilters(prescriptions, new IFilter<Prescription>[] { prescriptionAverageActFilter, postcodeSpendFilter, regionSpendFilter, drugTypeFilter });
            }

            var model = new NhsViewModel
            {
                PracticeCount = practiceCountFilter.Total,
                AverageCost = prescriptionAverageActFilter.Cost,
                DrugTypes = drugTypeFilter.DrugTypes.OrderByDescending(d => d.Count).Take(5),
                RegionPrescripctions = regionSpendFilter.Regions,
                PostCodeSpents = postcodeSpendFilter.PostCodes.OrderByDescending(p => p.Spent).Take(5)
            };

            return model;
        }
    }
}