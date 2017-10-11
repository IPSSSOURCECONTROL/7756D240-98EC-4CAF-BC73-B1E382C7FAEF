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
    }
}