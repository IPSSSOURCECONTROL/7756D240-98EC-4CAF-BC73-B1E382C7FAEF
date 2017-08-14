using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Customer;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Exception;
using KhanyisaIntel.Kbit.Framework.Infrustructure.MongoDb;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Workflow.Exceptions;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository
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
