using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper.Attributes;
using KhanyisaIntel.Kbit.Framework.Security.Application.Models;

namespace Kbit.ControlCentre.Models
{
    [MapsFrom(typeof(UserAm), ReverseMap = true)]
    public class ViewEditUserVm: ViewModelBase
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Code { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmationPassword { get; set; }

        [Display(Name = "Account Status")]
        public string AccountStatus { get; set; }

        [Display(Name = "License Specification")]
        public string LicenseSpecification { get; set; }

        [Display(Name = "Password Reset Policy")]
        public string PasswordResetPolicy { get; set; }

        public string Role { get; set; }

        public string BusinessId { get; set; }
        public List<string> Roles { get; set; }
        public List<string> LicenseSpecifications { get; set; }
        public List<string> PasswordResetPolicies { get; set; }
        public List<string> AccountStatuss { get; set; }
    }
}