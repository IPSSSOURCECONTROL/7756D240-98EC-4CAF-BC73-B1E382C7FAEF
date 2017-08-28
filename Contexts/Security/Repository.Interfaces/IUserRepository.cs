using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.Security.Domain.User;

namespace KhanyisaIntel.Kbit.Framework.Security.Repository.Interfaces
{
    public interface IUserRepository: IBasicRepository<User>, ISecurityContextAvailable
    {
    }
}
