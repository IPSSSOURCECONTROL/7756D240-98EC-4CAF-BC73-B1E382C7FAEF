using KhanyisaIntel.Kbit.Framework.Infrustructure.Security;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Repository.Interfaces
{
    public interface ISecurityContextAvailable
    {
        void SetSecurityContext(AuthorizationContext authorizationContext);
    }
}