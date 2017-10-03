using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.ProductListing;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository.Interfaces;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository
{
    public class ProductListingRepository : BasicRepositoryBase<ProductListing>, IProductListingRepository
    {
        public ProductListingRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}