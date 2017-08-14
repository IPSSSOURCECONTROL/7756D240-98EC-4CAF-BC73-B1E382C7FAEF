using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application.Model;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models
{
    public class CustomerAm: ApplicationModelBase
    {
        [KbitRequired]
        public string AddressLineOne { get; set; } = string.Empty;

        [KbitRequired]
        public string AddressLineTwo { get; set; } = string.Empty;

        [KbitRequired]
        public string Street { get; set; } = string.Empty;

        [KbitRequired]
        public string Suburb { get; set; } = string.Empty;

        [KbitRequired]
        public string TownOrCity { get; set; } = string.Empty;

        [KbitRequired]
        public string PostalCode { get; set; } = string.Empty;

        [KbitRequired]
        public string Email { get; set; } = string.Empty;

        public string TelephoneNumber { get; set; } = string.Empty;

        [KbitRequired]
        public string CellphoneNumber { get; set; } = string.Empty;

        [KbitRequired]
        public string Bank { get; set; } = string.Empty;

        [KbitRequired]
        public string AccountNumber { get; set; } = string.Empty;

        [KbitRequired]
        public string BranchCode { get; set; } = string.Empty;

        [KbitRequired]
        public string Reference { get; set; } = string.Empty;

        [KbitRequired]
        public string Name { get; set; } = string.Empty;

        [KbitRequired]
        public string RepresentativeId { get; set; } = string.Empty;

        [KbitRequired]
        public string RepresentativeName { get; set; } = string.Empty;

        [KbitRequired]
        public string RepresentativeCode { get; set; } = string.Empty;

        [KbitRequired]
        public string BusinessId { get; set; } = string.Empty;
    }

    public class BusinessAm : ApplicationModelBase
    {
        [KbitRequired]
        public string AddressLineOne { get; set; } = string.Empty;

        [KbitRequired]
        public string AddressLineTwo { get; set; } = string.Empty;

        [KbitRequired]
        public string Street { get; set; } = string.Empty;

        [KbitRequired]
        public string Suburb { get; set; } = string.Empty;

        [KbitRequired]
        public string TownOrCity { get; set; } = string.Empty;

        [KbitRequired]
        public string PostalCode { get; set; } = string.Empty;

        [KbitRequired]
        public string Email { get; set; } = string.Empty;

        public string TelephoneNumber { get; set; } = string.Empty;

        [KbitRequired]
        public string CellphoneNumber { get; set; } = string.Empty;

        [KbitRequired]
        public string Bank { get; set; } = string.Empty;

        [KbitRequired]
        public string AccountNumber { get; set; } = string.Empty;

        [KbitRequired]
        public string BranchCode { get; set; } = string.Empty;

        [KbitRequired]
        public string Reference { get; set; } = string.Empty;

        [KbitRequired]
        public string Name { get; set; } = string.Empty;
    }
}