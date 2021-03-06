using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Product
{
    public interface IProductService : IApplicationService<ProductServiceRequest, ProductResponse>
    {
        [AuthorizeAction]
        [ServiceRequestMethod]
        ProductResponse CalculateProductTotals(ProductServiceRequest request);
    }
}