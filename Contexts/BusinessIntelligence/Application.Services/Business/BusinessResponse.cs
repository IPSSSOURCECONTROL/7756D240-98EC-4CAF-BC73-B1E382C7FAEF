using System.Collections.Generic;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Business
{
    public class BusinessResponse : ServiceResponseBase
    {
        public BusinessAm Business { get; set; }
        public IEnumerable<BusinessAm> BusinessCollection { get; set; }
    }
}