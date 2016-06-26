namespace GiftAidCalculator.Service
{
    public class NoSupplementEvent : IEvent
    {
        string eventName;
        public string Name
        {
            get { return eventName; }
        }
        
        public decimal Supplement
        {
            get { return 0; }
        }

        public NoSupplementEvent()
        {
            this.eventName = "Others";
        }
    }
}