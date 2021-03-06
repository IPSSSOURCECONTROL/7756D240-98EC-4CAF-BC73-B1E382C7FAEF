using System;

namespace Architecture.Tests.Security.Domain.User.Password
{
    public class DailyPasswordResetPolicy : PasswordResetPolicy
    {
        public DailyPasswordResetPolicy()
        {
            this.Description = "Must reset password daily.";
            this.NextResetDate = DateTime.Today.AddDays(1).AddHours(23).AddMinutes(59).AddSeconds(59);
        }
    }
}