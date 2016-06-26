using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiftAidCalculator.Service.UserContext
{
    public sealed class DonorUser : IUser
    {
        public bool IsAdmin
        {
            get
            {
                return false;
            }
        }
    }
}
