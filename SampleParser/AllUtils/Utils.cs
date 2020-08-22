using Newtonsoft.Json.Linq;
using SampleParser.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace SampleParser.AllUtils
{
    public class Utils
    {
        private static string fileName = "db.txt";

        //Save data to csv file
        public static void SaveToFile(string str)
        {
            try
            {
                string executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string filePath = Path.Combine(executableLocation, fileName);

                if (!File.Exists(filePath))
                {
                    // Create a file to write to.
                    using (StreamWriter sw = File.CreateText(filePath))
                    {
                        sw.WriteLine(str);
                    }
                }
                else
                    using (StreamWriter sw = File.AppendText(filePath))
                    {
                        sw.WriteLine(str);
                    }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Read data from csv file
        public static List<PaymentViewModel> ReadFromFile()
        {
            List<PaymentViewModel> result = new List<PaymentViewModel>();

            string executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string filePath = Path.Combine(executableLocation, fileName);

            try
            {
                if (!File.Exists(filePath))
                {
                    return result;
                }

                // Open the file to read from.
                using (StreamReader sr = File.OpenText(filePath))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        result.Add(JsonToBankObject(s));

                    }
                }
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static PaymentViewModel JsonToBankObject(string json)
        {
            try
            {
                JObject jObject = JObject.Parse(json);
                PaymentViewModel payment = new PaymentViewModel();
                payment.DateOfPayment = (DateTime)jObject["DateOfPayment"];
                payment.Description = (string)jObject["Description"];
                payment.Sum = (decimal)jObject["Sum"];
                payment.Balance = (decimal)jObject["Balance"];
                payment.KeyId = (string)jObject["KeyId"];

                return payment;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static bool IsNumber(string str, out decimal number)
        {
            try
            {
                bool validate = false;
                var style = System.Globalization.NumberStyles.Number;
                CultureInfo seCultureInfo = new CultureInfo("SV-SE", false);

                if (Decimal.TryParse(str, style, seCultureInfo, out number))
                    validate = true;

                return validate;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static bool IsDate(string str, out DateTime dt)
        {
            try
            {
                CultureInfo seCultureInfo = new CultureInfo("SV-SE", false);

                bool isValid = DateTime.TryParseExact(
                    str,
                    "yyyy-MM-dd",
                    seCultureInfo,
                    DateTimeStyles.None,
                    out dt);

                return isValid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}