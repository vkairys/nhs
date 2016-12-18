using System.Collections.Generic;

namespace Nhs
{
    public class PracticeResult
    {
        public int Total { get; set; }
        public Dictionary<string, string> Practices { get; set; }

        public PracticeResult()
        {
            Practices = new Dictionary<string, string>();
        }
    }
}