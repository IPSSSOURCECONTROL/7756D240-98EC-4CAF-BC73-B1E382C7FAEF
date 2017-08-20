using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository;
using KhanyisaIntel.Kbit.Framework.Security.Domain.AspNet;

namespace KhanyisaIntel.Kbit.Framework.Security.Repository.Interfaces
{
    public interface IAspNetPrincipleUserRepository:
                IBasicRepository<AspNetPrincipleUser>, ISecurityContextAvailable
    {
    }
}
