using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository.Workflow;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Workflows.ProductListing.Steps
{
    public class RemoveProductListing : 
        WorkflowStepBase<ProductListingWorkflowContext, ProductListingRepository>
    {
        public RemoveProductListing(ProductListingRepository repository,
            ProductListingWorkflowContext context) : base(repository, context)
        {
        }
    }
}