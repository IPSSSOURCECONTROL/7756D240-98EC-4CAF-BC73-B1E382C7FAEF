using System;
using System.Collections.Generic;
using Architecture.Tests.BusinessIntelligence.Domain.Business;
using Architecture.Tests.Infrustructure.MongoDb;
using Architecture.Tests.Infrustructure.Repository;

namespace Architecture.Tests.BusinessIntelligence.Repository
{
    public class BusinessRepository : BasicRepositoryBase, IBusinessRepository
    {
        public BusinessRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public void Add(Business entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Business entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Business entity)
        {
            throw new NotImplementedException();
        }

        public Business GetById(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Business> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool IsExist(Business entity)
        {
            throw new NotImplementedException();
        }
    }
}