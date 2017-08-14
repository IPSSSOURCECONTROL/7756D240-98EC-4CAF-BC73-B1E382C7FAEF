namespace Architecture.Tests.Security.Domain.User.AccountStatus
{
    public class BlockedAccountStatus : AccountStatus
    {
        public BlockedAccountStatus()
        {
        }

        public BlockedAccountStatus(string reason)
        {
            this.Description = reason;
        }

        public override void CheckAccountStatus(User user)
        {
        }
    }
}