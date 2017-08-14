using System.Web.Http;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Business;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;

namespace KhanyisaIntel.Kbit.Framework.Mvc.Controllers.Business
{
    public class BusinessController: ApiController
    {
        private readonly IBusinessService _service;

        public BusinessController(IBusinessService businessService)
        {
            this._service = businessService;
        }

        [HttpGet]
        public IHttpActionResult GetById(string id)
        {
            BusinessServiceRequest request = new BusinessServiceRequest();
            request.EntityId = id;

            BusinessResponse serviceResponse = this._service.GetById(request);

            switch (serviceResponse.ServiceResult)
            {
                case ServiceResult.Default:
                    return this.BadRequest(serviceResponse.Message);
                case ServiceResult.Success:
                    return this.Ok(serviceResponse.Business);
                case ServiceResult.Error:
                    return this.BadRequest(serviceResponse.Message);
                case ServiceResult.Exception:
                    return this.BadRequest(serviceResponse.Message);
                default:
                    return this.BadRequest("Undefined response type.");
            }
        }
    }
}
