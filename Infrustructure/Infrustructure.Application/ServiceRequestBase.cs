using KhanyisaIntel.Kbit.Framework.Infrustructure.Security;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Application
{
    public class ServiceRequestBase
    {
        public AuthorizationContext AuthorizationContext { get; set; } = new AuthorizationContext();
        public string EntityId { get; set; } = string.Empty;
    }
}