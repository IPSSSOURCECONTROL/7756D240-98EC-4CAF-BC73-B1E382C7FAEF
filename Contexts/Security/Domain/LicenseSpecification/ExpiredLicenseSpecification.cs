using System;

namespace KhanyisaIntel.Kbit.Framework.Security.Domain.LicenseSpecification
{
    public class ExpiredLicenseSpecification : LicenseSpecification
    {
        public ExpiredLicenseSpecification(string id, DateTime expires, DateTime validFrom, string key)
            : base(id, expires, validFrom, key)
        {
        }
        public override void CheckLicenseState(User.User user)
        {
        }

        protected override string GetTypeName()
        {
            return this.GetType().Name;
        }
    }
}