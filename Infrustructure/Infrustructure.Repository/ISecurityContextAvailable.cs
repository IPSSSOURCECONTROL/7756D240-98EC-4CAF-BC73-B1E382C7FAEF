using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository.Workflow;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Repository
{
    public interface ISecurityContextAvailable
    {
        void SetSecurityContext(AuthorizationContext authorizationContext);
    }
}