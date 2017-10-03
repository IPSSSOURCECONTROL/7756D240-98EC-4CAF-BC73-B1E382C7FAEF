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

        public decimal TotalAmount { get; set; } = 0.00m;
        public decimal TotalDiscount { get; set; } = 0.00m;
        public decimal TotalVat { get; set; } = 0.00m;
    }
}