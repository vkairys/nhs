using System.Collections.Generic;

namespace Nhs
{
    public class NhsViewModel
    {
        public IEnumerable<DrugType> DrugTypes { get; set; }
        public IEnumerable<RegionPrescripctions> RegionPrescripctions { get; set; }
        public IEnumerable<PostCodeSpent> PostCodeSpents { get; set; }
        public decimal AverageCost { get; set; }
        public int PracticeCount { get; set; }
    }

    public class RegionPrescripctions
    {
        public string Region { get; set; }
        public decimal Spent { get; set; }
        public decimal Diff { get; set; }
    }

    public class DrugType
    {
        public string Name { get; set; }
        public double Count { get; set; }
    }

    public class PostCodeSpent
    {
        public string Name { get; set; }
        public decimal Spent { get; set; }
    }
}
