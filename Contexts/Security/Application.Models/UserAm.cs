using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application.Model;

namespace KhanyisaIntel.Kbit.Framework.Security.Application.Models
{
    public class UserAm: ApplicationModelBase
    {
        [KbitRequired]
        public string Name { get; set; }

        public string Code { get; set; }

        [KbitRequired]
        public string Email { get; set; }

        [KbitRequired]
        public string Password { get; set; }

        [KbitRequired]
        public string AccountStatus { get; set; }

        [KbitRequired]
        public string LicenseSpecification { get; set; }

        [KbitRequired]
        public string PasswordResetPolicy { get; set; }

        [KbitRequired]
        public string Role { get; set; }
    }
}
