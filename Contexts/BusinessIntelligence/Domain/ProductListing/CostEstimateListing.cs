using System.Linq;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.ProductListing
{
    public class CostEstimateListing : ProductListing
    {
        public CostEstimateListing(Business.Business business) : base(business)
        {
        }

        protected override string GetTypeName()
        {
            return this.GetType().Name;
        }

        public override string ProductListingUniqueIdentifier {
            get
            {
                return
                    $"CE_{this.Customer.Code}_{this.Customer.ProductListings.Count(x => x.GetType() == this.GetType())}";
            }
        }
    }
}