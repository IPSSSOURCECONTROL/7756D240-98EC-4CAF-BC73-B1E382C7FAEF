using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.MongoDb;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Workflow.Exceptions;
using KhanyisaIntel.Kbit.Framework.Security.Domain.AspNet;
using KhanyisaIntel.Kbit.Framework.Security.Domain.User;
using KhanyisaIntel.Kbit.Framework.Security.Repository.Interfaces;

namespace KhanyisaIntel.Kbit.Framework.Security.Repository
{
    public class UserRepository : BasicRepositoryBase, IUserRepository
    {
        private readonly IAspNetPrincipleUserRepository _aspNetPrincipleUserRepository;

        public UserRepository(IDatabaseContext databaseContext, 
            IAspNetPrincipleUserRepository aspNetPrincipleUserRepository) : base(databaseContext)
        {
            this._aspNetPrincipleUserRepository = aspNetPrincipleUserRepository;
        }

        [Transactional]
        [ValidateMethodArguments]
        public void Add(User entity)
        {
            this.ThrowErrorOnEntityExists<User>(entity.Id);

            AspNetPrincipleUser aspNetPrincipleUser = AspNetPrincipleUser.MapFrom(entity);

            this.ThrowErrorOnEntityExists<AspNetPrincipleUser>(aspNetPrincipleUser.Id);

            this.ThrowErrorOnEmailExists(entity);

            this.DatabaseContext.Add(entity);
            this._aspNetPrincipleUserRepository.Add(aspNetPrincipleUser);
        }

        [Transactional]
        [ValidateMethodArguments]
        public void Update(User entity)
        {
            if (!this.DatabaseContext.Table<User>().Any(x => x.Id == entity.Id))
            {
                throw new EntityDoesNotExistException(MethodBase.GetCurrentMethod(),
                    MessageFormatter.RecordWithIdDoesNotExist(entity.Id));
            }
            this.ThrowErrorOnEmailExists(entity);

            this.DatabaseContext.Remove<User>(entity.Id);
            this.DatabaseContext.Add(entity);

            this._aspNetPrincipleUserRepository.Delete(AspNetPrincipleUser.MapFrom(entity));
            this._aspNetPrincipleUserRepository.Add(AspNetPrincipleUser.MapFrom(entity));
        }

        [Transactional]
        [ValidateMethodArguments]
        public void Delete(User entity)
        {
            if (!this.DatabaseContext.Table<User>().Any(x => x.Id == entity.Id))
            {
                throw new EntityDoesNotExistException(MethodBase.GetCurrentMethod(),
                    MessageFormatter.RecordWithIdDoesNotExist(entity.Id));
            }

            this.DatabaseContext.Remove<User>(entity.Id);
            this._aspNetPrincipleUserRepository.Delete(AspNetPrincipleUser.MapFrom(entity));
        }

        [ValidateMethodArguments]
        public User GetById(string id)
        {
            return this.DatabaseContext.Table<User>().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<User> GetAll()
        {
            return this.DatabaseContext.Table<User>();
        }

        [ValidateMethodArguments]
        public bool IsExist(User entity)
        {
            return this.DatabaseContext.Table<User>().Any(x => x.Id == entity.Id);
        }
    }
}
