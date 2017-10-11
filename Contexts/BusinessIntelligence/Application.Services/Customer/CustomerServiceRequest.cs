using System.Collections.Generic;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Customer
{
    public class CustomerServiceRequest : ServiceRequestBase<CustomerAm>
    {
        public IEnumerable<ProductListingItemAm> ProductListingItems { get; set; }=new List<ProductListingItemAm>();
        public string ProductListingUniqueIdentifier { get; set; }
    }
}