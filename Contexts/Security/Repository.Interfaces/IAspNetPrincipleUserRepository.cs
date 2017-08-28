using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.Security.Domain.AspNet;

namespace KhanyisaIntel.Kbit.Framework.Security.Repository.Interfaces
{
    public interface IAspNetPrincipleUserRepository:
                IBasicRepository<AspNetPrincipleUser>, ISecurityContextAvailable
    {
    }
}
