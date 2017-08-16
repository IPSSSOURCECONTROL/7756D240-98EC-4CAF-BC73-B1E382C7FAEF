using System;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities;
using KhanyisaIntel.Kbit.Framework.Security.Repository.Interfaces;

namespace KhanyisaIntel.Kbit.Framework.Security.Application.Services.User
{
    public class UserService : ApplicationServiceBase<UserResponse, IUserRepository>,IUserService
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
            //if (request.User == null)
            //{
            //    this.Response.RegisterError(MessageFormatter.IsARequiredField(
            //        nameof(UserServiceRequest.User)));
            //    return this.Response;
            //}

            return null;
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
