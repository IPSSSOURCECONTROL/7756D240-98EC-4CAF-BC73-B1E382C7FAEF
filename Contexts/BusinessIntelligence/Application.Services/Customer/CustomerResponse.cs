using System.Collections.Generic;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Customer
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