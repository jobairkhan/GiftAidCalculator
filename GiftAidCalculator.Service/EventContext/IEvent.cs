using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiftAidCalculator.Service
{
    public interface IEvent
    {
        string Name { get; }
        decimal Supplement { get; }
    }
}
