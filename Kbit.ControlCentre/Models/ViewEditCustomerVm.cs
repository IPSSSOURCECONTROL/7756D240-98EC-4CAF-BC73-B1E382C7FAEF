﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper.Attributes;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;

namespace Kbit.ControlCentre.Models
{
    [MapsFrom(typeof(CustomerAm), ReverseMap = true)]
    public class ViewEditCustomerVm: ViewModelBase
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Address Line One")]
        [StringLength(255)]
        public string AddressLineOne { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Address Line Two")]
        [StringLength(255)]
        public string AddressLineTwo { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string Street { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string Suburb { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string TownOrCity { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Postal Code")]
        [StringLength(255)]
        public string PostalCode { get; set; } = string.Empty;

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Telephone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string TelephoneNumber { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Cellphone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Cell Phone number")]
        public string CellphoneNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string Bank { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Account Number")]
        [StringLength(255)]
        public string AccountNumber { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Branch Code")]
        [StringLength(255)]
        public string BranchCode { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string Reference { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(6)]
        public string Code { get; set; }

        public int CostEstimateCount { get; set; }

        public int InvoiceNoteCount { get; set; }

        public int CreditNoteCount { get; set; }

        [Display(Name = "Account Manager")]
        public string RepresentativeId { get; set; } = string.Empty;

        public IEnumerable<ViewEditUserVm> Representatives { get; set; }=new List<ViewEditUserVm>();
    }
}