using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiftAidCalculator.Service
{
    public sealed class GiftAidCalculatorService : IGiftAidCalculatorService
    {
        private IEvent _eventType;
        readonly ITaxRateStore _taxRateStore;

        public GiftAidCalculatorService(ITaxRateStore taxRateStore)
        {
            _taxRateStore = taxRateStore;
            _eventType = new NoSupplementEvent();
        }

        public GiftAidCalculatorService(ITaxRateStore currentTaxRate, IEvent eventType)
        {
            this._taxRateStore = currentTaxRate;
            this._eventType = eventType;
        }

        public decimal GiftAidAmount(decimal donationAmount)
        {
            var gaFigure = GetGaFigure(donationAmount);

            var supplementFigure = GetSupplementFigure(donationAmount);
            return Math.Round(gaFigure + supplementFigure, 2);
        }

        private decimal GetGaFigure(decimal donationAmount)
        {
            return donationAmount * getRatio(_taxRateStore.Get());
        }

        private decimal GetSupplementFigure(decimal donationAmount)
        {
            return donationAmount * getRatio(_eventType.Supplement);
        }
        

        private static decimal getRatio(decimal percentage)
        {
            return (percentage / (100m - percentage));
        }
    }
}
