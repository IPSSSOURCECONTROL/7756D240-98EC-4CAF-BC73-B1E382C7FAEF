using System;

namespace KhanyisaIntel.Kbit.Framework.Security.Domain.LicenseSpecification.Specifications
{
    public class AnnualLicenseSpecification : LicenseSpecification
    {
        /// <summary>
        /// Instantiates an <see cref="AnnualLicenseSpecification"/>
        /// </summary>
        /// <param name="id">UID</param>
        /// <param name="expires">The date the license is set to expire.</param>
        /// <param name="validFrom">Todays date</param>
        /// <param name="key">Unique license key.</param>
        public AnnualLicenseSpecification(string id, DateTime expires, DateTime validFrom, string key)
            : base(id,expires, validFrom, key)
        {
        }

        /// <summary>
        /// Creates an <see cref="AnnualLicenseSpecification"/> that expires after a year from today.
        /// </summary>
        public AnnualLicenseSpecification()
        {
            this.Expires = DateTime.Today.AddYears(1).AddHours(23).AddMinutes(59).AddSeconds(59);
            this.ValidFrom = DateTime.Now;
            this.Key = $"{Guid.NewGuid()}{Guid.NewGuid()}{Guid.NewGuid()}";
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