using System.Collections.Generic;
using System.IO;

namespace Nhs
{
    public interface ICsvSerializer
    {
        IEnumerable<Practice> DeserializePractices(StreamReader streamReader);
        IEnumerable<Prescription> DeserializePrescriptions(StreamReader streamReader);
        IEnumerable<PrescriptionCost> DeserializePrescriptionCosts(StreamReader streamReader);
    }
}