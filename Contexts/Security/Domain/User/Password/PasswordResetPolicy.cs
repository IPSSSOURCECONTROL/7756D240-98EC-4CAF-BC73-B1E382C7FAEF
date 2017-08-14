using System;

namespace KhanyisaIntel.Kbit.Framework.Security.Domain.User.Password
{
    public abstract class PasswordResetPolicy
    {

        public string Description { get; protected set; }
        public DateTime NextResetDate { get; protected set; }

        public bool IsDueForReset()
        {
            return DateTime.Now >= this.NextResetDate;
        }

        public int CalculateDaysLeftToNextReset()
        {
            return (this.NextResetDate - DateTime.Now).Days;
        }
    }
}