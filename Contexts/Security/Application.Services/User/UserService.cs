using System;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Security.Repository.Interfaces;

namespace KhanyisaIntel.Kbit.Framework.Security.Application.Services.User
{
    public class UserService : ApplicationServiceBase<UserResponse, IUserRepository>,IUserService
    {
        //private readonly IUserService _repository;

        public UserResponse GetById(UserServiceRequest request)
        {
            this.Repository.GetById(request.EntityId);

            return new UserResponse();
        }

        public UserResponse GetAll(UserServiceRequest request)
        {
            throw new NotImplementedException();
        }

        public UserResponse Add(UserServiceRequest request)
        {
            throw new NotImplementedException();
        }

        public UserResponse Update(UserServiceRequest request)
        {
            throw new NotImplementedException();
        }

        public UserResponse Delete(UserServiceRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
