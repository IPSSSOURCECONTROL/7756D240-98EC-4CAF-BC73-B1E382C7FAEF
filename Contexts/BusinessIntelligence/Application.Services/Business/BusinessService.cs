using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Business
{
    public class BusinessService : ApplicationServiceBase<
        BusinessServiceRequest,
        BusinessResponse,
        IBusinessRepository,
        Domain.Business.Business,
        BusinessAm>, IBusinessService
    {
        [AuthorizeAction]
        [ServiceRequestMethod]
        public override BusinessResponse GetAll(BusinessServiceRequest request)
        {
            this.Response.ApplicationModels = this.DomainFactory.BuildApplicationModelTypes(
                this.Repository.GetAll());
            this.Response.RegisterSuccess();
            return this.Response;
        }
    }
}