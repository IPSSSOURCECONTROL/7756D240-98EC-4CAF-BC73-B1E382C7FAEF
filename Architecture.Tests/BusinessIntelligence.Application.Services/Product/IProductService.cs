using Architecture.Tests.Infrustructure.Application;

namespace Architecture.Tests.BusinessIntelligence.Application.Services.Product
{
    public interface IProductService : IApplicationService<ProductServiceRequest, ProductResponse>
    {
    }
}