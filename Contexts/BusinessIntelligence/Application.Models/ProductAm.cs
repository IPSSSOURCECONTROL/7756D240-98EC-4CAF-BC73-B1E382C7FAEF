using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
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
        public string Vat { get; set; }
    }
}