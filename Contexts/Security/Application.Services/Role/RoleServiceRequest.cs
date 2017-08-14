using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Security.Application.Models;

namespace KhanyisaIntel.Kbit.Framework.Security.Application.Services.Role
{
    public class RoleServiceRequest : ServiceRequestBase
    {
        public RoleAm Role { get; set; }
    }
}