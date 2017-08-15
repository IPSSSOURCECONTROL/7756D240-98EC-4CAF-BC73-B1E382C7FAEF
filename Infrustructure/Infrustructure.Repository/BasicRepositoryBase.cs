using System;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Exception;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Logging;
using KhanyisaIntel.Kbit.Framework.Infrustructure.MongoDb;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Security;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Repository
{
    /// <summary>
    /// Base class for <see cref="IBasicRepository{TEntity}"/> types. All repositories of 
    /// <see cref="IBasicRepository{TEntity}"/> must derive from this type.
    /// </summary>
    public abstract class BasicRepositoryBase: ISecurityContextAvailable, IDatabaseContextAvailable
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
    }
}