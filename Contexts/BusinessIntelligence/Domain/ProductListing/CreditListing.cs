using System.Linq;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.ProductListing
{
    public class CreditListing : ProductListing
    {
        public CreditListing(Business.Business business) : base(business)
        {
        }

        public override string ProductListingUniqueIdentifier
        {
            get
            {
                return
                    $"CR_{this.Customer.Code}_{this.Customer.ProductListings.Count(x => x.GetType() == this.GetType())}";
            }
        }

        protected override string GetTypeName()
        {
            return this.GetType().Name;
        }
    }
}