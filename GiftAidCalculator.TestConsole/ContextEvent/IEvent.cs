namespace GiftAidCalculator.Service
{
    public interface IEvent
    {
        string Name { get; }
        decimal Supplement { get; }
    }
}
