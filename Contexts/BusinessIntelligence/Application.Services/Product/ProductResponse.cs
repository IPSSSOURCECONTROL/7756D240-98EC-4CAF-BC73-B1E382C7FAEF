using System.Collections.Generic;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Product
{
    public class ProductResponse : ServiceResponseBase
    {
        public ProductAm Product { get; set; }
        public IEnumerable<ProductAm> ProductCollection { get; set; }
    }
}