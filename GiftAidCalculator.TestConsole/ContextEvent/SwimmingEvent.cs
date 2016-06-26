namespace GiftAidCalculator.Service
{
    public class SwimmingEvent : IEvent
    {
        string eventName;
        public string Name
        {
            get
            {
                return eventName;
            }
        }

        decimal eventSupplementPercentage;
        public decimal Supplement
        {
            get
            {
                return eventSupplementPercentage;
            }
        }

        public SwimmingEvent(decimal percentage)
        {
            this.eventName = "Swimming";
            this.eventSupplementPercentage = percentage;
        }
    }
}