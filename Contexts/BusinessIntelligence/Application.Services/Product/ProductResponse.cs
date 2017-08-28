using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Product
{
    public class ProductResponse : ServiceResponseBase<ProductAm>
    {
        public ProductResponse()
        {
        }

        public ProductResponse(string message, ServiceResult serviceResult)
            : base(message, serviceResult)
        {
        }
    }
}