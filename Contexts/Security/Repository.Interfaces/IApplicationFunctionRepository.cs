using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.Security.Domain.ApplicationFunction;

namespace KhanyisaIntel.Kbit.Framework.Security.Repository.Interfaces
{
    public interface IApplicationFunctionRepository: IBasicRepository<ApplicationFunction>, ISecurityContextAvailable
    {
    }
}
