using System;

namespace Nhs.Filters
{
    public class PrescriptionAverageActFilter : IFilter<Prescription>
    {
        private readonly string _bnfName;
        private decimal _cost;
        private int _count;

        public PrescriptionAverageActFilter(string bnfName)
        {
            _bnfName = bnfName;
        }

        public decimal Cost => _count > 0 ? _cost / _count : 0;

        public void Execute(Prescription prescription)
        {
            if (string.Equals(prescription.BNFName, _bnfName, StringComparison.CurrentCultureIgnoreCase))
            {
                _cost += prescription.ActCost;
                _count++;
            }
        }
    }
}
