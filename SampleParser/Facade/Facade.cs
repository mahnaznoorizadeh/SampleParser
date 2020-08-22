using SampleParser.DataAccessLayer.Entity;
using SampleParser.DataAccessLayer.Implementation;
using SampleParser.DataAccessLayer.ModelMapper;
using SampleParser.ViewModel;
using System;
using System.Data.Entity.Core.Objects.DataClasses;

namespace SampleParser.Facade
{
    public static class Facade
    {
        public static PaymentViewModel GetPayment(DateTime dateOfPayment, string description, decimal sum, decimal balance)
        {
            var result = new PaymentDao().GetPayment(dateOfPayment, description, sum, balance);
            return Mapper.Map(result);
        }

        public static int SavePayment(PaymentViewModel paymentVM)
        {
            Payment p = Mapper.Map(paymentVM);
            var result = new PaymentDao().Save(p);
            return result;
        }


    }
}