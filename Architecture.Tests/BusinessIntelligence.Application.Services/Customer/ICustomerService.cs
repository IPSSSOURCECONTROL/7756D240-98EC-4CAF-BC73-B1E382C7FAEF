using Architecture.Tests.Infrustructure.Application;

namespace Architecture.Tests.BusinessIntelligence.Application.Services.Customer
{
    public interface ICustomerService : IApplicationService<CustomerServiceRequest, CustomerResponse>
    {
    }
}