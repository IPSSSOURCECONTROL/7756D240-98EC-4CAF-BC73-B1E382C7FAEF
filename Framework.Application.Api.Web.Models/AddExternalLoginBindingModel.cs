using System.ComponentModel.DataAnnotations;

namespace KhanyisaIntel.Kbit.Framework.Application.Api.Web.Models
{
    // Models used as parameters to AccountController actions.

    public class AddExternalLoginBindingModel
    {
        [Required]
        [Display(Name = "External access token")]
        public string ExternalAccessToken { get; set; }
    }
}
