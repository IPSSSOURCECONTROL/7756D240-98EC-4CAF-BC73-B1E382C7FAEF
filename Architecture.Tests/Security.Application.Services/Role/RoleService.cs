using System;
using System.Collections.Generic;
using Architecture.Tests.Infrustructure.AOP.Attributes;
using Architecture.Tests.Infrustructure.Application;
using Architecture.Tests.Security.Application.Models;
using Architecture.Tests.Security.Domain.Role;

namespace Architecture.Tests.Security.Application.Services.Role
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            this._roleRepository = roleRepository;
        }


        [ServiceRequestMethod]
        public RoleResponse GetById(RoleServiceRequest request)
        {
            RoleResponse response = new RoleResponse();

            Domain.Role.Role role = this._roleRepository.GetById(request.EntityId);

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

            IEnumerable<Domain.Role.Role> roles = this._roleRepository.GetAll();

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

            

            IEnumerable<Domain.Role.Role> roles = this._roleRepository.GetAll();

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