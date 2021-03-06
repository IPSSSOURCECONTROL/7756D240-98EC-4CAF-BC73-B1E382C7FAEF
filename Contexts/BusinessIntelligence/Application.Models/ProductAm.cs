﻿using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application.Model;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models
{
    public class ProductAm : ApplicationModelBase
    {
        [KbitRequired]
        public string Description { get; set; }

        [KbitRequired]
        public string PricingClassification { get; set; }

        public decimal Rate { get; set; }

        [KbitRequired]
        public string VatClassification { get; set; }
        [KbitRequired]
        public decimal VatRate { get; set; }

        [KbitRequired]
        public string ProductCode { get; set; }
    }
}