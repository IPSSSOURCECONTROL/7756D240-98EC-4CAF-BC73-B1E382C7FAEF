using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Customer;

namespace KhanyisaIntel.Kbit.Framework.Mvc.Api.Controllers.Customer
{
    //[Authorize]
    public class CustomerController : KbitApiControllerBase<CustomerAm,
        ICustomerService,CustomerServiceRequest,CustomerResponse>
    {
        public CustomerController(ICustomerService applicationService) : base(applicationService)
        {
        }

    }
}
