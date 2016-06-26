using System;

namespace GiftAidCalculator
{ 
    using Context.Users;
    namespace Context.TaxRate
    {
        public class TaxRateStore : ITaxRateStore
        {
            internal static TaxRate CurrentRate { get; set; } = new TaxRate(20m);

            public IUser user { get; private set; }


            public TaxRateStore()
            {
                this.user = new DonorUser();
            }

            public TaxRateStore(IUser loggedInuser)
            {
                this.user = loggedInuser;
            }

            private object handleLock = new object();
            public void Set(decimal taxRateArg)
            {
                if (user.IsAdmin)
                {
                    lock (handleLock)
                    {
                        CurrentRate = new TaxRate(taxRateArg);
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException("Access Denied!");
                }
            }

            public decimal Get()
            {
                return CurrentRate.Value;
            }
        }
    }
}