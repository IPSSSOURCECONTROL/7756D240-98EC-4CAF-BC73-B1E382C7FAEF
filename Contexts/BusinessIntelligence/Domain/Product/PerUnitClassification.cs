namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Product
{
    public class PerUnitClassification : PricingClassification
    {
        public PerUnitClassification(decimal rate, VatClassification vat) : base(rate, vat)
        {
        }
    }
}