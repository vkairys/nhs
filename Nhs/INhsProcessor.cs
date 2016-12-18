using System.Collections.Generic;
using System.IO;

namespace Nhs
{
    public interface INhsProcessor
    {
        PracticeResult ProcessPractice(TextReader textReader);
        PrescriptionCostResult ProcessPrescriptionCost(TextReader textReader);
        PrescriptionResult ProcessPrescription(TextReader textReader, Dictionary<string, string> prescriptionPostCodes,
            Dictionary<string, byte> prescriptionsTypes);
    }
}