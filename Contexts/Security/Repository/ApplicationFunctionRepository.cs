using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Exception;
using KhanyisaIntel.Kbit.Framework.Infrustructure.MongoDb;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Reflection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Validation;
using KhanyisaIntel.Kbit.Framework.Security.Domain.ApplicationFunction;
using KhanyisaIntel.Kbit.Framework.Security.Repository.Interfaces;

namespace KhanyisaIntel.Kbit.Framework.Security.Repository
{
    public class ApplicationFunctionRepository: BasicRepositoryBase, IApplicationFunctionRepository
    {
        private readonly IObjectActivator _objectActivator;

        public ApplicationFunctionRepository(
            IDatabaseContext databaseContext, 
            IObjectActivator objectActivator) : base(databaseContext)
        {
            this._objectActivator = objectActivator;
        }

        [Transactional]
        public void Add(ApplicationFunction entity)
        {
            Validator.CheckReferenceTypeForNull(entity, nameof(entity), MethodBase.GetCurrentMethod(),
                this.GetType());

            if (this.DatabaseContext.TableForQuery<ApplicationFunction>().Any(x => x.Name == entity.Name))
                throw new KBitException(MethodBase.GetCurrentMethod(),
                    MessageFormatter.RecordWithNameAlreadyExists(entity.Name));

            if(this.DatabaseContext.TableForQuery<ApplicationFunction>().Any(x=>x.Id == entity.Id))
                throw  new KBitException(MethodBase.GetCurrentMethod(), 
                    MessageFormatter.RecordWithIdAlreadyExists(entity.Id));

            this.DatabaseContext.Add(entity);
        }

        [Transactional]
        public void Update(ApplicationFunction entity)
        {
            Validator.CheckReferenceTypeForNull(entity, nameof(entity), MethodBase.GetCurrentMethod(),
                this.GetType());

            if (!this.DatabaseContext.TableForQuery<ApplicationFunction>().Any(x => x.Id == entity.Id))
                throw new KBitException(MethodBase.GetCurrentMethod(),
                    MessageFormatter.RecordWithIdDoesNotExist(entity.Id));

            this.DatabaseContext.Remove<ApplicationFunction>(entity.Id);
            this.DatabaseContext.Add(entity);
        }

        [Transactional]
        public void Delete(ApplicationFunction entity)
        {
            Validator.CheckReferenceTypeForNull(entity, nameof(entity), MethodBase.GetCurrentMethod(),
                this.GetType());

            this.DatabaseContext.Remove<ApplicationFunction>(entity.Id);
        }

        public ApplicationFunction GetById(string id)
        {
            Validator.IsNullEmptyOrWhitespace(id, nameof(id), MethodBase.GetCurrentMethod(), this.GetType());

            return this.DatabaseContext.Table<ApplicationFunction>().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<ApplicationFunction> GetAll()
        {
            return this.DatabaseContext.Table<ApplicationFunction>();
        }

        public bool IsExist(ApplicationFunction entity)
        {
            Validator.CheckReferenceTypeForNull(entity, nameof(entity), MethodBase.GetCurrentMethod(),
                this.GetType());

            return this.DatabaseContext.TableForQuery<ApplicationFunction>().Any(x => x.Id == entity.Id);
        }
    }
}
