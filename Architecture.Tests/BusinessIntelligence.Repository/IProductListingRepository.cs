using Architecture.Tests.BusinessIntelligence.Domain.ProductListing;
using Architecture.Tests.BusinessIntelligence.Workflows.ProductListing;
using Architecture.Tests.Infrustructure.Repository;

namespace Architecture.Tests.BusinessIntelligence.Repository
{
    public interface IProductListingRepository: IReadOnlyRepository<ProductListing>, 
        IRepositoryWorkflowAvailable<ProductListingWorkflow, ProductListingWorkflowContext>
    {

    }
}