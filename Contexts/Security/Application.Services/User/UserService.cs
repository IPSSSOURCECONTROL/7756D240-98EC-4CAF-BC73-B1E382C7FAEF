using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Encryption;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities;
using KhanyisaIntel.Kbit.Framework.Security.Application.Models;
using KhanyisaIntel.Kbit.Framework.Security.Repository.Interfaces;

namespace KhanyisaIntel.Kbit.Framework.Security.Application.Services.User
{
    public class UserService : ApplicationServiceBase<
        UserServiceRequest,
        UserResponse, 
        IUserRepository, 
        Domain.User.User, UserAm>,IUserService
    {
        private readonly IAspNetCryptology _aspNetCryptology;

        public UserService(IAspNetCryptology aspNetCryptology)
        {
            this._aspNetCryptology = aspNetCryptology;
        }

        [AuthorizeAction]
        [ServiceRequestMethod]
        public override UserResponse Add(UserServiceRequest request)
        {
            if (request.ApplicationModel == null)
            {
                this.Response.RegisterError(MessageFormatter.IsARequiredField(
                    MessageFormatter.NormalizeApplicationModelName(typeof(UserAm))));
                return this.Response;
            }

            Domain.User.User user = this.DomainFactory.BuildDomainEntityType(request.ApplicationModel);
            user.SetPassword(this._aspNetCryptology.HashPassword(request.ApplicationModel.Password));

            this.Repository.Add(user);

            this.Response.RegisterSuccess(MessageFormatter.EntitySuccessfullyAdded<Domain.User.User>(user.Id));

            return this.Response;
        }

        [AuthorizeAction]
        [ServiceRequestMethod]
        public override UserResponse Update(UserServiceRequest request)
        {
            if (request.ApplicationModel == null)
            {
                this.Response.RegisterError(MessageFormatter.IsARequiredField(
                    MessageFormatter.NormalizeApplicationModelName(typeof(UserAm))));
                return this.Response;
            }

            if (string.IsNullOrWhiteSpace(request.ApplicationModel.Id))
            {
                this.Response.RegisterError(MessageFormatter.IsARequiredField(
                    nameof(UserAm.Id)));
                return this.Response;
            }

            Domain.User.User user = this.DomainFactory.BuildDomainEntityType(request.ApplicationModel, false);
            user.SetPassword(this._aspNetCryptology.HashPassword(request.ApplicationModel.Password));

            this.Repository.Update(user);

            this.Response.RegisterSuccess(MessageFormatter.EntitySuccessfullyUpdated<Domain.User.User>(user.Id));

            return this.Response;
        }
    }
}
