using System.Linq;
using Nhs.Filters;

namespace Nhs
{
    public class Nhs
    {
        private readonly IFileStorage _fileStorage;
        private readonly INhsProcessor _nhsProcessor;

        public Nhs(IFileStorage fileStorage, INhsProcessor nhsProcessor)
        {
            _fileStorage = fileStorage;
            _nhsProcessor = nhsProcessor;
        }

        public NhsViewModel Execute(string practicePath, string prescriptionPath, string prescriptionCostPath)
        {
            var reader = new DataReader();

            PracticeResult practiceResult;
            using (var practices = _fileStorage.ReadData(practicePath))
            {
               practiceResult = _nhsProcessor.ProcessPractice(practices);
            }

            var prescriptionChapterFilter = new PrescriptionChapterFilter();
            using (var prescriptionCosts = _fileStorage.ReadData(prescriptionCostPath))
            {
                reader.ExecuteFilters(prescriptionCosts, new IFilter<PrescriptionCost>[] { prescriptionChapterFilter });
            }

            var drugTypeFilter = new DrugTypeFilter(prescriptionChapterFilter.Prescriptions);
            var prescriptionAverageActFilter = new PrescriptionAverageActFilter("Peppermint Oil");
            var postcodeSpendFilter = new PostcodeSpendFilter(practiceResult.Practices);
            var regionSpendFilter = new RegionSpendFilter(practiceResult.Practices);
            using (var prescriptions = _fileStorage.ReadData(prescriptionPath))
            {
                reader.ExecuteFilters(prescriptions, new IFilter<Prescription>[] { prescriptionAverageActFilter, postcodeSpendFilter, regionSpendFilter, drugTypeFilter });
            }

            var model = new NhsViewModel
            {
                PracticeCount = practiceResult.Total,
                AverageCost = prescriptionAverageActFilter.Cost,
                DrugTypes = drugTypeFilter.DrugTypes.OrderByDescending(d => d.Count).Take(5),
                RegionPrescripctions = regionSpendFilter.Regions,
                PostCodeSpents = postcodeSpendFilter.PostCodes.OrderByDescending(p => p.Spent).Take(5)
            };

            return model;
        }
    }
}