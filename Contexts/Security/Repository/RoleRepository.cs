using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.Security.Domain.Role;
using KhanyisaIntel.Kbit.Framework.Security.Repository.Interfaces;

namespace KhanyisaIntel.Kbit.Framework.Security.Repository
{
    public class RoleRepository : BasicRepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}