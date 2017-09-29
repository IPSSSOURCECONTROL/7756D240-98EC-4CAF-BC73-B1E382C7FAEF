using System;
using System.Reflection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Exception;
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

        }

        protected override void OnTransaction()
        {
            
        }
    }
}
