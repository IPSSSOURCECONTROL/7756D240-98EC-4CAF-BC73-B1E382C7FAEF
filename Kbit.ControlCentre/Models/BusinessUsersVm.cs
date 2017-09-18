using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using AutoMapper.Attributes;
using KhanyisaIntel.Kbit.Framework.Security.Application.Models;

namespace Kbit.ControlCentre.Models
{
    public class BusinessUsersVm: ViewModelBase
    {
        public string BusinessId { get; set; }
        public IEnumerable<ViewEditBusinessUserVm> ViewModels { get; set; } = new List<ViewEditBusinessUserVm>();
    }

    [MapsFrom(typeof(UserAm), ReverseMap = true)]
    public class ViewEditBusinessUserVm: ViewModelBase
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Code { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Account Status")]
        [StringLength(255)]
        public string AccountStatus { get; set; }

        [Required]
        [Display(Name = "License Specification")]
        [StringLength(255)]
        public string LicenseSpecification { get; set; }

        [Required]
        [Display(Name = "Password Reset Policy")]
        [StringLength(255)]
        public string PasswordResetPolicy { get; set; }

        [Required]
        public string RoleId { get; set; }

        public SelectList Roles { get; set; }
    }
}