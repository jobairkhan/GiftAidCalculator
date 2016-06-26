using System;

namespace GiftAidCalculator
{
    using Context.TaxRate;
    namespace Service
    {
        public sealed class GiftAidCalculatorService : IGiftAidCalculatorService
        {
            readonly ITaxRateStore _taxRateStore;
            private IEvent _eventType;

            public GiftAidCalculatorService(ITaxRateStore taxRateStore)
            {
                this._taxRateStore = taxRateStore;
                this._eventType = new NoSupplementEvent();
            }

            public void SetEventType(IEvent eventType)
            {
                this._eventType = eventType;
            }

            public decimal GiftAidAmount(decimal donationAmount)
            {
                var gaFigure = GetGaFigure(donationAmount);

                var supplementFigure = GetSupplementFigure(donationAmount);

                return ApplyRound(gaFigure, supplementFigure);
            }

            private static decimal ApplyRound(decimal gaFigure, decimal supplementFigure)
            {
                int currencyPrecision = 2;
                return Math.Round(gaFigure + supplementFigure, currencyPrecision);
            }

            private decimal GetGaFigure(decimal donationAmount)
            {
                return donationAmount * getRatio(this._taxRateStore.Get());
            }

            private decimal GetSupplementFigure(decimal donationAmount)
            {
                return donationAmount * getRatio(this._eventType.Supplement);
            }

            private static decimal getRatio(decimal percentage)
            {
                return (percentage / (100m - percentage));
            }

            public override string ToString()
            {
                return "Tax Rate: " + _taxRateStore.Get().ToString() + Environment.NewLine
                    + "Seleceted Event: " + this._eventType.Name ?? string.Empty;
            }
        }
    }
}