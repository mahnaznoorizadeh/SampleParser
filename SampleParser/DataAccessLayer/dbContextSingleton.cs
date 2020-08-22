
namespace SampleParser.DataAccessLayer
{
    public sealed class dbContextSingleton
    {
        //just for luck
        private static readonly object padlock = new object();
        //just for luck

        public Entity.BankDBModel db = null;
        private static dbContextSingleton instance = null;

        private dbContextSingleton()
        {
            db = new Entity.BankDBModel();
        }
        public static dbContextSingleton GetInstance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new dbContextSingleton();
                        }
                    }
                }
                return instance;
            }
            set { }
        }


    }//end class
}