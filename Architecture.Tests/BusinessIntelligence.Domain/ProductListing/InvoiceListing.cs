namespace Architecture.Tests.BusinessIntelligence.Domain.ProductListing
{
    public class InvoiceListing : ProductListing
    {
        public InvoiceListing(Business.Business business, Customer.Customer customer) : base(business, customer)
        {
        }

        protected override string GetTypeName()
        {
            return this.GetType().Name;
        }
    }
}