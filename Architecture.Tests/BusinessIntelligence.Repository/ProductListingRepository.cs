using System.Collections.Generic;
using Architecture.Tests.BusinessIntelligence.Domain.ProductListing;
using Architecture.Tests.BusinessIntelligence.Workflows.ProductListing;
using Architecture.Tests.Infrustructure.MongoDb;
using Architecture.Tests.Infrustructure.Repository;

namespace Architecture.Tests.BusinessIntelligence.Repository
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