using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Architecture.Tests.Infrustructure.AOP.Attributes;
using Architecture.Tests.Infrustructure.MongoDb;
using Architecture.Tests.Infrustructure.Reflection;
using Architecture.Tests.Infrustructure.Repository;
using Architecture.Tests.Infrustructure.Validation;
using Architecture.Tests.Infrustructure.Workflow.Exceptions;
using Architecture.Tests.Security.Domain.Role;

namespace Architecture.Tests.Security.Repository
{
    public class RoleRepository : BasicRepositoryBase, IRoleRepository
    {
        private readonly IObjectActivator _objectActivator;

        public RoleRepository(IDatabaseContext databaseContext, IObjectActivator objectActivator) : base(databaseContext)
        {
            this._objectActivator = objectActivator;
        }

        [Transactional]
        [ValidateMethodArguments]
        public void Add(Role entity)
        {
            if (this.DatabaseContext.TableForQuery<Role>().Any(x => x.Id == entity.Id))
                throw new EntityAlreadyExistException(MethodBase.GetCurrentMethod(), "Record already exists");

            if (this.DatabaseContext.TableForQuery<Role>().ToList().Any(x => x.TypeName == entity.TypeName))
                throw new EntityAlreadyExistException(MethodBase.GetCurrentMethod(), "Name already exists");

            this.DatabaseContext.Add(entity);
        }

        [Transactional]
        [ValidateMethodArguments]
        public void Update(Role entity)
        {
            if (!this.DatabaseContext.TableForQuery<Role>().Any(x => x.Id == entity.Id))
                throw new EntityDoesNotExistException(MethodBase.GetCurrentMethod(),
                    $"Record with id {entity.Id} does not exist.");

            this.DatabaseContext.Remove<Role>(entity.Id);
            this.DatabaseContext.Add(entity);
        }

        [Transactional]
        [ValidateMethodArguments]
        public void Delete(Role entity)
        {
            if (!this.DatabaseContext.TableForQuery<Role>().Any(x => x.Id == entity.Id))
                throw new EntityDoesNotExistException(MethodBase.GetCurrentMethod(),
                    $"Record with id {entity.Id} does not exist.");

            this.DatabaseContext.Remove<Role>(entity.Id);
        }

        public Role GetById(string id)
        {
            Role roleDa = this.DatabaseContext.TableForQuery<Role>().FirstOrDefault(x => x.Id == id);

            if (roleDa == null)
                return null;

            return roleDa;
        }

        public IEnumerable<Role> GetAll()
        {
            return this.DatabaseContext.TableForQuery<Role>();
        }

        public bool IsExist(Role entity)
        {
            Validator.CheckReferenceTypeForNull(entity, nameof(entity));

            return this.DatabaseContext.TableForQuery<Role>().Any(x => x.Id == entity.Id);
        }
    }
}