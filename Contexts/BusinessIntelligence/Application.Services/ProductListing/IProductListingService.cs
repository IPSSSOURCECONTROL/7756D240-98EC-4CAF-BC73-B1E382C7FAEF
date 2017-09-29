using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.ProductListing
{
    public interface IProductListingService : IApplicationService<ProductListingServiceRequest, ProductListingResponse>
    {
    }
}