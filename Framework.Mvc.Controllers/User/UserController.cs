using System.Web.Http;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Security.Application.Models;
using KhanyisaIntel.Kbit.Framework.Security.Application.Services.User;

namespace KhanyisaIntel.Kbit.Framework.Mvc.Controllers.User
{
    [Authorize]
    public class UserController: ApiController, IKbitApiController<UserAm>
    {
        private readonly IUserService _applicationService;

        public UserController(IUserService userService)
        {
            this._applicationService = userService;
        }
        
        [HttpGet]
        public IHttpActionResult GetById(string id)
        {
            UserResponse response = this._applicationService.GetById(
                new UserServiceRequest()
                {
                    EntityId = id
                });

            if (response.ServiceResult != ServiceResult.Success)
                return this.BadRequest(response.Message);

            return this.Ok(response.User);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            UserResponse response = this._applicationService.GetAll(new UserServiceRequest());

            if (response.ServiceResult != ServiceResult.Success)
                return this.BadRequest(response.Message);

            return this.Ok(response.Users);
        }

        [HttpPost]
        public IHttpActionResult Add(UserAm applicationModel)
        {
            UserResponse response = this._applicationService.Add(new UserServiceRequest()
            {
                User = applicationModel
            });

            if (response.ServiceResult != ServiceResult.Success)
                return this.BadRequest(response.Message);

            return this.Ok(response.Message);
        }

        [HttpPut]
        public IHttpActionResult Update(UserAm applicationModel)
        {
            UserResponse response = this._applicationService.Update(new UserServiceRequest()
            {
                User = applicationModel
            });

            if (response.ServiceResult != ServiceResult.Success)
                return this.BadRequest(response.Message);

            return this.Ok(response.Message);
        }

        [HttpDelete]
        public IHttpActionResult Delete(UserAm applicationModel)
        {
            UserResponse response = this._applicationService.Delete(new UserServiceRequest()
            {
                User = applicationModel
            });

            if (response.ServiceResult != ServiceResult.Success)
                return this.BadRequest(response.Message);

            return this.Ok(response.Message);
        }
    }
}
