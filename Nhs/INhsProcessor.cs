using System.Collections.Generic;
using System.IO;

namespace Nhs
{
    public interface INhsProcessor
    {
        PracticeResult ProcessPractice(StreamReader streamReader);
        PrescriptionCostResult ProcessPrescriptionCost(StreamReader streamReader);
        PrescriptionResult ProcessPrescription(StreamReader streamReader, Dictionary<string, string> prescriptionPostCodes,
            Dictionary<string, byte> prescriptionsTypes);
    }
}