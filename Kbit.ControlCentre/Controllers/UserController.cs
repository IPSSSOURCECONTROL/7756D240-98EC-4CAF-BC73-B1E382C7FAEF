using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Kbit.ControlCentre.Controllers.Extensions;
using Kbit.ControlCentre.Cors;
using Kbit.ControlCentre.Models;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Security.Application.Models;
using KhanyisaIntel.Kbit.Framework.Security.Application.Services.User;

namespace Kbit.ControlCentre.Controllers
{
    public class UserController : BaseController<IUserService, UserServiceRequest,
            UserResponse, UserAm, ViewEditUserVm>
    {
        public ActionResult Index()
        {
            return this.View("Index");
        }

        [JsonNetFilter]
        [HttpGet]
        public ActionResult GetAllBusinessUsers()
        {
            this.ServiceResponse = this.ApplicationService.GetAll(this.ServiceRequest);

            IndexUserVm viewModel = new IndexUserVm();

            IEnumerable<ViewEditUserVm> models = new List<ViewEditUserVm>();

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

        public ActionResult EditBusinessUser(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return this.RedirectToAction("Index");

            this.ServiceRequest.EntityId = id;

            this.ServiceResponse = this.ApplicationService.GetById(this.ServiceRequest);

            ViewEditUserVm viewModel = new ViewEditUserVm();

            if (this.ServiceResponse.ServiceResult != ServiceResult.Success)
            {
                viewModel.ServiceResult = false;
                viewModel.Message = this.ServiceResponse.Message;
                return this.View("Edit", viewModel);
            }

            viewModel = Mapper.Map<ViewEditUserVm>(this.ServiceResponse.ApplicationModel);
            viewModel.ServiceResult = true;
            viewModel.BindListsToUserVm();

            return this.View("Edit", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateUser(ViewEditUserVm viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.EditBusinessUser(viewModel.Id);
            }

            if (viewModel.ConfirmationPassword != viewModel.Password)
            {
                viewModel.ServiceResult = false;
                viewModel.Message = "Password & Confirmation password do not match";
                viewModel.BindListsToUserVm();
                return this.View("Edit", viewModel);
            }
            this.ServiceRequest.ApplicationModel = Mapper.Map<UserAm>(viewModel);
            this.ServiceRequest.BusinessId = viewModel.BusinessId;
            if (viewModel.Password != UserPasswordConstants.MaskPasswordValue)
            {
                this.ServiceRequest.UserPasswordChanged = true;
            }

            this.ServiceResponse = this.ApplicationService.Update(this.ServiceRequest);

            if (this.ServiceResponse.ServiceResult != ServiceResult.Success)
            {
                viewModel.ServiceResult = false;
                viewModel.Message = this.ServiceResponse.Message;
                viewModel.BindListsToUserVm();
                return this.View("Edit", viewModel);
            }

            viewModel.ServiceResult = true;
            viewModel.Message = this.ServiceResponse.Message;
            viewModel.BindListsToUserVm();
            return this.View("Edit", viewModel);
        }
    }
}