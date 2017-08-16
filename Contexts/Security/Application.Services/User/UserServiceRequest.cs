using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Security.Application.Models;

namespace KhanyisaIntel.Kbit.Framework.Security.Application.Services.User
{
    public class UserServiceRequest : ServiceRequestBase
    {
        public UserAm User { get; set; }=new UserAm();
    }
}