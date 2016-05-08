using System.Linq;

namespace Nhs.Filters
{
    public class PracticeCountFilter : IFilter<Practice>
    {
        private readonly string[] _postCodes;

        public PracticeCountFilter(string[] postCodes)
        {
            _postCodes = postCodes;
        }

        public int Total { get; private set; }

        public void Execute(Practice prescription)
        {
            var postCodeLength = char.IsDigit(prescription.PostCode[1]) ? 1 : 2;
            var areaPostCode = prescription.PostCode.Substring(0, postCodeLength);

            if (_postCodes.Contains(areaPostCode))
            {
                Total++;
            }
        }
    }
}
