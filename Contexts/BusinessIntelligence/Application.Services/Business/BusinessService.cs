using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Business
{
    public class BusinessService : ApplicationServiceBase<
        BusinessServiceRequest,
        BusinessResponse,
        IBusinessRepository,
        Domain.Business.Business,
        BusinessAm>, IBusinessService
    {
    }
}