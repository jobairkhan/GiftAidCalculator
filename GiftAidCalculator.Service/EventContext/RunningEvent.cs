using System;

namespace GiftAidCalculator.Service
{
    public class RunningEvent : IEvent
    {
        string eventName;
        public string Name
        {
            get { return eventName; }
        }

        decimal eventSupplimentPercentage;
        public decimal Supplement
        {
            get { return eventSupplimentPercentage; }
        }

        public RunningEvent(decimal percentage)
        {
            this.eventName = "Running";
            this.eventSupplimentPercentage = percentage;
        }
    }
}