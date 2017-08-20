using System.Web.Http;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Customer;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;

namespace KhanyisaIntel.Kbit.Framework.Mvc.Controllers.Customer
{
    [Authorize]
    public class CustomerController : ApiController, IKbitApiController<CustomerAm>
    {
        private readonly ICustomerService _applicationService;

        public CustomerController(ICustomerService customerService)
        {
            this._applicationService = customerService;
        }

        [HttpGet]
        public IHttpActionResult GetById(string id)
        {
            CustomerResponse response = this._applicationService.GetById(
                new CustomerServiceRequest()
                {
                    EntityId = id
                });

            if (response.ServiceResult != ServiceResult.Success)
                return this.BadRequest(response.Message);

            return this.Ok(response.CustomerAm);
        }

        public IHttpActionResult GetAll()
        {
            throw new System.NotImplementedException();
        }

        public IHttpActionResult Add(CustomerAm applicationModel)
        {
            throw new System.NotImplementedException();
        }

        public IHttpActionResult Update(CustomerAm applicationModel)
        {
            throw new System.NotImplementedException();
        }

        public IHttpActionResult Delete(CustomerAm applicationModel)
        {
            throw new System.NotImplementedException();
        }
    }
}
