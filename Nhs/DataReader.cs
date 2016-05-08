using System.Collections.Generic;
using System.IO;
using CsvHelper;

namespace Nhs
{
    public class DataReader
    {
        public void ExecuteFilters(TextReader data, IEnumerable<IFilter<Practice>> filters)
        {
            using (var reader = new CsvReader(data))
            {
                reader.Configuration.HasHeaderRecord = false;
                while (reader.Read())
                {
                    var practice = reader.GetRecord<Practice>();
                    foreach (var filter in filters)
                    {
                        filter.Execute(practice);
                    }
                }
            }
        }

        public void ExecuteFilters(TextReader data, IEnumerable<IFilter<Prescription>> filters)
        {
            using (var reader = new CsvReader(data))
            {
                reader.Configuration.IgnoreHeaderWhiteSpace = true;
                reader.Configuration.IsHeaderCaseSensitive = false;
                reader.Configuration.TrimFields = true;
                while (reader.Read())
                {
                    var prescription = reader.GetRecord<Prescription>();
                    foreach (var filter in filters)
                    {

                        filter.Execute(prescription);
                    }
                }
            }
        }

        public void ExecuteFilters(TextReader data, IEnumerable<IFilter<PrescriptionCost>> filters)
        {
            using (var reader = new CsvReader(data))
            {
                reader.Configuration.RegisterClassMap<PrescriptionCostClassMap>();
                while (reader.Read())
                {
                    var prescriptionCost = reader.GetRecord<PrescriptionCost>();
                    foreach (var filter in filters)
                    {
                        filter.Execute(prescriptionCost);
                    }
                }
            }
        }
    }
}
