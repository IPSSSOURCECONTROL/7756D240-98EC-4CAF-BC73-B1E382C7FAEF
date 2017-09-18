using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Product
{
    public class Product: BusinessEntity
    {
        public Product(string description, PricingClassification pricingClassification1, string businessId) :base(businessId)
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