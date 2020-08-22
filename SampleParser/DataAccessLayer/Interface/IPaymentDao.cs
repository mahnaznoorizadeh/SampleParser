using SampleParser.DataAccessLayer.Entity;
using SampleParser.ViewModel;
using System.Collections.Generic;

namespace SampleParser.DataAccessLayer.Interface
{
    public interface IPaymentDao
    {
        int Save(Payment payment);
        List<PaymentViewModel> GetAllPayments();
    }
}
