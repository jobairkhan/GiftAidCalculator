using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiftAidCalculator.Service
{
    public interface IUser
    {
        bool IsAdmin { get; }
    }
}
