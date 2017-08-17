namespace KhanyisaIntel.Kbit.Framework.Security.Domain.User.AccountStatusTypes
{
    public abstract class AccountStatus
    {
        public string Description { get; protected set; }
        public abstract void CheckAccountStatus(User user);
    }
}