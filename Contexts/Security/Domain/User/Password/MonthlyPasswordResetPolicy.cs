using System;

namespace KhanyisaIntel.Kbit.Framework.Security.Domain.User.Password
{
    public class MonthlyPasswordResetPolicy : PasswordResetPolicy
    {
        public MonthlyPasswordResetPolicy()
        {
            this.Description = "Must reset password weekly, after 20 working days.";
            this.NextResetDate = DateTime.Today.AddDays(20).AddHours(23).AddMinutes(59).AddSeconds(59);
        }
    }
}