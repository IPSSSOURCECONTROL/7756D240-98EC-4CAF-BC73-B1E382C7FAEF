using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Kbit.ControlCentre.Cors;
using Kbit.ControlCentre.Models;
using Kbit.ControlCentre.Session;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Customer;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Security.Application.Models;
using KhanyisaIntel.Kbit.Framework.Security.Application.Services.User;

namespace Kbit.ControlCentre.Controllers
{
    public class CustomerController : BaseController<ICustomerService, CustomerServiceRequest,
            CustomerResponse, CustomerAm, ViewEditCustomerVm>
    {
        private readonly IUserService _userService;

        public CustomerController(IUserService userService)
        {
            this._userService = userService;
        }

        public ActionResult Index()
        {
            return this.View("Index");
        }

        [JsonNetFilter]
        [HttpGet]
        public ActionResult GetAllBusinessCustomers()
        {
            this.ServiceResponse = this.ApplicationService.GetAll(this.ServiceRequest);

            IndexCustomerVm viewModel = new IndexCustomerVm();

            IEnumerable<ViewEditCustomerVm> models = new List<ViewEditCustomerVm>();

            if (this.ServiceResponse.ServiceResult != ServiceResult.Success)
            {
                viewModel.ServiceResult = false;
                viewModel.Message = this.ServiceResponse.Message;

                models = viewModel.ViewModels;
                return this.Json(models, JsonRequestBehavior.AllowGet);
            }

            viewModel.ViewModels = Mapper.Map(this.ServiceResponse.ApplicationModels, viewModel.ViewModels);
            viewModel.ServiceResult = true;

            models = viewModel.ViewModels;

            return this.Json(models, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBusinessCustomer(ViewEditCustomerVm viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                viewModel.Users = this.GetBusinessUsers();
                return this.LoadNewCustomerView(viewModel.Id);
            }

            viewModel.Id = string.Empty;

            this.ServiceRequest.ApplicationModel = Mapper.Map<CustomerAm>(viewModel);
            this.ServiceRequest.ApplicationModel.BusinessId = (string)this.Session[SessionConstants.CurrentUserBusinessId];

            this.ServiceResponse = this.ApplicationService.Add(this.ServiceRequest);

            if (this.ServiceResponse.ServiceResult != ServiceResult.Success)
            {
                viewModel.ServiceResult = false;
                viewModel.Message = this.ServiceResponse.Message;
                viewModel.Users = this.GetBusinessUsers();

                return this.View("NewBusinessCustomer", viewModel);
            }

            viewModel.ServiceResult = true;
            viewModel.Message = this.ServiceResponse.Message;
            viewModel.Users = this.GetBusinessUsers();

            return this.View("NewBusinessCustomer", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCustomer(ViewEditCustomerVm viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.EditCustomer(viewModel.Id);
            }

            this.ServiceRequest.AuthorizationContext.UserId = this.User.Identity.Name;
            this.ServiceRequest.AuthorizationContext.BusinessId = (string)this.Session[SessionConstants.CurrentUserBusinessId];
            this.ServiceRequest.ApplicationModel = Mapper.Map<CustomerAm>(viewModel);
            this.ServiceRequest.ApplicationModel.BusinessId = this.ServiceRequest.AuthorizationContext.BusinessId;

            this.ServiceResponse = this.ApplicationService.Update(this.ServiceRequest);

            if (this.ServiceResponse.ServiceResult != ServiceResult.Success)
            {
                viewModel.ServiceResult = false;
                viewModel.Message = this.ServiceResponse.Message;
                viewModel.Users = this.GetBusinessUsers();
                return this.View("Edit", viewModel);
            }

            viewModel.ServiceResult = true;
            viewModel.Message = this.ServiceResponse.Message;
            viewModel.Users = this.GetBusinessUsers();
            return this.View("Edit", viewModel);
        }

        public ActionResult LoadNewCustomerView(string id)
        {
            ViewEditCustomerVm viewModel = new ViewEditCustomerVm();
            viewModel.BusinessId = id;
            viewModel.ServiceResult = true;

            viewModel.Users = this.GetBusinessUsers();

            return this.View("NewBusinessCustomer", viewModel);
        }

        public ActionResult EditCustomer(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return this.RedirectToAction("Index");

            this.ServiceRequest.EntityId = id;

            this.ServiceResponse = this.ApplicationService.GetById(this.ServiceRequest);

            ViewEditCustomerVm viewModel = new ViewEditCustomerVm();

            if (this.ServiceResponse.ServiceResult != ServiceResult.Success)
            {
                viewModel.ServiceResult = false;
                viewModel.Message = this.ServiceResponse.Message;
                return this.View("Edit", viewModel);
            }

            viewModel = Mapper.Map<ViewEditCustomerVm>(this.ServiceResponse.ApplicationModel);
            viewModel.ServiceResult = true;
            viewModel.Users = this.GetBusinessUsers();

            return this.View("Edit", viewModel);
        }

        [HttpPost]
        public ActionResult DeleteBusinessCustomer(string id)
        {
            this.ServiceRequest.EntityId = id;

            this.ServiceResponse = this.ApplicationService.GetById(this.ServiceRequest);

            if (this.ServiceResponse.ServiceResult != ServiceResult.Success)
            {
                return this.Json(this.ServiceResponse);
            }

            this.ServiceRequest.ApplicationModel = this.ServiceResponse.ApplicationModel;
            this.ServiceResponse = this.ApplicationService.Delete(this.ServiceRequest);

            if (this.ServiceResponse.ServiceResult != ServiceResult.Success)
            {
                return this.Json(this.ServiceResponse);
            }

            return this.Json(this.ServiceResponse);
        }

        private IEnumerable<ViewEditUserVm> GetBusinessUsers()
        {
            UserServiceRequest request = new UserServiceRequest();
            request.AuthorizationContext.BusinessId = (string)this.Session[SessionConstants.CurrentUserBusinessId];
            request.AuthorizationContext.UserId = this.User.Identity.Name;

            UserResponse response = this._userService.GetAll(request);

            IndexUserVm viewModel = new IndexUserVm();

            IEnumerable<ViewEditUserVm> models = new List<ViewEditUserVm>();

            if (response.ServiceResult != ServiceResult.Success)
            {
                viewModel.ServiceResult = false;
                viewModel.Message = response.Message;

                return viewModel.ViewModels;
            }

            viewModel.ViewModels = Mapper.Map(response.ApplicationModels, viewModel.ViewModels);
            viewModel.ServiceResult = true;

            models = viewModel.ViewModels;

            return models;
        }
    }
}