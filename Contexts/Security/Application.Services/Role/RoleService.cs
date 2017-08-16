using System;
using System.Collections.Generic;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Security.Application.Models;
using KhanyisaIntel.Kbit.Framework.Security.Repository.Interfaces;

namespace KhanyisaIntel.Kbit.Framework.Security.Application.Services.Role
{
    public class RoleService : ApplicationServiceBase<RoleResponse, 
        IRoleRepository, Domain.Role.Role,RoleAm>, IRoleService
    {
        [ServiceRequestMethod]
        public RoleResponse GetById(RoleServiceRequest request)
        {
            RoleResponse response = new RoleResponse();

            Domain.Role.Role role = this.Repository.GetById(request.EntityId);

            if (role == null)
            {
                response.ServiceResult  = ServiceResult.Error;
                response.Message = $"Role with Id '{request.EntityId}' not found.";
                return response;
            }

            response.Role = new RoleAm();
            response.Role.Id = role.Id;
            response.Role.Name = role.GetDescription();

            response.ServiceResult = ServiceResult.Success;

            return response;
        }

        [ServiceRequestMethod]
        public RoleResponse GetAll(RoleServiceRequest request)
        {
            RoleResponse response = new RoleResponse();

            IEnumerable<Domain.Role.Role> roles = this.Repository.GetAll();

            foreach (Domain.Role.Role role in roles)
            {
                RoleAm roleAm = new RoleAm();
                roleAm.Name = role.GetDescription();
                roleAm.Id = role.Id;

                response.Roles.Add(roleAm);
            }
            
            response.ServiceResult = ServiceResult.Success;

            return response;
        }

        [ServiceRequestMethod]
        public RoleResponse Add(RoleServiceRequest request)
        {
            throw new NotSupportedException("Function not supported.");
        }

        [ServiceRequestMethod]
        public RoleResponse Update(RoleServiceRequest request)
        {
            RoleResponse response = new RoleResponse();

            if (request.Role == null)
            {
                response.RegisterError("Null role recieved.");
                return response;
            }

            

            IEnumerable<Domain.Role.Role> roles = this.Repository.GetAll();

            foreach (Domain.Role.Role role in roles)
            {
                RoleAm roleAm = new RoleAm();
                roleAm.Name = role.GetDescription();
                roleAm.Id = role.Id;

                response.Roles.Add(roleAm);
            }

            response.ServiceResult = ServiceResult.Success;

            return response;
        }

        [ServiceRequestMethod]
        public RoleResponse Delete(RoleServiceRequest request)
        {
            throw new NotImplementedException();
        }
    }
}