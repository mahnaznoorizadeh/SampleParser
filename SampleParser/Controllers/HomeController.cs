using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SampleParser.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using SampleParser.AllUtils;
using System.Text.RegularExpressions;
using SampleParser.DataAccessLayer.Entity;
using SampleParser.ViewModel;
using SampleParser.DataAccessLayer.Implementation;

namespace SampleParser.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Report()
        {
            List<PaymentViewModel> paymentsList = new PaymentDao().GetAllPayments();

            return View(paymentsList);
        }

        public void Test()
        {
            Response.Write("This is a Test!");
        }

        [HttpPost]
        public string ParseData()
        {
            try
            {
                BankDataOrder order = new BankDataOrder();

                string BankTransaction = Request.Form["BankTransaction"];
                order.Automatic = (Request.Form["Automatic"] == "true" ? true : false);
                if (order.Automatic == false)
                {
                    order.DateOfPayment = Int16.Parse(Request.Form["DateOfPayment"]);
                    order.Description = Int16.Parse(Request.Form["Description"]);
                    order.Sum = Int16.Parse(Request.Form["Sum"]);
                    order.Balance = Int16.Parse(Request.Form["Balance"]);
                }

                var result = ParseBankTransaction(BankTransaction, order);
                return result.ToString();

            }
            catch (Exception ex)
            {
                return "{error:true, exeption:" + ex.Message + "}";
            }
        }

        [HttpGet]
        public string ReadData()
        {
            try
            {

                List<PaymentViewModel> paymentList = new List<PaymentViewModel>();
                paymentList = Utils.ReadFromFile();
                var jsonSerialiser = new JavaScriptSerializer();
                string json = JsonConvert.SerializeObject(paymentList,
                    new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd" });

                return json;

            }
            catch (Exception ex)
            {
                return "{error:true, exeption:" + ex.Message + "}";

            }
        }


        //parse bank transactions 
        private int ParseBankTransaction(string BankTransaction, BankDataOrder order)
        {
            try
            {
                string[] row;
                int countRecords = 0;
                List<PaymentViewModel> listPayments = new List<PaymentViewModel>();


                foreach (var myString in BankTransaction.Split(new string[] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    String[] spearator = { "\t" };
                    var count = Regex.Matches(myString, "\t").Count;
                    if (count < 3)
                        continue;

                    row = myString.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
                    PaymentViewModel paymentVM = new PaymentViewModel();

                    paymentVM = (order.Automatic == true ? ParseAutomatic(row) : ParseManual(row, order));

                    var p = Facade.Facade.GetPayment(paymentVM.DateOfPayment.Value, paymentVM.Description, paymentVM.Sum.Value, paymentVM.Balance.Value);

                    if (PaymentIsValid(paymentVM) && p == null)
                    {
                        var r = Facade.Facade.SavePayment(paymentVM);
                        if (r > 0)
                        {
                            countRecords++;
                        }
                    }

                }

                return countRecords;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Automatic Parsing Rows
        private PaymentViewModel ParseAutomatic(string[] row)
        {
            try
            {
                PaymentViewModel payment = new PaymentViewModel();
                DateTime dt;
                decimal number;

                foreach (var col in row)
                {
                    if (col.Trim() == "")
                        continue;

                    if (Utils.IsDate(col, out dt))
                    {
                        if (payment.DateOfPayment == null)
                        {
                            payment.DateOfPayment = dt.Date;
                            continue;
                        }
                        continue;
                    }

                    else if (Utils.IsNumber(col, out number))
                    {
                        if (payment.Sum == null)
                            payment.Sum = number;
                        else if (payment.Balance == null)
                            payment.Balance = number;

                        continue;
                    }
                    else if (payment.Description == null && payment.DateOfPayment != null)
                        payment.Description = col;

                }
                payment.KeyId = MakeKeyIdFromPaymentObj(payment);
                return payment;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private PaymentViewModel ParseManual(string[] row, BankDataOrder order)
        {
            try
            {
                PaymentViewModel payment = new PaymentViewModel();
                DateTime dt;
                decimal number;

                List<string> r = RemoveEmptyCols(row);

                if (Utils.IsDate(r[order.DateOfPayment - 1], out dt))
                    payment.DateOfPayment = dt;

                if (Utils.IsNumber(r[order.Sum - 1], out number))
                    payment.Sum = number;

                if (Utils.IsNumber(r[order.Balance - 1], out number))
                    payment.Balance = number;

                payment.Description = r[order.Description - 1];

                payment.KeyId = MakeKeyIdFromPaymentObj(payment);

                return payment;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<string> RemoveEmptyCols(string[] row)
        {
            List<string> result = new List<string>();

            foreach (var col in row)
            {
                if (col.Trim() == "")
                    continue;

                result.Add(col);
            }
            return result;
        }

        //check payment object is valid or not
        private bool PaymentIsValid(PaymentViewModel payment)
        {
            try
            {
                if (payment.DateOfPayment == null ||
                    payment.Sum == null ||
                    payment.Balance == null)
                    return false;

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string MakeKeyIdFromPaymentObj(PaymentViewModel payment)
        {
            string keyId = "";
            keyId += (payment.DateOfPayment != null ? payment.DateOfPayment.Value.ToString("yyyy/MM/dd") : "");
            keyId += payment.Sum;
            keyId += payment.Balance;

            return keyId;
        }

    }
}