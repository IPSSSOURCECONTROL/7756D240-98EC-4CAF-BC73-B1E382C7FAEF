using System.Collections.Generic;
using Architecture.Tests.BusinessIntelligence.Application.Models;
using Architecture.Tests.Infrustructure.Application;

namespace Architecture.Tests.BusinessIntelligence.Application.Services.Customer
{
    public class CustomerResponse : ServiceResponseBase
    {
        public CustomerResponse()
        {
        }

        public CustomerResponse(string message, ServiceResult serviceResult): base(message, serviceResult)
        {
        }

        public CustomerAm CustomerAm { get; set; }=new CustomerAm();
        public IEnumerable<CustomerAm> CustomerAms { get; set; } = new List<CustomerAm>();
    }
}