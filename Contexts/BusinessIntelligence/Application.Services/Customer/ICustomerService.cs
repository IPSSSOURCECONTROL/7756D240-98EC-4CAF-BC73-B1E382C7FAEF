using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Customer
{
    public interface ICustomerService : IApplicationService<CustomerServiceRequest, CustomerResponse>
    {
        [AuthorizeAction]
        [ServiceRequestMethod]
        CustomerResponse AddCostEstimate(CustomerServiceRequest request);

        [AuthorizeAction]
        [ServiceRequestMethod]
        CustomerResponse GetCostEstimates(CustomerServiceRequest request);

        [AuthorizeAction]
        [ServiceRequestMethod]
        CustomerResponse DeleteProductListing(CustomerServiceRequest request);

        [AuthorizeAction]
        [ServiceRequestMethod]
        CustomerResponse GetProductListingItems(CustomerServiceRequest request);

        [AuthorizeAction]
        [ServiceRequestMethod]
        CustomerResponse UpdateCostEstimate(CustomerServiceRequest request);
    }
}