namespace KhanyisaIntel.Kbit.Framework.Security.Domain.User.AccountStatusTypes
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