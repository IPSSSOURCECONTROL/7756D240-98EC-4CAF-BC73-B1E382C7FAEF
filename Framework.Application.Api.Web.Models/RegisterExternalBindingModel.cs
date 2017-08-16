using System.ComponentModel.DataAnnotations;

namespace KhanyisaIntel.Kbit.Framework.Application.Api.Web.Models
{
    public class RegisterExternalBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}