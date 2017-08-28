using System.Web.Http;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application.Model;

namespace KhanyisaIntel.Kbit.Framework.Mvc.Controllers
{
    /// <summary>
    /// <para>Base type for all <see cref="ApiController"/> types in 
    /// the KBIT Framework Stack. All <see cref="ApiController"/> types must inherit from this 
    /// base type. </para>
    /// <para>Implements generic functionality for all methods defined in the <see cref="IApplicationService{TServiceRequest,TServiceResponse}"/> 
    /// type. All methods of the <see cref="IApplicationService{TServiceRequest,TServiceResponse}"/> type are 
    /// implemented as virtual methods so that if custom implementation is needed, simply override the method 
    /// in the deriving type.  </para>
    /// </summary>
    /// <typeparam name="TApplicationModel"></typeparam>
    /// <typeparam name="TApplicationService"></typeparam>
    /// <typeparam name="TServiceRequest"></typeparam>
    /// <typeparam name="TServiceResponse"></typeparam>
    public abstract class KbitApiControllerBase<TApplicationModel, TApplicationService, TServiceRequest, TServiceResponse>
        : ApiController,IKbitApiController<TApplicationModel> 
        where TApplicationModel : ApplicationModelBase, new ()
        where TApplicationService : IApplicationService<TServiceRequest, TServiceResponse>
        where TServiceRequest : ServiceRequestBase<TApplicationModel>, new ()
        where TServiceResponse : ServiceResponseBase<TApplicationModel>
    {
        private readonly TApplicationService _applicationService;

        protected KbitApiControllerBase(TApplicationService applicationService)
        {
            this._applicationService = applicationService;
        }

        [HttpPost]
        public virtual IHttpActionResult Add(TApplicationModel applicationModel)
        {
            TServiceRequest request = new TServiceRequest();
            request.ApplicationModel = applicationModel;

            TServiceResponse response = this._applicationService.Add(request);

            if (response.ServiceResult != ServiceResult.Success)
                return this.BadRequest(response.Message);

            return this.Ok(response.Message);
        }

        [HttpDelete]
        public virtual IHttpActionResult Delete(TApplicationModel applicationModel)
        {
            TServiceRequest request = new TServiceRequest();
            request.ApplicationModel = applicationModel;

            TServiceResponse response = this._applicationService.Delete(request);

            if (response.ServiceResult != ServiceResult.Success)
                return this.BadRequest(response.Message);

            return this.Ok(response.Message);
        }

        [HttpGet]
        public virtual IHttpActionResult GetAll()
        {
            TServiceRequest request = new TServiceRequest();

            TServiceResponse response = this._applicationService.GetAll(request);

            if (response.ServiceResult != ServiceResult.Success)
                return this.BadRequest(response.Message);

            return this.Ok(response.ApplicationModels);
        }

        [HttpGet]
        public virtual IHttpActionResult GetById(string id)
        {
            TServiceRequest request = new TServiceRequest();
            request.EntityId = id;

            TServiceResponse response = this._applicationService.GetById(request);

            if (response.ServiceResult != ServiceResult.Success)
                return this.BadRequest(response.Message);

            return this.Ok(response.ApplicationModel);
        }

        [HttpPut]
        public virtual IHttpActionResult Update(TApplicationModel applicationModel)
        {
            TServiceRequest request = new TServiceRequest();
            request.ApplicationModel = applicationModel;

            TServiceResponse response = this._applicationService.Update(request);

            if (response.ServiceResult != ServiceResult.Success)
                return this.BadRequest(response.Message);

            return this.Ok(response.Message);
        }
    }
}