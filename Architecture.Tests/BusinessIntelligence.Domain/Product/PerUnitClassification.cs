namespace Architecture.Tests.BusinessIntelligence.Domain.Product
{
    public class PerUnitClassification : PricingClassification
    {
        public PerUnitClassification(decimal rate, Vat vat) : base(rate, vat)
        {
        }
    }
}