using KhanyisaIntel.Kbit.Framework.Infrustructure.Application.Model;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Security;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Application
{
    public class ServiceRequestBase<TApplicationModel> where TApplicationModel : ApplicationModelBase
    {
        public ServiceRequestBase()
        {
        }

        public TApplicationModel ApplicationModel { get; set; }
        public AuthorizationContext AuthorizationContext { get; set; } = new AuthorizationContext();
        public string EntityId { get; set; } = string.Empty;
    }
}