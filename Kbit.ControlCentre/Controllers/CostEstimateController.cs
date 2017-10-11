using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Kbit.ControlCentre.Controllers.Globals;
using Kbit.ControlCentre.Cors;
using Kbit.ControlCentre.Models;
using Kbit.ControlCentre.Session;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Business;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Customer;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Product;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities;

namespace Kbit.ControlCentre.Controllers
{
    public class CostEstimateController : BaseController<ICustomerService, CustomerServiceRequest,
            CustomerResponse, CustomerAm, ViewEditCustomerVm>
    {
        private readonly IBusinessService _businessService;
        private readonly IProductService _productService;
        private readonly IUniqueValueGenerator _uniqueValueGenerator;

        public CostEstimateController(IBusinessService businessService, 
            IProductService productService, IUniqueValueGenerator uniqueValueGenerator)
        {
            this._businessService = businessService;
            this._productService = productService;
            this._uniqueValueGenerator = uniqueValueGenerator;
        }

        public ActionResult Index()
        {
            return this.View("Index");
        }

        public ActionResult EditCustomerCostEstimates()
        {
            throw new NotImplementedException();
        }

        public ActionResult LoadEditCustomerCostEstimatesView(string id)
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
                return this.View("EditCustomerCostEstimates", viewModel);
            }

            viewModel = Mapper.Map<ViewEditCustomerVm>(this.ServiceResponse.ApplicationModel);
            viewModel.ServiceResult = true;

            return this.View("EditCustomerCostEstimates", viewModel);
        }

        public ActionResult LoadCreateCustomerCostEstimatesView(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return this.RedirectToAction("Index");

            this.ServiceRequest.EntityId = id;

            this.ServiceResponse = this.ApplicationService.GetById(this.ServiceRequest);

            ViewEditCostEstimateVm viewModel = new ViewEditCostEstimateVm();

            if (this.ServiceResponse.ServiceResult != ServiceResult.Success)
            {
                viewModel.ServiceResult = false;
                viewModel.Message = this.ServiceResponse.Message;
                return this.View("NewCustomerCostEstimate", viewModel);
            }

            viewModel.Customer = Mapper.Map<ViewEditCustomerVm>(this.ServiceResponse.ApplicationModel);
            viewModel.Business = Mapper.Map<ViewEditBusinessVm>(this.GetBusiness());
            viewModel.ProductListingNumber = $"CE{this._uniqueValueGenerator.GenerateUniqueValue()}";
            Mapper.Map(this.GetBusinessProducts(), viewModel.Products);
            viewModel.ServiceResult = true;

            return this.View("NewCustomerCostEstimate", viewModel);
        }

        public ActionResult LoadCustomerCostEstimatesView(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return this.RedirectToAction("Index");

            this.ServiceRequest.EntityId = id;

            this.ServiceResponse = this.ApplicationService.GetCostEstimates(this.ServiceRequest);

            IndexCustomerProductListingVm viewModel = new IndexCustomerProductListingVm();

            if (this.ServiceResponse.ServiceResult != ServiceResult.Success)
            {
                viewModel.ServiceResult = false;
                viewModel.Message = this.ServiceResponse.Message;
                return this.View("CustomerCostEstimates", viewModel);
            }

            viewModel.CustomerId = id;
            viewModel.CustomName = this.ServiceResponse.ApplicationModel.Name;
            Mapper.Map(this.ServiceResponse.ProductListings, viewModel.ProductListings);
            viewModel.ServiceResult = true;

            return this.View("CustomerCostEstimates", viewModel);
        }

        [HttpGet]
        [JsonNetFilter]
        public ActionResult GetCustomerCostEstimates(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return this.RedirectToAction("Index");

            this.ServiceRequest.EntityId = id;

            this.ServiceResponse = this.ApplicationService.GetCostEstimates(this.ServiceRequest);

            IndexCustomerProductListingVm viewModel = new IndexCustomerProductListingVm();

            List<ProductListingVm> models = new List<ProductListingVm>();

            if (this.ServiceResponse.ServiceResult != ServiceResult.Success)
            {
                viewModel.ServiceResult = false;
                viewModel.Message = this.ServiceResponse.Message;
                return this.Json(viewModel, JsonRequestBehavior.AllowGet);
            }

            Mapper.Map(this.ServiceResponse.ProductListings, viewModel.ProductListings);

            return this.Json(viewModel.ProductListings, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<ProductAm> GetBusinessProducts()
        {
            ProductServiceRequest request = new ProductServiceRequest();
            request.AuthorizationContext.UserId = (string)this.Session[SessionConstants.CurrentUserName];
            request.AuthorizationContext.BusinessId = (string)this.Session[SessionConstants.CurrentUserBusinessId];

            ProductResponse response = this._productService.GetAll(request);

            return response.ApplicationModels;
        }

        private BusinessAm GetBusiness()
        {
            BusinessServiceRequest request = new BusinessServiceRequest();
            request.AuthorizationContext.UserId = (string)this.Session[SessionConstants.CurrentUserName];
            request.AuthorizationContext.BusinessId = (string) this.Session[SessionConstants.CurrentUserBusinessId];
            request.EntityId = (string) this.Session[SessionConstants.CurrentUserBusinessId];

            BusinessResponse response = this._businessService.GetById(request);

            return response.ApplicationModel;
        }

        [HttpGet]
        [JsonNetFilter]
        public ActionResult GetProductById(string id)
        {
            ProductServiceRequest request = new ProductServiceRequest();
            request.AuthorizationContext.UserId = (string)this.Session[SessionConstants.CurrentUserName];
            request.AuthorizationContext.BusinessId = (string)this.Session[SessionConstants.CurrentUserBusinessId];
            request.EntityId = id;

            ProductResponse response = this._productService.GetById(request);
            ViewEditProductVm viewModel = new ViewEditProductVm();

            if (response.ServiceResult != ServiceResult.Success)
            {

                viewModel.ServiceResult = false;
                viewModel.Message = response.Message;
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            Mapper.Map(response.ApplicationModel, viewModel);
            viewModel.ServiceResult = true;
            return this.Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [JsonNetFilter]
        public ActionResult CalculateProductListItemTotalAmount(ProductLineItemVm productDetails)
        {
            ProductServiceRequest request = new ProductServiceRequest();
            request.AuthorizationContext.UserId = (string)this.Session[SessionConstants.CurrentUserName];
            request.AuthorizationContext.BusinessId = (string)this.Session[SessionConstants.CurrentUserBusinessId];
            request.EntityId = productDetails.ProductId;
            request.Discount = productDetails.Discount;
            request.Quantity = productDetails.Quantity;

            ProductResponse response = this._productService.CalculateProductTotals(request);
            CalculateProductListItemTotalAmountVm viewModel = new CalculateProductListItemTotalAmountVm();

            if (response.ServiceResult != ServiceResult.Success)
            {

                viewModel.ServiceResult = false;
                viewModel.Message = response.Message;
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            viewModel.TotalAmount = response.TotalAmount;
            viewModel.ServiceResult = true;
            return this.Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [JsonNetFilter]
        public ActionResult CalculateProductListSubTotalAmounts(List<ProductLineItemVm> productDetailsArray)
        {         
            ProductListingFooterTotalsVm viewModel = new ProductListingFooterTotalsVm();

            if (productDetailsArray == null)
            {
                viewModel.ServiceResult = true;
                return this.Json(viewModel, JsonRequestBehavior.AllowGet);
            }

            foreach (ProductLineItemVm productDetails in productDetailsArray)
            {
                ProductServiceRequest request = new ProductServiceRequest();
                request.AuthorizationContext.UserId = (string)this.Session[SessionConstants.CurrentUserName];
                request.AuthorizationContext.BusinessId = (string)this.Session[SessionConstants.CurrentUserBusinessId];
                request.EntityId = productDetails.ProductId;
                request.Discount = productDetails.Discount;
                request.Quantity = productDetails.Quantity;
                 
                ProductResponse response = this._productService.CalculateProductTotals(request);
                if (response.ServiceResult != ServiceResult.Success)
                {

                    viewModel.ServiceResult = false;
                    viewModel.Message = response.Message;
                    return this.Json(response, JsonRequestBehavior.AllowGet);
                }
                viewModel.SubTotal +=response.TotalAmount;
                viewModel.TotalDiscount += response.TotalDiscount;
                viewModel.TotalVat += response.TotalVat;
            }

            viewModel.ServiceResult = true;
            return this.Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [JsonNetFilter]
        public ActionResult SaveProductListing(SaveProductListingVm viewModel)
        {
            if (viewModel.ProductDetailsArray == null || string.IsNullOrWhiteSpace(viewModel.Id))
            {
                viewModel.Message = $"{ControllerText.ServerError} Invalid Operation.";
                viewModel.ServiceResult = true;
                return this.Json(viewModel, JsonRequestBehavior.AllowGet);
            }

            Mapper.Map(viewModel.ProductDetailsArray, this.ServiceRequest.ProductListingItems);

            this.ServiceRequest.EntityId = viewModel.Id;
            this.ServiceRequest.ProductListingUniqueIdentifier =
                viewModel.ProductListingUniqueIdentifier;

            this.ServiceResponse = this.ApplicationService.AddCostEstimate(this.ServiceRequest);

            if (this.ServiceResponse.ServiceResult != ServiceResult.Success)
            {
                viewModel.ServiceResult = false;
                viewModel.Message = this.ServiceResponse.Message;
                return this.Json(viewModel, JsonRequestBehavior.AllowGet);
            }

            viewModel.ServiceResult = true;
            viewModel.Message = this.ServiceResponse.Message;
            return this.Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteCustomerEstimate(string id, string customerId)
        {
            this.ServiceRequest.EntityId = customerId;
            this.ServiceRequest.ProductListingUniqueIdentifier = id;

            this.ServiceResponse = this.ApplicationService.DeleteProductListing(this.ServiceRequest);

            if (this.ServiceResponse.ServiceResult != ServiceResult.Success)
            {
                return this.Json(this.ServiceResponse);
            }

            return this.Json(this.ServiceResponse);
        }
    }
}