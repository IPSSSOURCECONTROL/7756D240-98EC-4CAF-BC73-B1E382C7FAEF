using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Security.Application.Models;

namespace KhanyisaIntel.Kbit.Framework.Security.Application.Services.Role
{
    public class RoleResponse : ServiceResponseBase<RoleAm>
    {
        public RoleResponse()
        {
        }

        public RoleResponse(string message, ServiceResult serviceResult)
            : base(message, serviceResult)
        {
        }
    }
}