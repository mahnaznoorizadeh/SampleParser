using SampleParser.DataAccessLayer.Entity;
using SampleParser.ViewModel;
using System;

namespace SampleParser.DataAccessLayer.ModelMapper
{
    public class Mapper
    {
        #region Payment
        internal static Payment Map(PaymentViewModel viewModel)
        {
            try
            {
                if (viewModel == null)
                {
                    return null;
                }

                Payment payment = new Payment();
                payment.Id = viewModel.Id;
                payment.KeyId = viewModel.KeyId;
                payment.DateOfPayment = viewModel.DateOfPayment;
                payment.Description = viewModel.Description;
                payment.Sum = viewModel.Sum;
                payment.Balance = viewModel.Balance;
                return payment;

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        internal static PaymentViewModel Map(Payment entity)
        {
            try
            {
                if (entity == null)
                {
                    return null;
                }
                
                PaymentViewModel payment = new PaymentViewModel();
                payment.Id = entity.Id;
                payment.KeyId = entity.KeyId;
                payment.DateOfPayment = entity.DateOfPayment;
                payment.Description = entity.Description;
                payment.Sum = entity.Sum;
                payment.Balance = entity.Balance;
                return payment;

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
    }
}