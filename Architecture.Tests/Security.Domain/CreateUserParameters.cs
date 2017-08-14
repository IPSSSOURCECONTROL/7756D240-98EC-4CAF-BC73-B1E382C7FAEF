using Architecture.Tests.Security.Domain.User.AccountStatus;
using Architecture.Tests.Security.Domain.User.Password;

namespace Architecture.Tests.Security.Domain
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