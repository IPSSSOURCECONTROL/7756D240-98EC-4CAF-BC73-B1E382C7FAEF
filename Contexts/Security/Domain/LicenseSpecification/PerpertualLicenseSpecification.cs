using System;

namespace KhanyisaIntel.Kbit.Framework.Security.Domain.LicenseSpecification
{
    public class PerpertualLicenseSpecification : LicenseSpecification
    {
        public PerpertualLicenseSpecification()
        {
            this.Expires = DateTime.Today.AddYears(500).AddHours(23).AddMinutes(59).AddSeconds(59);
            this.ValidFrom = DateTime.Now;
            this.Key = $"{Guid.NewGuid()}{Guid.NewGuid()}{Guid.NewGuid()}";
        }

        public PerpertualLicenseSpecification(DateTime expires, DateTime validFrom, string key = null, string id = null)
            : base(id, expires, validFrom, key)
        {
        }
        public override void CheckLicenseState(User.User user)
        {
            if (DateTime.Now > this.Expires)
                user.SetLicense(new ExpiredLicenseSpecification(user.License.Id, user.License.Expires,
                    user.License.ValidFrom, user.License.Key));
        }

        protected override string GetTypeName()
        {
            return this.GetType().Name;
        }

    }
}