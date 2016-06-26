namespace GiftAidCalculator.Service
{
    public interface IGiftAidCalculatorService
    {
        void SetEventType(IEvent injectEvent);
        decimal GiftAidAmount(decimal donateAmount);
    }
}
