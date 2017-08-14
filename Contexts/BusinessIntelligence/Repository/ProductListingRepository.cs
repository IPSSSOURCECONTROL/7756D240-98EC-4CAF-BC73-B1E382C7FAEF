using System.Collections.Generic;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.ProductListing;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Workflows.ProductListing;
using KhanyisaIntel.Kbit.Framework.Infrustructure.MongoDb;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository
{
    public class ProductListingRepository : WorkflowRepository<ProductListingWorkflow, ProductListingWorkflowContext>, IProductListingRepository
    {
        public ProductListingRepository(
            IDatabaseContext databaseContext, 
            ProductListingWorkflow productListingWorkflow) : base(databaseContext)
        {
            productListingWorkflow.WorkflowData = this;
            this.Workflow = productListingWorkflow;
        }

        public ProductListing GetById(string id)
        {
            return null;
        }

        public IEnumerable<ProductListing> GetAll()
        {
            return null;
        }

        public bool IsExist(ProductListing entity)
        {
            return false;
        }
    }
}