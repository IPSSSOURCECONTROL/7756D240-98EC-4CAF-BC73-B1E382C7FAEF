using Architecture.Tests.Infrustructure.Workflow;

namespace Architecture.Tests.Infrustructure.Repository
{
    public interface ISecurityContextAvailable
    {
        void SetSecurityContext(AuthorizationContext authorizationContext);
    }
}