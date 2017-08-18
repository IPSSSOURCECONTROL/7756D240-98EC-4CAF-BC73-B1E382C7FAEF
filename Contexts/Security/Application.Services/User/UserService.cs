using System;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Encryption;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities;
using KhanyisaIntel.Kbit.Framework.Security.Application.Models;
using KhanyisaIntel.Kbit.Framework.Security.Domain.AspNet;
using KhanyisaIntel.Kbit.Framework.Security.Repository.Interfaces;

namespace KhanyisaIntel.Kbit.Framework.Security.Application.Services.User
{
    public class UserService : ApplicationServiceBase<UserResponse, 
        IUserRepository, 
        Domain.User.User, UserAm>,IUserService
    {
        private readonly IAspNetCryptology _aspNetCryptology;

        public UserService(IAspNetCryptology aspNetCryptology)
        {
            this._aspNetCryptology = aspNetCryptology;
        }

        [ServiceRequestMethod]
        public UserResponse GetById(UserServiceRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.EntityId))
            {
                this.Response.RegisterError(MessageFormatter.IsARequiredField(
                    nameof(UserServiceRequest.EntityId)));
                return this.Response;
            }

            this.Response.User = this.DomainFactory.BuildApplicationModelType(this.Repository.GetById(request.EntityId));

            return new UserResponse();
        }

        [ServiceRequestMethod]
        public UserResponse GetAll(UserServiceRequest request)
        {
            this.Response.Users = this.DomainFactory.BuildApplicationModelTypes(this.Repository.GetAll());

            this.Response.RegisterSuccess();

            return this.Response;
        }

        [ServiceRequestMethod]
        public UserResponse Add(UserServiceRequest request)
        {
            if (request.User == null)
            {
                this.Response.RegisterError(MessageFormatter.IsARequiredField(
                    nameof(UserServiceRequest.User)));
                return this.Response;
            }

            Domain.User.User user = this.DomainFactory.BuildDomainEntityType(request.User);
            user.SetPassword(this._aspNetCryptology.HashPassword(request.User.Password));

            this.Repository.Add(user);

            this.Response.RegisterSuccess(MessageFormatter.EntitySuccessfullyAdded<Domain.User.User>(user.Id));

            return this.Response;
        }

        [ServiceRequestMethod]
        public UserResponse Update(UserServiceRequest request)
        {
            if (request.User == null)
            {
                this.Response.RegisterError(MessageFormatter.IsARequiredField(
                    nameof(UserServiceRequest.User)));
                return this.Response;
            }

            Domain.User.User user = this.DomainFactory.BuildDomainEntityType(request.User);
            user.SetPassword(this._aspNetCryptology.HashPassword(request.User.Password));

            this.Repository.Update(user);

            this.Response.RegisterSuccess(MessageFormatter.EntitySuccessfullyUpdated<Domain.User.User>(user.Id));

            return this.Response;
        }

        [ServiceRequestMethod]
        public UserResponse Delete(UserServiceRequest request)
        {
            if (request.User == null)
            {
                this.Response.RegisterError(MessageFormatter.IsARequiredField(
                    nameof(UserServiceRequest.User)));
                return this.Response;
            }

            Domain.User.User user = this.DomainFactory.BuildDomainEntityType(request.User);
            user.SetPassword(this._aspNetCryptology.HashPassword(request.User.Password));

            this.Repository.Delete(user);

            this.Response.RegisterSuccess(MessageFormatter.EntitySuccessfullyRemoved<Domain.User.User>(user.Id));

            return this.Response;
        }
    }
}
