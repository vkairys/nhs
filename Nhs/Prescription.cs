namespace Nhs
{
    public class Prescription
    {
        public string SHA { get; set; }
        public string PCT { get; set; }
        public string Practice { get; set; }
        public string BNFCode { get; set; }
        public string BNFName { get; set; }
        public int Items { get; set; }
        public decimal NIC { get; set; }
        public decimal ActCost { get; set; }
        public int Period { get; set; }
    }
}
