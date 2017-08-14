namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Product
{
    public class PerHourClassification : PricingClassification
    {
        public PerHourClassification(decimal rate, Vat vat) : base(rate, vat)
        {
        }
    }
}