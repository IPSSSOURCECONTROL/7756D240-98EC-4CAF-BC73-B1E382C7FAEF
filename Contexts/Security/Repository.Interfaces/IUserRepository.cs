using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository;
using KhanyisaIntel.Kbit.Framework.Security.Domain.User;

namespace KhanyisaIntel.Kbit.Framework.Security.Repository.Interfaces
{
    public interface IUserRepository: IBasicRepository<User>, ISecurityContextAvailable
    {
    }
}
