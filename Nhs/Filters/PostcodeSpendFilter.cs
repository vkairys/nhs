using System.Collections.Generic;
using System.Linq;

namespace Nhs.Filters
{
    public class PostcodeSpendFilter : IFilter<Prescription>
    {
        private readonly Dictionary<string, PostCodeSpent> _practices;

        public List<PostCodeSpent> PostCodes
        {
            get
            {
                return
                    _practices.GroupBy(p => p.Value.Name)
                        .Select(p =>
                        new PostCodeSpent
                        {
                            Name = p.Key,
                            Spent = p.Sum(kk => kk.Value.Spent)
                        }).ToList();
            }
        }

        public PostcodeSpendFilter(Dictionary<string, string> practices)
        {
            _practices = practices.ToDictionary(p => p.Key, v => new PostCodeSpent
            {
                Name = v.Value
            });
        }

        public void Execute(Prescription prescription)
        {
            if (_practices.ContainsKey(prescription.Practice))
            {
                var postCode = _practices[prescription.Practice];
                postCode.Spent += prescription.ActCost;
            }
        }
    }
}