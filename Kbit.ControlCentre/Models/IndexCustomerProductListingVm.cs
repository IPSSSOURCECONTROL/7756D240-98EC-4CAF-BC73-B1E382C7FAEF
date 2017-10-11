using System.Collections.Generic;

namespace Kbit.ControlCentre.Models
{
    public class IndexCustomerProductListingVm : ViewModelBase
    {
        public string CustomerId { get; set; }
        public string CustomName { get; set; }
        public IEnumerable<ProductListingVm> ProductListings { get; set; }=new List<ProductListingVm>();
    }
}