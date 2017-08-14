using KhanyisaIntel.Kbit.Framework.Security.Domain.User.AccountStatus;
using KhanyisaIntel.Kbit.Framework.Security.Domain.User.Password;

namespace KhanyisaIntel.Kbit.Framework.Security.Domain
{
    public class CreateUserParameters
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public PasswordResetPolicy PasswordResetPolicy { get; set; }
        public Role.Role Role { get; set; }
        public AccountStatus AccountStatus { get;  set; }
    }
}