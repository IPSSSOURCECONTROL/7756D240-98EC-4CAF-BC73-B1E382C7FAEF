namespace Architecture.Tests.Security.Domain.User.AccountStatus
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