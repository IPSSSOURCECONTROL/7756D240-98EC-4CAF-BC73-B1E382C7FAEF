using Architecture.Tests.Infrustructure.Domain;

namespace Architecture.Tests.BusinessIntelligence.Domain.Product
{
    public class Product: AggregateRoot
    {
        public Product(string description, PricingClassification pricingClassification1)
        {
            this.Description = description;
            this.PricingClassification = pricingClassification1;
        }

        public string Description { get; private set; }
        public PricingClassification PricingClassification { get; private set; }

        protected override string GetTypeName()
        {
            return this.GetType().Name;
        }
    }
}