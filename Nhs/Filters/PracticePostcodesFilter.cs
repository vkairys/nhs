using System.Collections.Generic;

namespace Nhs.Filters
{
    public class PracticePostcodesFilter : IFilter<Practice>
    {
        public Dictionary<string, string> Practices { get; }

        public PracticePostcodesFilter()
        {
            Practices = new Dictionary<string, string>();
        }

        public void Execute(Practice prescription)
        {
            Practices.Add(prescription.Id, prescription.PostCode);
        }
    }
}
