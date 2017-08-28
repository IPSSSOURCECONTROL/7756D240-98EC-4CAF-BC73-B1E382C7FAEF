using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Workflows.ProductListing.Steps;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Reflection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository.Workflow;
using KhanyisaIntel.Kbit.Framework.Infrustructure.WorkflowCommon;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Workflows.ProductListing
{
    public class ProductListingWorkflow: WorkflowBase<ProductListingWorkflowContext, 
        IProductListingRepository>
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
