namespace GiftAidCalculator.Context.TaxRate
{
    public struct TaxRate 
    {
        public decimal Value { get; private set; }

        public TaxRate(decimal rate)
        {
            this.Value = rate;
        }

        public override string ToString()
        {
            return "Tax Rate: " + this.Value.ToString();
        }
    }
}
