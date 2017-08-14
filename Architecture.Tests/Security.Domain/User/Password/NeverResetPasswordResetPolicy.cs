using System;

namespace Architecture.Tests.Security.Domain.User.Password
{
    public class NeverResetPasswordResetPolicy : PasswordResetPolicy
    {
        public NeverResetPasswordResetPolicy()
        {
            this.Description = "Never expires";
            this.NextResetDate = DateTime.Today.AddYears(200).AddHours(23).AddMinutes(59).AddSeconds(59);
        }
    }
}