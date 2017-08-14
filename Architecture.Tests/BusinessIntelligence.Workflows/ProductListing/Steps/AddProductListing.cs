using System;
using Architecture.Tests.BusinessIntelligence.Repository;
using Architecture.Tests.Infrustructure.Workflow;

namespace Architecture.Tests.BusinessIntelligence.Workflows.ProductListing.Steps
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
