using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository.Workflow;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Workflows.ProductListing.Steps
{
    public class ModifyProductListing : WorkflowStepBase<ProductListingWorkflowContext, ProductListingRepository>
    {
        public ModifyProductListing(ProductListingRepository repository,
            ProductListingWorkflowContext context) : base(repository, context)
        {
        }
    }
}