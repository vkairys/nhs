using System.Collections.Generic;
using System.IO;

namespace Nhs
{
    public interface ICsvSerializer
    {
        IEnumerable<Practice> DeserializePractices(TextReader textReader);
        IEnumerable<Prescription> DeserializePrescriptions(TextReader textReader);
        IEnumerable<PrescriptionCost> DeserializePrescriptionCosts(TextReader textReader);
    }
}