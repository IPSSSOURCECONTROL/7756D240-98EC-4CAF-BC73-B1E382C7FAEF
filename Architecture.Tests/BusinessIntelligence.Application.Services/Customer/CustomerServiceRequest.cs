using Architecture.Tests.BusinessIntelligence.Application.Models;
using Architecture.Tests.Infrustructure.Application;

namespace Architecture.Tests.BusinessIntelligence.Application.Services.Customer
{
    public class CustomerServiceRequest : ServiceRequestBase
    {
        public CustomerAm Customer { get; set; }=new CustomerAm();
    }
}