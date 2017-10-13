using KhanyisaIntel.Kbit.Framework.Infrustructure.Application.Model;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models
{
    public class ProductListingItemAm: ApplicationModelBase
    {
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public string ProductId { get; set; }
        public ProductAm Product { get; set; }
        public decimal Subtotal { get; set; }
    }
}