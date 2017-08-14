using Architecture.Tests.BusinessIntelligence.Repository;
using Architecture.Tests.BusinessIntelligence.Workflows.ProductListing.Steps;
using Architecture.Tests.Infrustructure.Reflection;
using Architecture.Tests.Infrustructure.Workflow;

namespace Architecture.Tests.BusinessIntelligence.Workflows.ProductListing
{
    public class ProductListingWorkflow: WorkflowBase<ProductListingWorkflowContext, IProductListingRepository>
    {
        public ProductListingWorkflow(IObjectActivator objectActivator)
        {
            this.ObjectActivator = objectActivator;
        }

        protected override void RegisterSteps()
        {
            this.AddStep<AddProductListing>(WorkflowOperation.Add);
            this.AddStep<ModifyProductListing>(WorkflowOperation.Update);
            this.AddStep<RemoveProductListing>(WorkflowOperation.Remove);
        }
    }
}
