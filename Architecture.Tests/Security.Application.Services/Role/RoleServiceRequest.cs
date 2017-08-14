using Architecture.Tests.Infrustructure.Application;
using Architecture.Tests.Security.Application.Models;

namespace Architecture.Tests.Security.Application.Services.Role
{
    public class RoleServiceRequest : ServiceRequestBase
    {
        public RoleAm Role { get; set; }
    }
}