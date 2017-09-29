namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Product
{
    public abstract class PricingClassification
    {
        protected PricingClassification()
        {
        }

        protected PricingClassification(decimal rate, VatClassification vat)
        {
            this.Rate = rate;
            this.Vat = vat ?? new NoVat();
        }

        public decimal Rate { get; set; }
        public VatClassification Vat { get; set; }
    }
}