using Architecture.Tests.Infrustructure.Repository;

namespace Architecture.Tests.Security.Domain.ApplicationFunction
{
    public interface IApplicationFunctionRepository: IBasicRepository<ApplicationFunction>, ISecurityContextAvailable
    {
    }
}
