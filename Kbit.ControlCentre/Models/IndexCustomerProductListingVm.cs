using System;
using System.Collections.Generic;

namespace Kbit.ControlCentre.Models
{
    public class IndexCustomerProductListingVm : ViewModelBase
    {
        public string CustomerId { get; set; }
        public string CustomName { get; set; }
        public ProductListingVm ProductListing { get; set; }
        public IEnumerable<ProductListingVm> ProductListings { get; set; } = new List<ProductListingVm>();
        public IEnumerable<ProductLineItemVm> ProductListingItems { get; set; } = new List<ProductLineItemVm>();
        public string ProductListingNumber { get; set; }
        public DateTime DateTime { get; set; }
    }
}