using Architecture.Tests.BusinessIntelligence.Repository;
using Architecture.Tests.Infrustructure.Workflow;

namespace Architecture.Tests.BusinessIntelligence.Workflows.ProductListing.Steps
{
    public class RemoveProductListing : WorkflowStepBase<ProductListingWorkflowContext, ProductListingRepository>
    {
        public RemoveProductListing(ProductListingRepository repository,
            ProductListingWorkflowContext context) : base(repository, context)
        {
        }
    }
}