using System.Security.Principal;
using System.Web.Http;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Customer;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;

namespace KhanyisaIntel.Kbit.Framework.Mvc.Controllers.Customer
{
    [Authorize]
    public class CustomerController : ApiController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            this._customerService = customerService;
        }

        [HttpGet]
        public IHttpActionResult GetCustomerById(string id)
        {
            CustomerServiceRequest request = new CustomerServiceRequest();
            request.EntityId = id;

            IPrincipal user = this.User;
            var asdasd = user.Identity.Name;

            CustomerResponse serviceResponse = this._customerService.GetById(request);

            switch (serviceResponse.ServiceResult)
            {
                case ServiceResult.Default:
                    return this.BadRequest(serviceResponse.Message);
                case ServiceResult.Success:
                    return this.Ok(serviceResponse.CustomerAm);
                case ServiceResult.Error:
                    return this.BadRequest(serviceResponse.Message);
                case ServiceResult.Exception:
                    return this.BadRequest(serviceResponse.Message);
                default:
                    return this.BadRequest("Undefined response type.");
            }
        }
    }
}
