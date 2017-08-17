using System;

namespace KhanyisaIntel.Kbit.Framework.Security.Domain.User.PasswordTypes
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