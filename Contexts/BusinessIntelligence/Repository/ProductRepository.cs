using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Product;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository.Interfaces;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository
{
    public class ProductRepository : BasicRepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}
