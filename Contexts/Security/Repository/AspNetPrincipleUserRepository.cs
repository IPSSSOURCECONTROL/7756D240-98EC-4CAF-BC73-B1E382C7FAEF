using System.Linq;
using System.Reflection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Exception;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities;
using KhanyisaIntel.Kbit.Framework.Security.Domain.AspNet;
using KhanyisaIntel.Kbit.Framework.Security.Repository.Interfaces;

namespace KhanyisaIntel.Kbit.Framework.Security.Repository
{
    public class AspNetPrincipleUserRepository: BasicRepositoryBase<AspNetPrincipleUser>, IAspNetPrincipleUserRepository
    {
        public AspNetPrincipleUserRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {
        }

        [Transactional]
        [ValidateMethodArguments]
        public override void Add(AspNetPrincipleUser entity)
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
    }
}
