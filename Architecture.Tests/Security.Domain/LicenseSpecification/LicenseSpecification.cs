using System;
using Architecture.Tests.Infrustructure.Domain;

namespace Architecture.Tests.Security.Domain.LicenseSpecification
{
    public abstract class LicenseSpecification: AggregateRoot
    {
        private string _key;

        protected LicenseSpecification(string id, DateTime expires, DateTime validFrom, string key)
        {
            this.Id = id;
            this.Expires = expires;
            this.ValidFrom = validFrom;
            this.Key = key;
        }

        protected LicenseSpecification()
        {
        }

        public DateTime Expires { get; protected set; }
        public DateTime ValidFrom { get; protected set; }

        public string Key
        {
            get
            {
                if(string.IsNullOrWhiteSpace(this._key))
                    this._key = $"{Guid.NewGuid()}{Guid.NewGuid()}{Guid.NewGuid()}";
                return this._key;
            }
            protected set { this._key = value; }
        }

        public abstract void CheckLicenseState(User.User user);
    }
}