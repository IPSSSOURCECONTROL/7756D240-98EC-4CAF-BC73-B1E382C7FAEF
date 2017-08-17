namespace KhanyisaIntel.Kbit.Framework.Security.Domain.User.AccountStatusTypes
{
    public class ActiveAccountStatus : AccountStatus
    {
        public override void CheckAccountStatus(User user)
        {
            if (user.Password.IsDueForReset())
            {
                user.Block("Password expired");
            }
        }
    }
}