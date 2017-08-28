using System.Web.Http;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Business;

namespace KhanyisaIntel.Kbit.Framework.Mvc.Controllers.Business
{
    [Authorize]
    public class BusinessController: 
        KbitApiControllerBase<BusinessAm,IBusinessService,
            BusinessServiceRequest,BusinessResponse>
    {
        public BusinessController(IBusinessService applicationService) : base(applicationService)
        {
        }
    }
}
