using System.Collections.Generic;
using System.Linq;

namespace Nhs.Filters
{
    public class RegionSpendFilter : IFilter<Prescription>
    {
        private readonly IDictionary<string, string> _practices;
        private readonly Dictionary<string, Region> _postcodes = new Dictionary<string, Region>();
        private readonly Region[] _regions = {
                new Region
                {
                    Name = "South East",
                    Postcodes = new[]
                    {
                      "BN","CT","GU","ME","RH","SO","TN","MK","PO","RG","SL","OX"
                    }
                },
                new Region
                {
                    Name = "London",
                    Postcodes = new[]
                    {
                        "BR", "CR","DA","E","EC","EN","HA","IG","KT","N","NW","RM","SE","SM","SW","TW","UB","W","WC","WD"
                    }
                },
                new Region
                {
                    Name = "North West",
                    Postcodes = new[]
                    {
                        "BB", "BD", "BL", "CA", "CH", "CW", "FY", "HD", "HX", "IM", "L", "LA", "M","OL", "PR", "SK", "WA","WN"
                    }
                },
                new Region
                {
                    Name = "East of England",
                    Postcodes = new[]
                    {
                         "CB","CM","CO","IP","LN","NR","PE","SS","AL","LU","SG","HP"
                    }
                },
                new Region
                {
                    Name = "West Midlands",
                    Postcodes = new[]
                    {
                        "B", "CV", "DY", "HR", "ST", "TF", "WR", "WS", "WV","SY","LD"
                    }
                },
                new Region
                {
                    Name = "South West",
                    Postcodes = new[]
                    {
                        "BA","BH","BS","DT","EX","GL","PL","SN","SP","TA","TQ","TR"
                    }
                },
                new Region
                {
                    Name = "Yorkshire and the Humber",
                    Postcodes = new[]
                    {
                        "DN","HG","HU","LS","S","WF","YO"
                    }
                },
                new Region
                {
                    Name = "East Midlands",
                    Postcodes = new[]
                    {

                      "DE","LE","NG","NN"
                    }
                },
                new Region
                {
                    Name = "North East",
                    Postcodes = new[]
                    {
                        "DH", "DL", "NE", "SR", "TS","TD"
                    }
                }
            };

        public IEnumerable<RegionPrescripctions> Regions
        {
            get
            {
                var total = _regions.Sum(r => r.ItemCount);
                var nationalAverage = total > 0 ? _regions.Sum(r => r.TotalPrice) / total : 0;

                return _regions.Select(r =>
                {
                    var spent = r.ItemCount > 0 ? r.TotalPrice / r.ItemCount : 0;
                    return
                        new RegionPrescripctions
                        {
                            Region = r.Name,
                            Spent = r.ItemCount > 0 ? r.TotalPrice / r.ItemCount : 0,
                            Diff = nationalAverage > 0
                                ? (spent - nationalAverage) / nationalAverage
                                : 0
                        };
                });
            }
        }


        public RegionSpendFilter(Dictionary<string, string> practices)
        {
            _practices = practices;
            foreach (var region in _regions)
            {
                foreach (var postCode in region.Postcodes)
                {
                    _postcodes.Add(postCode, region);
                }
            }
        }

        public void Execute(Prescription prescription)
        {
            string postCode;
            if (_practices.TryGetValue(prescription.Practice, out postCode))
            {
                var postCodeLength = char.IsDigit(postCode[1]) ? 1 : 2;
                var areaPostCode = postCode.Substring(0, postCodeLength);

                _postcodes[areaPostCode].TotalPrice += prescription.ActCost;
                _postcodes[areaPostCode].ItemCount++;
            }
        }

        private class Region
        {
            public string Name { get; set; }
            public string[] Postcodes { get; set; }
            public decimal TotalPrice { get; set; }
            public int ItemCount { get; set; }
        }
    }
}
