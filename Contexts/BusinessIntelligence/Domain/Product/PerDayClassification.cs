namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Product
{
    public class PerDayClassification : PricingClassification
    {
        public PerDayClassification(decimal rate, VatClassification vat) : base(rate, vat)
        {
        }
    }
}