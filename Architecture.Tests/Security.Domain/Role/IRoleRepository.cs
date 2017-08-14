using Architecture.Tests.Infrustructure.Repository;

namespace Architecture.Tests.Security.Domain.Role
{
    public interface IRoleRepository: IBasicRepository<Role>, ISecurityContextAvailable
    {
    }
}
