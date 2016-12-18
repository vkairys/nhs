using System.Collections.Generic;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;

namespace Nhs
{
    public class CsvSerializer : ICsvSerializer
    {
        public IEnumerable<Practice> DeserializePractices(TextReader streamReader)
        {
            var configurations = new CsvConfiguration
            {
                HasHeaderRecord = false,
                TrimFields = true
            };

            return ReadRecords<Practice>(streamReader, configurations);
        }

        public IEnumerable<Prescription> DeserializePrescriptions(TextReader textReader)
        {
            var configurations = new CsvConfiguration
            {
                IgnoreHeaderWhiteSpace = true,
                IsHeaderCaseSensitive = false,
                TrimFields = true
            };

            return ReadRecords<Prescription>(textReader, configurations);
        }

        public IEnumerable<PrescriptionCost> DeserializePrescriptionCosts(TextReader textReader)
        {
            var configurations = new CsvConfiguration();
            configurations.RegisterClassMap<PrescriptionCostClassMap>();

            return ReadRecords<PrescriptionCost>(textReader, configurations);
        }

        private IEnumerable<T> ReadRecords<T>(TextReader textReader, CsvConfiguration configuration)
        {
            using (var reader = new CsvReader(textReader, configuration))
            {
                while (reader.Read())
                {
                    yield return reader.GetRecord<T>();
                }
            }
        }
    }
}