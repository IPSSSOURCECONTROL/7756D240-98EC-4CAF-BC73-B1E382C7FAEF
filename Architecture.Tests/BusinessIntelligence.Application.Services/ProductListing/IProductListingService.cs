using Architecture.Tests.Infrustructure.Application;

namespace Architecture.Tests.BusinessIntelligence.Application.Services.ProductListing
{
    public interface IProductListingService : IApplicationService<ProductListingServiceRequest, ProductListingResponse>
    {
    }
}