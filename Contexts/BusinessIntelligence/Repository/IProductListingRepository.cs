using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.ProductListing;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Workflows.ProductListing;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository.Interfaces;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository
{
    public interface IProductListingRepository: IReadOnlyRepository<ProductListing>, 
        IRepositoryWorkflowAvailable<ProductListingWorkflow, ProductListingWorkflowContext>
    {

    }
}