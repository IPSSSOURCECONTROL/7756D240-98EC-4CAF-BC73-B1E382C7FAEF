using System.Web.Http;
using KhanyisaIntel.Kbit.Framework.Security.Application.Models;
using KhanyisaIntel.Kbit.Framework.Security.Application.Services.User;

namespace KhanyisaIntel.Kbit.Framework.Mvc.Controllers.User
{
    [Authorize]
    public class UserController: 
        KbitApiControllerBase<UserAm,IUserService, UserServiceRequest, UserResponse>
    {
        public UserController(IUserService applicationService) : base(applicationService)
        {
        }
    }
}
