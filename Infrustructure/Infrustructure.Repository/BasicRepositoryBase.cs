using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Exception;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Logging;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Security;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Workflow.Exceptions;
using KhanyisaIntel.Kbit.Framework.Security.Domain.User;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Repository
{

    /// <summary>
    /// Base class for <see cref="IBasicRepository{TEntity}"/> types. All repositories of 
    /// <see cref="IBasicRepository{TEntity}"/> must derive from this type.<para/>
    /// Exposes a <see cref="DatabaseContext"/> to query the data source.
    /// Exposes a <see cref="Logger"/> for general logging purposes, if any.
    /// </summary>
    public abstract class BasicRepositoryBase<TDomainEntity> : ISecurityContextAvailable, IDatabaseContextAvailable, IBasicRepository<TDomainEntity> where TDomainEntity : AggregateRoot, IEntity
    {
        protected BasicRepositoryBase(IDatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        [MandatoryInjection]
        public ILoggingType Logger { get; set; }
        public IDatabaseContext DatabaseContext { get; set; }
        public AuthorizationContext AuthorizationContext { get; private set; }
        public void SetSecurityContext(AuthorizationContext authorizationContext)
        {
            if (authorizationContext == null)
                throw new ArgumentNullException(nameof(authorizationContext));

            //if (authorizationContext.Role == null)
            //    throw new ArgumentNullException("Role can not be null.");

            if (string.IsNullOrWhiteSpace(authorizationContext.BusinessId))
                throw new ArgumentException("Business Id can not be null, empty or whitespace.");

            if (string.IsNullOrWhiteSpace(authorizationContext.UserId))
                throw new ArgumentException("User Id can not be null, empty or whitespace.");

            this.AuthorizationContext = authorizationContext;
        }

        protected void Authorize()
        {
            //if (!this.DatabaseContext.TableForQuery<UserDa>().Any(x => x.Id == this.AuthorizationContext.UserId))
            //{
            //    throw new NotAuthorizedException($"Not authorized to perform action");
            //}

            //TILL WE IMPLEMENT APPLICATION FUNCTIONS ON ROLES
            //if (this.AuthorizationContext.Role is AdministratorRole || this.AuthorizationContext.Role is SupermanRole)
            //{
            //    return;
            //}

            throw new NotAuthorizedException($"Not authorized to perform action");
        }

        /// <summary>
        /// Will throw an <see cref="EntityAlreadyExistException"/> if an 
        /// entity with the given <see cref="id"/> already exists in the data source.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        protected void ThrowErrorOnEntityExists<TEntity>(string id) where TEntity : class, IEntity
        {
            if (this.DatabaseContext.Table<TEntity>()
                .Any(x => x.Id == id))
            {
                throw new EntityAlreadyExistException(MethodBase.GetCurrentMethod(),
                    MessageFormatter.RecordWithIdAlreadyExists(id));
            }
        }

        protected void ThrowErrorOnEmailExists(User entity)
        {
            if (this.DatabaseContext.Table<User>()
                .Any(x => x.Email == entity.Email))
            {
                throw new EntityAlreadyExistException(MethodBase.GetCurrentMethod(),
                    MessageFormatter.RecordWithNameAlreadyExists(entity.Email));
            }
        }

        /// <summary>
        /// Will throw an <see cref="EntityAlreadyExistException"/> if an 
        /// entity with the given <see cref="id"/> does not exists in the data source.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        protected void ThrowErrorOnEntityDoesNotExist<TEntity>(string id) where TEntity : class, IEntity
        {
            if (!this.DatabaseContext.Table<TEntity>()
                .Any(x => x.Id == id))
            {
                throw new EntityDoesNotExistException(MethodBase.GetCurrentMethod(),
                    MessageFormatter.RecordWithIdAlreadyExists(id));
            }
        }

        [Transactional]
        [ValidateMethodArguments]
        public virtual void Add(TDomainEntity entity)
        {
            this.ThrowErrorOnEntityExists<TDomainEntity>(entity.Id);
            this.DatabaseContext.Add(entity);
        }

        [Transactional]
        [ValidateMethodArguments]
        public virtual void Update(TDomainEntity entity)
        {
            this.ThrowErrorOnEntityDoesNotExist<TDomainEntity>(entity.Id);

            this.DatabaseContext.Remove<TDomainEntity>(entity.Id);
            this.DatabaseContext.Add(entity);
        }

        [Transactional]
        [ValidateMethodArguments]
        public virtual void Delete(TDomainEntity entity)
        {
            this.ThrowErrorOnEntityDoesNotExist<TDomainEntity>(entity.Id);

            this.DatabaseContext.Remove<TDomainEntity>(entity.Id);
        }

        [ValidateMethodArguments]
        public virtual TDomainEntity GetById(string id)
        {
            return this.DatabaseContext.TableForQuery<TDomainEntity>().FirstOrDefault(x => x.Id == id);
        }

        [ValidateMethodArguments]
        public virtual IEnumerable<TDomainEntity> GetAll()
        {
            return this.DatabaseContext.Table<TDomainEntity>();
        }

        [ValidateMethodArguments]
        public virtual bool IsExist(TDomainEntity entity)
        {
            if (entity == null)
                return false;

            return this.DatabaseContext.Table<TDomainEntity>().Any(x => x.Id == entity.Id);
        }
    }
}