using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Customer
{
    public class CustomerService: ApplicationServiceBase2<
        CustomerServiceRequest,CustomerResponse, 
        ICustomerRepository,
        Domain.Customer.Customer,
        CustomerAm>, ICustomerService
    {
    }
}
