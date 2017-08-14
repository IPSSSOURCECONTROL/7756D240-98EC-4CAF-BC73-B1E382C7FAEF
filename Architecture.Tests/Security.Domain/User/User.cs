using System;
using Architecture.Tests.Infrustructure.Domain;
using Architecture.Tests.Security.Domain.Exceptions;
using Architecture.Tests.Security.Domain.LicenseSpecification.Specifications;
using Architecture.Tests.Security.Domain.User.AccountStatus;

namespace Architecture.Tests.Security.Domain.User
{
    public class User: AggregateRoot
    {
        private LicenseSpecification.LicenseSpecification _license;

        public User(string id, string name, string code, string email, 
            Password.Password password, Role.Role role, AccountStatus.AccountStatus accountStatus)
        {
            this.Validate(name, code, email);

            this.Id = id;
            this.Name = name;
            this.Code = code;
            this.Email = email;
            this.Password = password;
            this.Role = role;
            this.AccountStatus = accountStatus;
        }

        public string Name { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }
        public Password.Password Password { get; private set; }
        public Role.Role Role { get; private set; }
        public AccountStatus.AccountStatus AccountStatus { get; private set; }

        public LicenseSpecification.LicenseSpecification License
        {
            get
            {
                if(this._license == null)
                    throw new InvalidOperationException("User license not set.");
                return this._license;
            }
            private set { this._license = value; }
        }

        public void Activate()
        {
            this.AccountStatus = new ActiveAccountStatus();
        }

        public void Deactivate(string reason)
        {
            this.AccountStatus = new InactiveAccountStatus(reason);
        }

        public void Block(string reason)
        {
            this.AccountStatus = new BlockedAccountStatus(reason);
        }

        public void CheckLicense()
        {
            this.License.CheckLicenseState(this);
        }

        public bool IsLicenseValid()
        {
            if (this.License is ExpiredLicenseSpecification)
                return false;

            return true;
        }

        public void SetLicense(LicenseSpecification.LicenseSpecification license)
        {
            if (license == null)
                throw new CannotAddNullLicenseException(nameof(license));

            if(this._license != null)
                throw new InvalidOperationException("License already set for user.");

            this.License = license;
        }

        public void SetPassword(string password)
        {
            this.Password.SetPassword(password);
        }

        public string GetPassword()
        {
            if (this.Password == null)
                throw new PasswordNoteSetException();

            return this.Password.Value;
        }

        public void GrantRole(Role.Role role)
        {
            if(role == null)
                throw new CannotAddNullRoleException(nameof(role));

            this.Role = role;
        }

        public bool IsAccountActive()
        {
            if (this.AccountStatus is BlockedAccountStatus || this.AccountStatus is InactiveAccountStatus)
                return false;

            return true;
        }

        public void CheckAccountStatus()
        {
            this.AccountStatus.CheckAccountStatus(this);
        }

        protected override string GetTypeName()
        {
            return this.GetType().Name;
        }

        private void Validate(string name, string code, string email)
        {
            if(string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(nameof(name));

            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException(nameof(code));

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException(nameof(email));
        }
    }
}
