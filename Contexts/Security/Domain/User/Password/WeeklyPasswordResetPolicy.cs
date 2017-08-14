using System;

namespace KhanyisaIntel.Kbit.Framework.Security.Domain.User.Password
{
    public class WeeklyPasswordResetPolicy : PasswordResetPolicy
    {
        public WeeklyPasswordResetPolicy()
        {
            this.Description = "Must reset password weekly, after 5 working days.";
            this.NextResetDate = DateTime.Today.AddDays(5).AddHours(23).AddMinutes(59).AddSeconds(59);
        }
    }
}