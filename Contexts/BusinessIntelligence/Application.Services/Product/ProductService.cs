using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities;

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
        public ProductResponse CalculateProductTotal(ProductServiceRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.EntityId))
            {
                this.Response.RegisterError(MessageFormatter.IsARequiredField(request.EntityId));
                return this.Response;
            }

            Domain.Product.Product domainEntityType = this.Repository.GetById(request.EntityId);

            if (domainEntityType == null)
            {
                this.Response.RegisterError(MessageFormatter.RecordWithIdDoesNotExist(request.EntityId));
            }
            else
            {
                this.Response.TotalAmount = domainEntityType.CalculateTotalAmount(request.Quantity, request.Discount);
                //this.Response.
                this.Response.RegisterSuccess();
            }

            return this.Response;
        }
    }
}