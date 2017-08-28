using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.Security.Domain.Role;

namespace KhanyisaIntel.Kbit.Framework.Security.Repository.Interfaces
{
    public interface IRoleRepository : IBasicRepository<Role>, ISecurityContextAvailable
    {
    }
}
