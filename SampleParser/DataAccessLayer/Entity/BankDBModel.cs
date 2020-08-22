using System.Data.Entity;

namespace SampleParser.DataAccessLayer.Entity
{
    public class BankDBModel : DbContext
    {
        public BankDBModel() :

        base(@"Data Source=.;Initial Catalog=BankPayment;Integrated Security=True;")
        {
            Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<Payment> Payment { get; set; }

    }
}