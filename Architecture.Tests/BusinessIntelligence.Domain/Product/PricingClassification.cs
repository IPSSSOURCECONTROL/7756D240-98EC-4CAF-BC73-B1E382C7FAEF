namespace Architecture.Tests.BusinessIntelligence.Domain.Product
{
    public abstract class PricingClassification
    {
        protected PricingClassification()
        {
        }

        protected PricingClassification(decimal rate, Vat vat)
        {
            this.Rate = rate;
            this.Vat = vat ?? new NoVat();
        }

        public decimal Rate { get; set; }
        public Vat Vat { get; set; }
    }
}