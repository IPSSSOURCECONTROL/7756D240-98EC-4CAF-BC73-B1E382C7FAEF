using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Security.Application.Models;

namespace KhanyisaIntel.Kbit.Framework.Security.Application.Services.User
{
    public class UserResponse : ServiceResponseBase<UserAm>
    {
        public UserResponse()
        {
        }

        public UserResponse(string message, ServiceResult serviceResult)
            :base(message, serviceResult)
        {

        }
    }
}