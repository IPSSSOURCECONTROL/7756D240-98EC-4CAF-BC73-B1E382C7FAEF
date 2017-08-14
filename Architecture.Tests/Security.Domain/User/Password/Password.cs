using Architecture.Tests.Security.Domain.Exceptions;

namespace Architecture.Tests.Security.Domain.User.Password
{
    public class Password
    {
        public Password(string value, PasswordResetPolicy passwordResetPolicy)
        {
            this.Value = value;
            this.PasswordResetPolicy = passwordResetPolicy;
        }

        public string Value { get; private set; }
        public PasswordResetPolicy PasswordResetPolicy { get; set; }
        public bool IsDueForReset()
        {
            return this.PasswordResetPolicy.IsDueForReset();
        }

        public void SetPassword(string password)
        {
            if(string.IsNullOrWhiteSpace(password))
                throw new CannotAddInvalidPasswordException("null or empty");

            this.Value = password;
        }

        public void SetPasswordResetPolicy(PasswordResetPolicy passwordResetPolicy)
        {
            if(passwordResetPolicy == null)
                throw new CannotSetNullPasswordResetPolicyException(nameof(passwordResetPolicy));

            this.PasswordResetPolicy = passwordResetPolicy;
        }
    }
}
