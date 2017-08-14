using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Customer
{
    public class CustomerServiceRequest : ServiceRequestBase
    {
        public CustomerAm Customer { get; set; }=new CustomerAm();
    }
}