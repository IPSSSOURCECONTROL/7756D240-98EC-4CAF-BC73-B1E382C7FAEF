using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Exception;
using KhanyisaIntel.Kbit.Framework.Infrustructure.MongoDb;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Reflection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Validation;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Workflow.Exceptions;
using KhanyisaIntel.Kbit.Framework.Security.Domain.Role;
using KhanyisaIntel.Kbit.Framework.Security.Repository.Interfaces;

namespace KhanyisaIntel.Kbit.Framework.Security.Repository
{
    public class RoleRepository : BasicRepositoryBase, IRoleRepository
    {
        public RoleRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {
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