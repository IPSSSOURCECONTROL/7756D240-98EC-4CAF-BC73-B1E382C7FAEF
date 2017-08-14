namespace Architecture.Tests.Security.Domain.User.AccountStatus
{
    public abstract class AccountStatus
    {
        public string Description { get; protected set; }
        public abstract void CheckAccountStatus(User user);
    }
}