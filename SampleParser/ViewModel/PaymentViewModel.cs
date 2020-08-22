using System;

namespace SampleParser.ViewModel
{
    public class PaymentViewModel : BaseViewModel
    {
        public string KeyId { get; set; }
        public DateTime? DateOfPayment { get; set; } = null;
        public string Description { get; set; }
        public decimal? Sum { get; set; } = null;
        public decimal? Balance { get; set; } = null;
    }
}