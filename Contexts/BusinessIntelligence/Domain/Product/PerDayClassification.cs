namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Product
{
    public class PerDayClassification : PricingClassification
    {
        public PerDayClassification(decimal rate, Vat vat) : base(rate, vat)
        {
        }
    }
}