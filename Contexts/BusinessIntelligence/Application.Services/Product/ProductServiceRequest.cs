using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Product
{
    public class ProductServiceRequest : ServiceRequestBase<ProductAm>
    {
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
    }
}