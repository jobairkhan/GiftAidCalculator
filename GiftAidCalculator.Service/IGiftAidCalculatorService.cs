using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiftAidCalculator.Service
{
    public interface IGiftAidCalculatorService
    {
        decimal GiftAidAmount(decimal donateAmount);
    }
}
