using System.IO;

namespace Nhs
{
    public interface INhsProcessor
    {
        PracticeResult ProcessPractice(StreamReader streamReader);
        PrescriptionCostResult ProcessPrescriptionCost(StreamReader streamReader);
    }
}