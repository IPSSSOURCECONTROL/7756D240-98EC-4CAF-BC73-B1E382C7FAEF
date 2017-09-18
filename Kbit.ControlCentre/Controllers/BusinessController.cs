using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Kbit.ControlCentre.Cors;
using Kbit.ControlCentre.Models;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Business;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Security.Application.Models;
using KhanyisaIntel.Kbit.Framework.Security.Application.Services.User;

namespace Kbit.ControlCentre.Controllers
{
    public class UserPasswordConstants
    {
        public static string MaskPasswordValue { get { return "DefaultPasswordMask"; } }
    }

    [AllowCrossSite]
    public class BusinessController : 
        BaseController<IBusinessService, BusinessServiceRequest,
            BusinessResponse, BusinessAm, ViewEditBusinessVm>
    {
        private readonly IUserService _userService;

        public BusinessController(IUserService userService)
        {
            this._userService = userService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Edit(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return this.RedirectToAction("Index");

            this.ServiceRequest.EntityId = id;

            this.ServiceResponse = this.ApplicationService.GetById(this.ServiceRequest);

            if (this.ServiceResponse.ServiceResult != ServiceResult.Success)
            {
                this.ControllerViewlModel.ServiceResult = false;
                this.ControllerViewlModel.Message = this.ServiceResponse.Message;
                return this.View("Edit", this.ControllerViewlModel);
            }

            this.ControllerViewlModel = Mapper.Map<ViewEditBusinessVm>(this.ServiceResponse.ApplicationModel);
            this.ControllerViewlModel.ServiceResult = true;

            return this.View("Edit", this.ControllerViewlModel);
        }

        public ActionResult New()
        {
            return this.View("New", new ViewEditBusinessVm());
        }

        [JsonNetFilter]
        [HttpGet]
        public ActionResult GetAll()
        {
            this.ServiceResponse = this.ApplicationService.GetAll(this.ServiceRequest);

            IndexBusinessVm viewModel = new IndexBusinessVm();

            IEnumerable<ViewEditBusinessVm> models = new List<ViewEditBusinessVm>();

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
        public ActionResult Update(ViewEditBusinessVm viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Edit(viewModel.Id);
            }

            this.ServiceRequest.ApplicationModel = Mapper.Map<BusinessAm>(viewModel);

            this.ServiceResponse = this.ApplicationService.Update(this.ServiceRequest);

            if (this.ServiceResponse.ServiceResult != ServiceResult.Success)
            {
                this.ControllerViewlModel.ServiceResult = false;
                this.ControllerViewlModel.Message = this.ServiceResponse.Message;
                return this.View("Edit", this.ControllerViewlModel);
            }

            this.ControllerViewlModel.ServiceResult = true;
            this.ControllerViewlModel.Message = this.ServiceResponse.Message;

            return this.View("Edit", this.ControllerViewlModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateBusinessUser(ViewEditUserVm viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.EditBusinessUser(viewModel.Id);
            }

            if (viewModel.ConfirmationPassword != viewModel.Password)
            {
                viewModel.ServiceResult = false;
                viewModel.Message = "Password & Confirmation password do not match";
                this.BindListsToUserVm(viewModel);
                return this.View("EditBusinessUser", viewModel);
            }

            UserServiceRequest request = new UserServiceRequest();
            request.ApplicationModel = Mapper.Map<UserAm>(viewModel);
            request.BusinessId = viewModel.BusinessId;
            if (viewModel.Password != UserPasswordConstants.MaskPasswordValue)
            {
                request.UserPasswordChanged = true;
            }

            UserResponse response = this._userService.Update(request);

            if (response.ServiceResult != ServiceResult.Success)
            {
                viewModel.ServiceResult = false;
                viewModel.Message = response.Message;
                this.BindListsToUserVm(viewModel);
                return this.View("EditBusinessUser", viewModel);
            }

            viewModel.ServiceResult = true;
            viewModel.Message = response.Message;
            this.BindListsToUserVm(viewModel);
            return this.View("EditBusinessUser", viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ViewEditBusinessVm viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Edit(viewModel.Id);
            }

            this.ServiceRequest.ApplicationModel = Mapper.Map<BusinessAm>(viewModel);

            this.ServiceResponse = this.ApplicationService.Add(this.ServiceRequest);

            if (this.ServiceResponse.ServiceResult != ServiceResult.Success)
            {
                this.ControllerViewlModel.ServiceResult = false;
                this.ControllerViewlModel.Message = this.ServiceResponse.Message;
                return this.View("New", this.ControllerViewlModel);
            }

            this.ControllerViewlModel.ServiceResult = true;
            this.ControllerViewlModel.Message = this.ServiceResponse.Message;

            return this.View("New", this.ControllerViewlModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBusinessUser(ViewEditUserVm viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Edit(viewModel.Id);
            }
            viewModel.Id = string.Empty;

            UserServiceRequest request = new UserServiceRequest();

            request.ApplicationModel = Mapper.Map<UserAm>(viewModel);

            UserResponse response = this._userService.Add(request);

            if (response.ServiceResult != ServiceResult.Success)
            {
                viewModel.ServiceResult = false;
                viewModel.Message = response.Message;

                this.BindListsToUserVm(viewModel);

                return this.View("NewBusinessUser", viewModel);
            }

            viewModel.ServiceResult = true;
            viewModel.Message = response.Message;

            this.BindListsToUserVm(viewModel);

            return this.View("NewBusinessUser", viewModel);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
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

            UserServiceRequest userServiceRequest = new UserServiceRequest();
            userServiceRequest.AuthorizationContext.UserId = this.User.Identity.Name;
            userServiceRequest.AuthorizationContext.BusinessId = id;

            UserResponse userResponse = this._userService.Delete(userServiceRequest);

            if (userResponse.ServiceResult != ServiceResult.Success)
            {
                return this.Json(userResponse);
            }

            return this.Json(this.ServiceResponse);
        }

        [HttpPost]
        public ActionResult DeleteBusinessUser(string id)
        {
            UserServiceRequest request = new UserServiceRequest();
            request.AuthorizationContext.UserId = this.User.Identity.Name;
            request.EntityId = id;

            UserResponse response = this._userService.GetById(request);

            if (response.ServiceResult != ServiceResult.Success)
            {
                return this.Json(response);
            }

            request.ApplicationModel = response.ApplicationModel;
            response = this._userService.Delete(request);

            if (response.ServiceResult != ServiceResult.Success)
            {
                return this.Json(response);
            }

            return this.Json(response);
        }

        public ActionResult LoadBusinessUsersView(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return this.RedirectToAction("Index");

            BusinessUsersVm businessUsersVm = new BusinessUsersVm();
            businessUsersVm.BusinessId = id;

            return this.View("BusinesUsers", businessUsersVm);
        }

        [JsonNetFilter]
        [HttpGet]
        public ActionResult GetAllBusinessUsers(string id)
        {
            UserServiceRequest request = new UserServiceRequest();
            request.AuthorizationContext.BusinessId = id;
            request.AuthorizationContext.UserId = this.User.Identity.Name;

            UserResponse response = this._userService.GetAll(request);

            IndexUserVm viewModel = new IndexUserVm();

            IEnumerable<ViewEditUserVm> models = new List<ViewEditUserVm>();

            if (response.ServiceResult != ServiceResult.Success)
            {
                viewModel.ServiceResult = false;
                viewModel.Message = response.Message;

                models = viewModel.ViewModels;
                return this.Json(models, JsonRequestBehavior.AllowGet);
            }

            viewModel.ViewModels = Mapper.Map(response.ApplicationModels, viewModel.ViewModels);
            viewModel.ServiceResult = true;

            models = viewModel.ViewModels;

            return this.Json(models, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditBusinessUser(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return this.RedirectToAction("Index");

            UserServiceRequest request = new UserServiceRequest();
            request.EntityId = id;
            request.AuthorizationContext.UserId = this.User.Identity.Name;

            UserResponse response = this._userService.GetById(request);

            ViewEditUserVm viewModel = new ViewEditUserVm();

            if (response.ServiceResult != ServiceResult.Success)
            {
                viewModel.ServiceResult = false;
                viewModel.Message = response.Message;
                return this.View("EditBusinessUser", viewModel);
            }

            viewModel = Mapper.Map<ViewEditUserVm>(response.ApplicationModel);
            viewModel.ServiceResult = true;
            this.BindListsToUserVm(viewModel);

            return this.View("EditBusinessUser", viewModel);
        }

        public ActionResult LoadNewBusinessUserView(string id)
        {
            ViewEditUserVm viewModel = new ViewEditUserVm();
            viewModel.BusinessId = id;
            viewModel.ServiceResult = true;

            this.BindListsToUserVm(viewModel);

            return this.View("NewBusinessUser", viewModel);
        }

        private string GetUserByIdInternal(ViewEditUserVm viewModel)
        {
            UserServiceRequest request = new UserServiceRequest();
            request.AuthorizationContext.UserId = this.User.Identity.Name;
            request.AuthorizationContext.BusinessId = viewModel.BusinessId;

            return this._userService.GetById(request).ApplicationModel.Password;
        }

        private void BindListsToUserVm(ViewEditUserVm viewModel)
        {
            viewModel.Password = UserPasswordConstants.MaskPasswordValue;
            viewModel.ConfirmationPassword = UserPasswordConstants.MaskPasswordValue;
            viewModel.Roles = new List<string>
            {
                "Limited Role",
                "Superman Role",
                "Administrator Role",
                "Manager Role",
                "No Role"
            };
            viewModel.LicenseSpecifications = new List<string>
            {
                "Annual License Specification",
                "Demo License Specification",
                "Expired License Specification",
                "Monthly License Specification",
                "Perpertual License Specification"
            };
            viewModel.PasswordResetPolicies = new List<string>
            {
                "Daily Password Reset Policy",
                "Monthly Password Reset Policy",
                "Never Reset Password Reset Policy",
                "Weekly Password Reset Policy"
            };
            viewModel.AccountStatuss = new List<string>
            {
                "Active Account Status",
                "Blocked Account Status",
                "Inactive Account Status"
            };
        }
    }
}