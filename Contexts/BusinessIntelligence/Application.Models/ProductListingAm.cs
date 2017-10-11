using System;
using System.Collections.Generic;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application.Model;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models
{
    public class ProductListingAm: ApplicationModelBase
    {
        public string CustomerId { get; set; }
        public string ProductListingUniqueIdentifier { get; set; }
        public DateTime DateTime { get; set; }
        public IEnumerable<ProductListingItemAm> ProductListingItems { get; set; } =new List<ProductListingItemAm>();
    }
}
