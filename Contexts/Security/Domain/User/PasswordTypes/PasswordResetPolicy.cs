using System;

namespace KhanyisaIntel.Kbit.Framework.Security.Domain.User.PasswordTypes
{
    public abstract class PasswordResetPolicy
    {

        public string Description { get; set; }
        public DateTime NextResetDate { get; set; }

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