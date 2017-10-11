using System;
using System.Collections.Generic;
using AutoMapper.Attributes;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;

namespace Kbit.ControlCentre.Models
{
    [MapsFrom(typeof(ProductListingAm), ReverseMap = true)]
    public class ProductListingVm: ViewModelBase
    {
        public string CustomerId { get; set; }
        public string ProductListingUniqueIdentifier { get; set; }
        public DateTime DateTime { get; set; }
        public IEnumerable<ProductLineItemVm> ProductListingItems { get; set; } = new List<ProductLineItemVm>();
    }
}