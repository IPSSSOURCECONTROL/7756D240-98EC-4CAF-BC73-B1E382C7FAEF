using System;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository.Workflow;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Workflows.ProductListing.Steps
{
    public class AddProductListing: WorkflowStepBase<ProductListingWorkflowContext, ProductListingRepository>
    {
        public AddProductListing(ProductListingRepository repository, 
            ProductListingWorkflowContext context) : base(repository, context)
        {
        }

        protected override void OnPretransaction()
        {
            this.Repository.GetAll();
            if(this.WorkflowContext.Entity.Business == null)
                throw new Exception();
        }
    }
}
