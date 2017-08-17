using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;

namespace KhanyisaIntel.Kbit.Framework.Security.Domain.AspNet
{
    public class AspNetPrincipleUser: AggregateRoot
    {
        protected override string GetTypeName()
        {
            return this.GetType().Name;
        }
    }
}
