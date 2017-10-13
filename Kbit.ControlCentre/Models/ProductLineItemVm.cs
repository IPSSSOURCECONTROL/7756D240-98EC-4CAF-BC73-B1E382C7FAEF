using AutoMapper.Attributes;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;

namespace Kbit.ControlCentre.Models
{
    [MapsFrom(typeof(ProductListingItemAm), ReverseMap = true)]
    public class ProductLineItemVm: ViewModelBase
    {
        public string ProductId { get; set; }
        public ViewEditProductVm Product { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
    }
}