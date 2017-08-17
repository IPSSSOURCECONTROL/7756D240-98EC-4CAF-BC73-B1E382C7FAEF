namespace KhanyisaIntel.Kbit.Framework.Security.Domain.User.AccountStatusTypes
{
    public class InactiveAccountStatus : AccountStatus
    {
        public InactiveAccountStatus(string reason)
        {
            this.Description = reason;
        }

        public override void CheckAccountStatus(User user)
        {
            
        }
    }
}