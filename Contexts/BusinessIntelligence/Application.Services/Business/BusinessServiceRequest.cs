using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Business
{
    public class BusinessServiceRequest : ServiceRequestBase
    {
        public BusinessAm Business { get; set; } = new BusinessAm();
    }
}