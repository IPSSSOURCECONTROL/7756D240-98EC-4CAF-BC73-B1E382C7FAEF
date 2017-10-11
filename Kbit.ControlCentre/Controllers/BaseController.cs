using System.Web.Mvc;
using Kbit.ControlCentre.Models;
using Kbit.ControlCentre.Session;
using Kbit.ControlCentre.ToastrWrapper;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application.Model;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Logging;

namespace Kbit.ControlCentre.Controllers
{
    public abstract class BaseController<
        TApplicationService, 
        TServiceRequest, 
        TServiceResponse,
        TApplicationModel,
        TViewlModel> : Controller 
        where TApplicationService : IApplicationService<TServiceRequest, TServiceResponse>
        where TServiceRequest : ServiceRequestBase<TApplicationModel>, new()
        where TServiceResponse: ServiceResponseBase<TApplicationModel>, new ()
        where TApplicationModel: ApplicationModelBase, new()
        where TViewlModel: ViewModelBase, new ()
    {
        private TServiceRequest _serviceRequest = new TServiceRequest();

        [MandatoryInjection]
        public TApplicationService ApplicationService { get; set; }
        [MandatoryInjection]
        public ILoggingType Logger { get; set; }
        public TServiceResponse ServiceResponse { get; set; } = new TServiceResponse();
        public TViewlModel ControllerViewlModel { get; set; }=new TViewlModel();
        public TServiceRequest ServiceRequest
        {
            get
            {
                this._serviceRequest.AuthorizationContext.UserId = this.User.Identity.Name;
                this._serviceRequest.AuthorizationContext.BusinessId =(string)
                    this.Session[SessionConstants.CurrentUserBusinessId];
                return this._serviceRequest;
            }
        }

        public Toastr Toastr { get; set; } = new Toastr();

        public ToastMessage AddToastMessage(string title, string message, ToastType toastType)
        {
            return this.Toastr.AddToastMessage(title, message, toastType);
        }
    }
}