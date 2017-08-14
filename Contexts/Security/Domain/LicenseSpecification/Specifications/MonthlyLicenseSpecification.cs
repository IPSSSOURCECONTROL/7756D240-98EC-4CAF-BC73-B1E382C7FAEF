using System;

namespace KhanyisaIntel.Kbit.Framework.Security.Domain.LicenseSpecification.Specifications
{
    public class MonthlyLicenseSpecification : LicenseSpecification
    {
        public MonthlyLicenseSpecification(string id, DateTime expires, DateTime validFrom, string key)
            : base(id, expires, validFrom, key)
        {
        }

        public MonthlyLicenseSpecification()
        {
            this.Expires = DateTime.Today.AddDays(30).AddHours(23).AddMinutes(59).AddSeconds(59);
            this.ValidFrom = DateTime.Now;
            this.Key = $"{Guid.NewGuid()}{Guid.NewGuid()}{Guid.NewGuid()}";
        }

        public override void CheckLicenseState(User.User user)
        {
            if (DateTime.Now > this.Expires)
                user.SetLicense(new ExpiredLicenseSpecification(user.License.Id,user.License.Expires,
                    user.License.ValidFrom, user.License.Key));
        }

        protected override string GetTypeName()
        {
            return this.GetType().Name;
        }


    }
}