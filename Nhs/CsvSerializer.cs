using System.Collections.Generic;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;

namespace Nhs
{
    public class CsvSerializer : ICsvSerializer
    {
        public IEnumerable<Practice> DeserializePractices(StreamReader streamReader)
        {
            var configurations = new CsvConfiguration
            {
                HasHeaderRecord = false,
                TrimFields = true
            };

            return ReadRecords<Practice>(streamReader, configurations);
        }

        public IEnumerable<Prescription> DeserializePrescriptions(StreamReader streamReader)
        {
            var configurations = new CsvConfiguration
            {
                IgnoreHeaderWhiteSpace = true,
                IsHeaderCaseSensitive = false,
                TrimFields = true
            };

            return ReadRecords<Prescription>(streamReader, configurations);
        }

        public IEnumerable<PrescriptionCost> DeserializePrescriptionCosts(StreamReader streamReader)
        {
            var configurations = new CsvConfiguration();
            configurations.RegisterClassMap<PrescriptionCostClassMap>();

            return ReadRecords<PrescriptionCost>(streamReader, configurations);
        }

        private IEnumerable<T> ReadRecords<T>(StreamReader streamReader, CsvConfiguration configuration)
        {
            using (var reader = new CsvReader(streamReader, configuration))
            {
                while (reader.Read())
                {
                    yield return reader.GetRecord<T>();
                }
            }
        }
    }
}