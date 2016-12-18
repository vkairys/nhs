using System.Linq;

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
            PracticeResult practiceResult;
            using (var practices = _fileStorage.ReadData(practicePath))
            {
                practiceResult = _nhsProcessor.ProcessPractice(practices);
            }

            PrescriptionCostResult prescriptionCostResult;
            using (var prescriptionCosts = _fileStorage.ReadData(prescriptionCostPath))
            {
                prescriptionCostResult = _nhsProcessor.ProcessPrescriptionCost(prescriptionCosts);
            }

            PrescriptionResult prescriptionResult;
            using (var prescriptions = _fileStorage.ReadData(prescriptionPath))
            {
                prescriptionResult = _nhsProcessor.ProcessPrescription(prescriptions, practiceResult.Practices,
                    prescriptionCostResult.Prescriptions);
            }

            var model = new NhsViewModel
            {
                PracticeCount = practiceResult.Total,
                AverageCost = prescriptionResult.Cost,
                DrugTypes = prescriptionResult.DrugTypes.OrderByDescending(d => d.Count).Take(5),
                RegionPrescripctions = prescriptionResult.Regions,
                PostCodeSpents = prescriptionResult.PostCodes.OrderByDescending(p => p.Spent).Take(5)
            };

            return model;
        }
    }
}