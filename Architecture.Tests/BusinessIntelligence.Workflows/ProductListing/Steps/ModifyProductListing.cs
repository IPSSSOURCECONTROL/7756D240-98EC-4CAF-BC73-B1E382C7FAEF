using Architecture.Tests.BusinessIntelligence.Repository;
using Architecture.Tests.Infrustructure.Workflow;

namespace Architecture.Tests.BusinessIntelligence.Workflows.ProductListing.Steps
{
    public class ModifyProductListing : WorkflowStepBase<ProductListingWorkflowContext, ProductListingRepository>
    {
        public ModifyProductListing(ProductListingRepository repository,
            ProductListingWorkflowContext context) : base(repository, context)
        {
        }
    }
}