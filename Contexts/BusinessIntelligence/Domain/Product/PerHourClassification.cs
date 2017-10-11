namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Product
{
    public class PerHourClassification : PricingClassification
    {
        public PerHourClassification(decimal rate, VatClassification vat) : base(rate, vat)
        {
        }
    }
}