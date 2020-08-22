using SampleParser.DataAccessLayer.Entity;
using SampleParser.DataAccessLayer.Interface;
using SampleParser.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleParser.DataAccessLayer.Implementation
{
    public class PaymentDao : IPaymentDao
    {
        public int Save(Payment payment)
        {
            try
            {
                using (var context = new BankDBModel())
                {
                    payment.InsertDateTime = DateTime.Now;
                    payment.IsDeleted = false;
                    
                    context.Payment.Add(payment);
                    context.SaveChanges();
                    
                    return payment.Id;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        public Payment GetPayment(DateTime dateOfPayment, string description, decimal sum, decimal balance)
        {
            try
            {
                using (var context = new BankDBModel())
                {
                    var p = context.Payment.Where(
                        x => x.DateOfPayment == dateOfPayment &&
                             x.Description == description &&
                             x.Sum == sum &&
                             x.Balance == balance

                        ).FirstOrDefault();

                    return p;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PaymentViewModel> GetAllPayments()
        {
            try
            {
                using (var context = new BankDBModel())
                {
                    var lst = context.Payment.Select(x => new PaymentViewModel()
                    {
                        Id = x.Id,
                        KeyId = x.KeyId,
                        DateOfPayment = x.DateOfPayment,
                        Description = x.Description,
                        Sum = x.Sum,
                        Balance = x.Balance
                    }).ToList();

                    return lst;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}