using System;
using System.Collections.Generic;
using Architecture.Tests.BusinessIntelligence.Domain.Product;
using Architecture.Tests.Infrustructure.MongoDb;
using Architecture.Tests.Infrustructure.Repository;

namespace Architecture.Tests.BusinessIntelligence.Repository
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