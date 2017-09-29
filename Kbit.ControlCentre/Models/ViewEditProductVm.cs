using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AutoMapper.Attributes;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;

namespace Kbit.ControlCentre.Models
{
    [MapsFrom(typeof(ProductAm), ReverseMap = true)]
    public class ViewEditProductVm: ViewModelBase
    {

        public string Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Product Code")]
        [StringLength(maximumLength: 4)]
        public string ProductCode { get; set; }

        [Required]
        [Display(Name = "Pricing Classification")]
        public string PricingClassification { get; set; }

        public IEnumerable<string> PricingClassifications { get; set; } = new List<string>();

        public decimal Rate { get; set; }

        [Required]
        public string VatClassification { get; set; }

        [Required]
        public decimal VatRate { get; set; }

        public IEnumerable<string> VatTypes { get; set; } = new List<string>();
    }
}