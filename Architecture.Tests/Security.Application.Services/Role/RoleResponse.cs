using System.Collections.Generic;
using Architecture.Tests.Infrustructure.Application;
using Architecture.Tests.Security.Application.Models;

namespace Architecture.Tests.Security.Application.Services.Role
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