using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiftAidCalculator.Service
{
    public interface ITaxRateStore
    {
        decimal Get();
        void Set(decimal taxRateArg);        
    }
}
