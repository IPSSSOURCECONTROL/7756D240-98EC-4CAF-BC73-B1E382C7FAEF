using KhanyisaIntel.Kbit.Framework.Infrustructure.Security;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Repository
{
    public interface ISecurityContextAvailable
    {
        void SetSecurityContext(AuthorizationContext authorizationContext);
    }
}