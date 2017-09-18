using System.Web.Mvc;
using Kbit.ControlCentre.Models;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Customer;

namespace Kbit.ControlCentre.Controllers
{
    public class CustomerController : BaseController<ICustomerService, CustomerServiceRequest,
            CustomerResponse, CustomerAm, ViewEditCustomerVm>
    {
        // GET: Customer
        public ActionResult Index()
        {
            return this.View("Index");
        }
    }
}