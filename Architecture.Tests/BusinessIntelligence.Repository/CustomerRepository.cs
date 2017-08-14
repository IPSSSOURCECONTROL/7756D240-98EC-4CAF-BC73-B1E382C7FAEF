using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Architecture.Tests.BusinessIntelligence.Domain.Customer;
using Architecture.Tests.Infrustructure.AOP.Attributes;
using Architecture.Tests.Infrustructure.MongoDb;
using Architecture.Tests.Infrustructure.Repository;
using Architecture.Tests.Infrustructure.Utilities;
using Architecture.Tests.Infrustructure.Workflow.Exceptions;

namespace Architecture.Tests.BusinessIntelligence.Repository
{
    public class CustomerRepository: BasicRepositoryBase, ICustomerRepository
    {
        public CustomerRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {
        }

        [Transactional]
        [ValidateMethodArguments]
        public void Add(Customer entity)
        {
            if(this.DatabaseContext.Table<Customer>().Any(x=> x.Id == entity.Id))
                throw new EntityAlreadyExistException(MethodBase.GetCurrentMethod(), 
                    MessageFormatter.RecordWithIdAlreadyExists(entity.Id));

            this.DatabaseContext.Add(entity);
        }

        [Transactional]
        [ValidateMethodArguments]
        public void Update(Customer entity)
        {
            if (!this.DatabaseContext.TableForQuery<Customer>().Any(x => x.Id == entity.Id))
                throw new EntityDoesNotExistException(MethodBase.GetCurrentMethod(),
                    MessageFormatter.RecordWithIdDoesNotExist(entity.Id));

            this.DatabaseContext.Remove<Customer>(entity.Id);
            this.DatabaseContext.Add(entity);
        }

        [Transactional]
        [ValidateMethodArguments]
        public void Delete(Customer entity)
        {
            if (!this.DatabaseContext.Table<Customer>().Any(x => x.Id == entity.Id))
                throw new EntityDoesNotExistException(MethodBase.GetCurrentMethod(),
                    MessageFormatter.RecordWithIdDoesNotExist(entity.Id));

            this.DatabaseContext.Remove<Customer>(entity.Id);
        }

        [Transactional]
        [ValidateMethodArguments]
        public Customer GetById(string id)
        {
            return this.DatabaseContext.TableForQuery<Customer>().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Customer> GetAll()
        {
            return this.DatabaseContext.Table<Customer>();
        }

        [Transactional]
        [ValidateMethodArguments]
        public bool IsExist(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
