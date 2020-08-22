
namespace SampleParser.Models
{
    public class BankDataOrder
    {
        //automatic or manual set headers
        public bool? Automatic { get; set; }
        public int DateOfPayment { get; set; }
        public int Description { get; set; }
        public int Sum { get; set; }
        public int Balance { get; set; }

    }
}