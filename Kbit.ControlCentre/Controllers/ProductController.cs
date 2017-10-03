using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Kbit.ControlCentre.Cors;
using Kbit.ControlCentre.Models;
using Kbit.ControlCentre.Session;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Product;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities;

namespace Kbit.ControlCentre.Controllers
{
    public class ProductController : BaseController<IProductService, ProductServiceRequest,
            ProductResponse, ProductAm, ViewEditProductVm>
    {
        private readonly IPolymorphicTyeNameProvider _polymorphicTyeNameProvider;

        public ProductController(IPolymorphicTyeNameProvider polymorphicTyeNameProvider)
        {
            this._polymorphicTyeNameProvider = polymorphicTyeNameProvider;
        }

        public ActionResult Index()
        {
            return this.View("Index");
        }

        [JsonNetFilter]
        [HttpGet]
        public ActionResult GetAllBusinessProducts()
        {
            this.ServiceResponse = this.ApplicationService.GetAll(this.ServiceRequest);

            IndexProductVm viewModel = new IndexProductVm();

            IEnumerable<ViewEditProductVm> models = new List<ViewEditProductVm>();

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
        public ActionResult AddBusinessProduct(ViewEditProductVm viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                viewModel.PricingClassifications =
                    this._polymorphicTyeNameProvider.GetPolymorphicTypeNamesForBaseType(nameof(viewModel.PricingClassification));
                viewModel.VatTypes =
                    this._polymorphicTyeNameProvider.GetPolymorphicTypeNamesForBaseType(nameof(viewModel.VatClassification));

                return this.LoadNewProductView(viewModel.Id);
            }

            viewModel.Id = string.Empty;

            this.ServiceRequest.ApplicationModel = Mapper.Map<ProductAm>(viewModel);
            this.ServiceRequest.ApplicationModel.BusinessId = (string)this.Session[SessionConstants.CurrentUserBusinessId];

            this.ServiceResponse = this.ApplicationService.Add(this.ServiceRequest);

            if (this.ServiceResponse.ServiceResult != ServiceResult.Success)
            {
                viewModel.ServiceResult = false;
                viewModel.Message = this.ServiceResponse.Message;
                viewModel.PricingClassifications =
                    this._polymorphicTyeNameProvider.GetPolymorphicTypeNamesForBaseType(nameof(viewModel.PricingClassification));
                viewModel.VatTypes =
                    this._polymorphicTyeNameProvider.GetPolymorphicTypeNamesForBaseType(nameof(viewModel.VatClassification));

                return this.View("NewBusinessProduct", viewModel);
            }

            viewModel.ServiceResult = true;
            viewModel.Message = this.ServiceResponse.Message;
            viewModel.PricingClassifications =
                this._polymorphicTyeNameProvider.GetPolymorphicTypeNamesForBaseType(nameof(viewModel.PricingClassification));
            viewModel.VatTypes =
                this._polymorphicTyeNameProvider.GetPolymorphicTypeNamesForBaseType(nameof(viewModel.VatClassification));

            return this.View("NewBusinessProduct", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProduct(ViewEditProductVm viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.EditProduct(viewModel.Id);
            }

            this.ServiceRequest.AuthorizationContext.UserId = this.User.Identity.Name;
            this.ServiceRequest.AuthorizationContext.BusinessId = (string)this.Session[SessionConstants.CurrentUserBusinessId];
            this.ServiceRequest.ApplicationModel = Mapper.Map<ProductAm>(viewModel);
            this.ServiceRequest.ApplicationModel.BusinessId = this.ServiceRequest.AuthorizationContext.BusinessId;

            this.ServiceResponse = this.ApplicationService.Update(this.ServiceRequest);

            if (this.ServiceResponse.ServiceResult != ServiceResult.Success)
            {
                viewModel.ServiceResult = false;
                viewModel.Message = this.ServiceResponse.Message;
                viewModel.PricingClassifications =
                    this._polymorphicTyeNameProvider.GetPolymorphicTypeNamesForBaseType(nameof(viewModel.PricingClassification));
                viewModel.VatTypes =
                    this._polymorphicTyeNameProvider.GetPolymorphicTypeNamesForBaseType(nameof(viewModel.VatClassification));

                return this.View("Edit", viewModel);
            }

            viewModel.ServiceResult = true;
            viewModel.Message = this.ServiceResponse.Message;
            viewModel.PricingClassifications =
                this._polymorphicTyeNameProvider.GetPolymorphicTypeNamesForBaseType(nameof(viewModel.PricingClassification));
            viewModel.VatTypes =
                this._polymorphicTyeNameProvider.GetPolymorphicTypeNamesForBaseType(nameof(viewModel.VatClassification));

            return this.View("Edit", viewModel);
        }

        public ActionResult LoadNewProductView(string id)
        {
            ViewEditProductVm viewModel = new ViewEditProductVm();
            viewModel.BusinessId = id;
            viewModel.ServiceResult = true;
            viewModel.PricingClassifications =
                this._polymorphicTyeNameProvider.GetPolymorphicTypeNamesForBaseType(nameof(viewModel.PricingClassification));
            viewModel.VatTypes =
                this._polymorphicTyeNameProvider.GetPolymorphicTypeNamesForBaseType(nameof(viewModel.VatClassification));

            return this.View("NewBusinessProduct", viewModel);
        }

        public ActionResult EditProduct(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return this.RedirectToAction("Index");

            this.ServiceRequest.EntityId = id;

            this.ServiceResponse = this.ApplicationService.GetById(this.ServiceRequest);

            ViewEditProductVm viewModel = new ViewEditProductVm();

            if (this.ServiceResponse.ServiceResult != ServiceResult.Success)
            {
                viewModel.ServiceResult = false;
                viewModel.Message = this.ServiceResponse.Message;
                return this.View("Edit", viewModel);
            }

            viewModel = Mapper.Map<ViewEditProductVm>(this.ServiceResponse.ApplicationModel);
            viewModel.ServiceResult = true;
            viewModel.VatTypes =
                this._polymorphicTyeNameProvider.GetPolymorphicTypeNamesForBaseType(nameof(viewModel.VatClassification));
            viewModel.PricingClassifications =
                this._polymorphicTyeNameProvider.GetPolymorphicTypeNamesForBaseType(nameof(viewModel.PricingClassification));

            return this.View("Edit", viewModel);
        }

        [HttpPost]
        public ActionResult DeleteBusinessProduct(string id)
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
    }
}