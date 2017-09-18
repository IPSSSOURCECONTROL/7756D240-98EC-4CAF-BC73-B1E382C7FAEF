using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
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
        [AuthorizeAction]
        [ServiceRequestMethod]
        public override RoleResponse GetAll(RoleServiceRequest request)
        {
            this.Response.ApplicationModels = this.DomainFactory.BuildApplicationModelTypes(
                this.Repository.GetAll());

            this.Response.RegisterSuccess();

            return this.Response;
        }
    }
}