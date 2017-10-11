using System.Linq;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.ProductListing
{
    public class CreditListing : ProductListing
    {
        public CreditListing(Business.Business business) : base(business)
        {
        }

        protected override string GetTypeName()
        {
            return this.GetType().Name;
        }
    }
}