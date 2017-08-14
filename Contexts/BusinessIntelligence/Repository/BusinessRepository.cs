using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Business;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Exception;
using KhanyisaIntel.Kbit.Framework.Infrustructure.MongoDb;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Workflow.Exceptions;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository
{
    public class BusinessRepository : BasicRepositoryBase, IBusinessRepository
    {
        public BusinessRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {
        }

        [Transactional]
        [ValidateMethodArguments]
        public void Add(Business entity)
        {
            if (this.DatabaseContext.Table<Business>().Any(x => x.Id == entity.Id))
                throw new EntityAlreadyExistException(MethodBase.GetCurrentMethod(),
                    MessageFormatter.RecordWithIdAlreadyExists(entity.Id));


            this.DatabaseContext.Add(entity);
        }

        [Transactional]
        [ValidateMethodArguments]
        public void Update(Business entity)
        {
            if (!this.DatabaseContext.TableForQuery<Business>().Any(x => x.Id == entity.Id))
                throw new EntityDoesNotExistException(MethodBase.GetCurrentMethod(),
                    MessageFormatter.RecordWithIdDoesNotExist(entity.Id));

            this.DatabaseContext.Remove<Business>(entity.Id);
            this.DatabaseContext.Add(entity);
        }

        [Transactional]
        [ValidateMethodArguments]
        public void Delete(Business entity)
        {
            if (!this.DatabaseContext.Table<Business>().Any(x => x.Id == entity.Id))
                throw new EntityDoesNotExistException(MethodBase.GetCurrentMethod(),
                    MessageFormatter.RecordWithIdDoesNotExist(entity.Id));

            this.DatabaseContext.Remove<Business>(entity.Id);
        }

        [Transactional]
        [ValidateMethodArguments]
        public Business GetById(string id)
        {
            return this.DatabaseContext.TableForQuery<Business>().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Business> GetAll()
        {
            return this.DatabaseContext.Table<Business>();
        }

        [Transactional]
        [ValidateMethodArguments]
        public bool IsExist(Business entity)
        {
            return this.DatabaseContext.Table<Business>().Any(x => x.Id == entity.Id);
        }
    }
}