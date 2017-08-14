using System;
using System.Collections.Generic;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Product;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.Infrustructure.MongoDb;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository
{
    public class ProductRepository : BasicRepositoryBase, IProductRepository
    {
        public ProductRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public void Add(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        public Product GetById(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool IsExist(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}