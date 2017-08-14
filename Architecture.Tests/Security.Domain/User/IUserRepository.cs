using Architecture.Tests.Infrustructure.Repository;

namespace Architecture.Tests.Security.Domain.User
{
    public interface IUserRepository: IBasicRepository<User>, ISecurityContextAvailable
    {
    }
}
