using System;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities;
using KhanyisaIntel.Kbit.Framework.Security.Application.Models;
using KhanyisaIntel.Kbit.Framework.Security.Repository.Interfaces;

namespace KhanyisaIntel.Kbit.Framework.Security.Application.Services.User
{
    public class UserService : ApplicationServiceBase<UserResponse, 
        IUserRepository, 
        Domain.User.User, UserAm>,IUserService
    {
        [ServiceRequestMethod]
        public UserResponse GetById(UserServiceRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.EntityId))
            {
                this.Response.RegisterError(MessageFormatter.IsARequiredField(
                    nameof(UserServiceRequest.EntityId)));
                return this.Response;
            }


            this.Repository.GetById(request.EntityId);

            return new UserResponse();
        }

        [ServiceRequestMethod]
        public UserResponse GetAll(UserServiceRequest request)
        {
            throw new NotImplementedException();
        }

        [ServiceRequestMethod]
        public UserResponse Add(UserServiceRequest request)
        {
            if (request.User != null)
            {
                return null;
            }

            this.Response.RegisterError(MessageFormatter.IsARequiredField(
                nameof(UserServiceRequest.User)));

            //Domain.User.User user = 


            return this.Response;
        }

        [ServiceRequestMethod]
        public UserResponse Update(UserServiceRequest request)
        {
            throw new NotImplementedException();
        }

        [ServiceRequestMethod]
        public UserResponse Delete(UserServiceRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
