using System.Collections.Generic;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Customer
{
    public class CustomerResponse : ServiceResponseBase<CustomerAm>
    {
        public CustomerResponse()
        {
        }

        public CustomerResponse(string message, ServiceResult serviceResult): base(message, serviceResult)
        {
        }

        public IEnumerable<ProductListingAm> ProductListings { get; set; } =new List<ProductListingAm>();
    }
}