using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Business
{
    public class BusinessResponse : ServiceResponseBase<BusinessAm>
    {
        public BusinessResponse()
        {
        }

        public BusinessResponse(string message, ServiceResult serviceResult)
            :base(message, serviceResult)
        {

        }
    }
}