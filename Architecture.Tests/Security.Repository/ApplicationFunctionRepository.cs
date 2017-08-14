using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Architecture.Tests.Infrustructure.AOP.Attributes;
using Architecture.Tests.Infrustructure.Exception;
using Architecture.Tests.Infrustructure.MongoDb;
using Architecture.Tests.Infrustructure.Reflection;
using Architecture.Tests.Infrustructure.Repository;
using Architecture.Tests.Infrustructure.Utilities;
using Architecture.Tests.Infrustructure.Validation;
using Architecture.Tests.Security.Domain.ApplicationFunction;

namespace Architecture.Tests.Security.Repository
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
