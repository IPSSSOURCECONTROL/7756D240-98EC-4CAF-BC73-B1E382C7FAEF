using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Exception;
using KhanyisaIntel.Kbit.Framework.Infrustructure.MongoDb;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities;
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
                    MessageFormatter.RecordWithIdDoesNotExist(entity.Id));

            this.DatabaseContext.Remove<Role>(entity.Id);
            this.DatabaseContext.Add(entity);
        }

        [Transactional]
        [ValidateMethodArguments]
        public void Delete(Role entity)
        {
            if (!this.DatabaseContext.TableForQuery<Role>().Any(x => x.Id == entity.Id))
                throw new EntityDoesNotExistException(MethodBase.GetCurrentMethod(),
                    MessageFormatter.RecordWithIdDoesNotExist(entity.Id));

            this.DatabaseContext.Remove<Role>(entity.Id);
        }

        [ValidateMethodArguments]
        public Role GetById(string id)
        {
            return this.DatabaseContext.Table<Role>().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Role> GetAll()
        {
            return this.DatabaseContext.TableForQuery<Role>();
        }

        [ValidateMethodArguments]
        public bool IsExist(Role entity)
        {
            return this.DatabaseContext.TableForQuery<Role>().Any(x => x.Id == entity.Id);
        }
    }
}