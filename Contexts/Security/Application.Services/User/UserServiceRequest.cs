using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Security.Application.Models;

namespace KhanyisaIntel.Kbit.Framework.Security.Application.Services.User
{
    public class UserServiceRequest : ServiceRequestBase<UserAm>
    {
        public string BusinessId { get; set; }
        public bool UserPasswordChanged { get; set; }
    }
}