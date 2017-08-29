using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Product
{
    public class ProductService : ApplicationServiceBase<
        ProductServiceRequest,
        ProductResponse, 
        IProductRepository,
        Domain.Product.Product,
        ProductAm>,
        IProductService
    {
    }
}