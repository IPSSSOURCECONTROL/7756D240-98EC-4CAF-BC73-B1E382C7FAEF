using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Exception;
using KhanyisaIntel.Kbit.Framework.Infrustructure.MongoDb;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities;
using KhanyisaIntel.Kbit.Framework.Security.Domain.AspNet;
using KhanyisaIntel.Kbit.Framework.Security.Repository.Interfaces;

namespace KhanyisaIntel.Kbit.Framework.Security.Repository
{
    public class AspNetPrincipleUserRepository: BasicRepositoryBase, IAspNetPrincipleUserRepository
    {
        public AspNetPrincipleUserRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {
        }

        [Transactional]
        [ValidateMethodArguments]
        public void Add(AspNetPrincipleUser entity)
        {
            this.ThrowErrorOnEntityExists<AspNetPrincipleUser>(entity.Id);

            if (this.DatabaseContext.Table<AspNetPrincipleUser>()
                .Any(x => x.EmailAddress == entity.EmailAddress))
            {
                throw new EntityAlreadyExistException(MethodBase.GetCurrentMethod(),
                    MessageFormatter.RecordWithNameAlreadyExists(entity.EmailAddress));
            }

            this.DatabaseContext.Add(entity);
        }

        [Transactional]
        [ValidateMethodArguments]
        public void Update(AspNetPrincipleUser entity)
        {
            this.ThrowErrorOnEntityDoesNotExist<AspNetPrincipleUser>(entity.Id);
            this.DatabaseContext.Remove<AspNetPrincipleUser>(entity.Id);
            this.DatabaseContext.Add(entity);
        }

        [Transactional]
        [ValidateMethodArguments]
        public void Delete(AspNetPrincipleUser entity)
        {
            this.ThrowErrorOnEntityDoesNotExist<AspNetPrincipleUser>(entity.Id);
            this.DatabaseContext.Remove<AspNetPrincipleUser>(entity.Id);
        }

        [ValidateMethodArguments]
        public AspNetPrincipleUser GetById(string id)
        {
            return this.DatabaseContext.Table<AspNetPrincipleUser>()
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<AspNetPrincipleUser> GetAll()
        {
            return this.DatabaseContext.Table<AspNetPrincipleUser>();
        }

        public bool IsExist(AspNetPrincipleUser entity)
        {
            if (entity == null)
                return false;

            return this.DatabaseContext.Table<AspNetPrincipleUser>()
                .Any(x => x.Id == entity.Id);
        }
    }
}
