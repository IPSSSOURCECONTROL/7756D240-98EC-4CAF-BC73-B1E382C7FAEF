using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Security.Application.Models;
using KhanyisaIntel.Kbit.Framework.Security.Repository.Interfaces;

namespace KhanyisaIntel.Kbit.Framework.Security.Application.Services.Role
{
    public class RoleService : ApplicationServiceBase<
        RoleServiceRequest,
        RoleResponse, 
        IRoleRepository, 
        Domain.Role.Role,RoleAm>, 
        IRoleService
    {

    }
}