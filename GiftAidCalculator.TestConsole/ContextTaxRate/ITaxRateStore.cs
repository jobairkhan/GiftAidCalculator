namespace GiftAidCalculator.Context.TaxRate
{
    public interface ITaxRateStore
    {
        decimal Get();
        void Set(decimal taxRateArg);
    }
}
