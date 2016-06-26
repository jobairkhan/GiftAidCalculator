using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiftAidCalculator.Service.UserContext
{
    public sealed class SiteAdminUser : IUser
    {
        public bool IsAdmin
        {
            get
            {
                return true;
            }
        }
    }
}
