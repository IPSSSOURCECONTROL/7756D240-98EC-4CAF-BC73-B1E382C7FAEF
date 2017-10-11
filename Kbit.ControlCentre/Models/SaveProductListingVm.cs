using System.Collections.Generic;

namespace Kbit.ControlCentre.Models
{
    public class SaveProductListingVm : ViewModelBase
    {
        public IEnumerable<ProductLineItemVm> ProductDetailsArray { get; set; } = new List<ProductLineItemVm>();
        public string Id { get; set; }
        public string ProductListingUniqueIdentifier { get; set; }
    }
}