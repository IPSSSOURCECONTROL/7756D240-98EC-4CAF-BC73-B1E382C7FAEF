using System.Collections.Generic;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Security.Application.Models;

namespace KhanyisaIntel.Kbit.Framework.Security.Application.Services.Role
{
    public class RoleResponse : ServiceResponseBase
    {
        public RoleResponse()
        {
        }

        public RoleResponse(string message, ServiceResult serviceResult): base(message, serviceResult)
        {
        }

        public RoleAm Role { get; set; }
        public IList<RoleAm> Roles { get; set; } = new List<RoleAm>();

        
    }
}