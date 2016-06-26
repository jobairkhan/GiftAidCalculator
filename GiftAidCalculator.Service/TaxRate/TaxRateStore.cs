using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiftAidCalculator.Service
{
    internal class TaxRateStore : ITaxRateStore
    {
        internal static TaxRate CurrentRate { get; set; } = new TaxRate(20m);

        public IUser user { get; private set; }

        internal TaxRateStore(IUser loggedInuser)
        {
            if (loggedInuser.GetType() != typeof(UserContext.SiteAdminUser))
            {
                throw new UnauthorizedAccessException("Access Denied!");
            }
            this.user = loggedInuser;
        }

        public void Set(decimal taxRateArg)
        {
            if (user.IsAdmin)
            {
                CurrentRate = new TaxRate(taxRateArg);
            }
            else
            {
                throw new UnauthorizedAccessException("Access Denied");
            }
        }

        public decimal Get()
        {
            return CurrentRate.Value;
        }
    }
}
