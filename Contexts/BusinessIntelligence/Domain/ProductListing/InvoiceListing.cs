using System.Linq;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.ProductListing
{
    public class InvoiceListing : ProductListing
    {
        public InvoiceListing(Business.Business business) : base(business)
        {
        }

        protected override string GetTypeName()
        {
            return this.GetType().Name;
        }
    }
}