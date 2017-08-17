
using Framework.Application.Api.Web;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;

namespace KhanyisaIntel.Kbit.Framework.Application.Api.Web.Adapters
{
    public class AccountControllerAdapter
    {
        private readonly ApplicationUserManager _applicationUserManager;

        public AccountControllerAdapter(ApplicationUserManager applicationUserManager)
        {
            this._applicationUserManager = applicationUserManager;
        }

        [ValidateMethodArguments]
        public void RegisterUser(UserAccount userAccount)
        {
            //this._applicationUserManager.CreateAsync()
        }
    }

    public class UserAccount
    {
    }
}
