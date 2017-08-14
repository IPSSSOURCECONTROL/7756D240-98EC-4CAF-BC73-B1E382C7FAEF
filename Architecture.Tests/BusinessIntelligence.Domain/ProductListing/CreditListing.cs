namespace Architecture.Tests.BusinessIntelligence.Domain.ProductListing
{
    public class CreditListing : ProductListing
    {
        public CreditListing(Business.Business business, Customer.Customer customer) : base(business, customer)
        {
        }

        protected override string GetTypeName()
        {
            return this.GetType().Name;
        }
    }
}