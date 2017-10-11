using System;
using System.Collections.Generic;
using System.Linq;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Customer;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.ProductListing;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Customer
{
    public class CustomerService: ApplicationServiceBase<
        CustomerServiceRequest,CustomerResponse, 
        ICustomerRepository,
        Domain.Customer.Customer,
        CustomerAm>, ICustomerService
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly IProductRepository _productRepository;

        public CustomerService(IBusinessRepository businessRepository, IProductRepository productRepository)
        {
            this._businessRepository = businessRepository;
            this._productRepository = productRepository;
        }

        [AuthorizeAction]
        [ServiceRequestMethod]
        public override CustomerResponse Add(CustomerServiceRequest request)
        {
            if (request.ApplicationModel == null)
            {
                this.Response.RegisterError(MessageFormatter.IsARequiredField(
                    MessageFormatter.NormalizeApplicationModelName(typeof(CustomerAm))));
                return this.Response;
            }

            if (string.IsNullOrWhiteSpace(request.ApplicationModel.RepresentativeId))
            {
                this.Response.RegisterError(MessageFormatter.IsARequiredField(
                    nameof(request.ApplicationModel.RepresentativeId)));
                return this.Response;
            }

            Representative representative = this.Repository.GetRepresentativeById(
                request.ApplicationModel.RepresentativeId);

            Domain.Customer.Customer domainEntityType = this.DomainFactory.BuildDomainEntityType(request.ApplicationModel);
            domainEntityType.AssignRepresentative(representative);

            this.Repository.Add(domainEntityType);

            this.Response.RegisterSuccess(
                MessageFormatter.EntitySuccessfullyAdded<Domain.Customer.Customer>(domainEntityType.Id));

            return this.Response;
        }

        [AuthorizeAction]
        [ServiceRequestMethod]
        public override CustomerResponse Update(CustomerServiceRequest request)
        {
            if (request.ApplicationModel == null)
            {
                this.Response.RegisterError(MessageFormatter.IsARequiredField(
                    MessageFormatter.NormalizeApplicationModelName(typeof(CustomerAm))));
                return this.Response;
            }

            Representative representative = this.Repository.GetRepresentativeById(
                request.ApplicationModel.RepresentativeId);

            Domain.Customer.Customer domainEntityType = this.DomainFactory.BuildDomainEntityType(request.ApplicationModel, false);
            domainEntityType.AssignRepresentative(representative);

            this.Repository.Update(domainEntityType);

            this.Response.RegisterSuccess(
                MessageFormatter.EntitySuccessfullyUpdated<Domain.Customer.Customer>(domainEntityType.Id));

            return this.Response;
        }

        [AuthorizeAction]
        [ServiceRequestMethod]
        public CustomerResponse AddCostEstimate(CustomerServiceRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.EntityId))
            {
                this.Response.RegisterError(MessageFormatter.IsARequiredField(nameof(request.EntityId)));
                return this.Response;
            }

            if (request.ProductListingItems == null)
            {
                this.Response.RegisterError(MessageFormatter.IsARequiredField(nameof(request.ProductListingItems)));
                return this.Response;
            }

            Domain.Customer.Customer customer = this.Repository.GetById(request.EntityId);
            Domain.Business.Business business = this._businessRepository.GetById(request.AuthorizationContext.BusinessId);

            if (customer == null)
            {
                this.Response.RegisterError(MessageFormatter.TargetObjectWithIdDoesNotExist(
                    nameof(Domain.Customer.Customer),
                    request.EntityId));
                return this.Response;
            }

            if (business == null)
            {
                this.Response.RegisterError(MessageFormatter.TargetObjectWithIdDoesNotExist(
                    nameof(Domain.Business.Business),
                    request.EntityId));
                return this.Response;
            }

            CostEstimateListing costEstimateListing = new CostEstimateListing(business);
            costEstimateListing.AssignUniqueIdentifier(request.ProductListingUniqueIdentifier);

            foreach (ProductListingItemAm requestProductListingItem in request.ProductListingItems)
            {
                Domain.Product.Product product = this._productRepository.GetById(requestProductListingItem.ProductId);

                if (product == null)
                {
                    this.Response.RegisterError(MessageFormatter.TargetObjectWithIdDoesNotExist(nameof(Domain.Product.Product),
                        requestProductListingItem.ProductId));
                    return this.Response;
                }

                ProductListingItem productListingItem = new ProductListingItem(product);
                productListingItem.CalculateAmount(requestProductListingItem.Quantity, 
                    requestProductListingItem.Discount);
                productListingItem.CalculateTotalDiscount(requestProductListingItem.Quantity,
                    requestProductListingItem.Discount);
                productListingItem.CalculateTotalVat(requestProductListingItem.Quantity,
                    requestProductListingItem.Discount);

                costEstimateListing.AddProductListingItem(productListingItem);
            }

            customer.AddProductListing(costEstimateListing);

            this.Repository.Update(customer);

            this.Response.RegisterSuccess($"Cost Estimate with ID {request.ProductListingUniqueIdentifier} successfully " +
                                          $"created for customer '{customer.Name}'");

            return this.Response;
        }

        [AuthorizeAction]
        [ServiceRequestMethod]
        public CustomerResponse GetCostEstimates(CustomerServiceRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.EntityId))
            {
                this.Response.RegisterError(MessageFormatter.IsARequiredField(nameof(request.EntityId)));
                return this.Response;
            }

            Domain.Customer.Customer customer = this.Repository.GetById(request.EntityId);
            Domain.Business.Business business = this._businessRepository.GetById(request.AuthorizationContext.BusinessId);

            if (customer == null)
            {
                this.Response.RegisterError(MessageFormatter.TargetObjectWithIdDoesNotExist(
                    nameof(Domain.Customer.Customer),
                    request.EntityId));
                return this.Response;
            }

            if (business == null)
            {
                this.Response.RegisterError(MessageFormatter.TargetObjectWithIdDoesNotExist(
                    nameof(Domain.Business.Business),
                    request.EntityId));
                return this.Response;
            }

            this.Response.ApplicationModel = this.DomainFactory.BuildApplicationModelType(customer);
            this.Response.ProductListings = BuildCustomerCostEstimates(customer, business);
            this.Response.RegisterSuccess();

            return this.Response;
        }

        [AuthorizeAction]
        [ServiceRequestMethod]
        public CustomerResponse DeleteProductListing(CustomerServiceRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.EntityId))
            {
                this.Response.RegisterError(MessageFormatter.ServerError);
                this.Logger.Error("Null customer id.");
                return this.Response;
            }

            if (string.IsNullOrWhiteSpace(request.ProductListingUniqueIdentifier))
            {
                this.Response.RegisterError(MessageFormatter.ServerError);
                this.Logger.Error($"Null {nameof(request.ProductListingUniqueIdentifier)}");
                return this.Response;
            }

            Domain.Customer.Customer customer = this.Repository.GetById(request.EntityId);

            if (customer == null)
            {
                this.Response.RegisterError(MessageFormatter.TargetObjectWithIdDoesNotExist(
                    nameof(Domain.Customer.Customer),
                    request.EntityId));
                return this.Response;
            }

            customer.DeleteProductListing(request.ProductListingUniqueIdentifier);

            this.Repository.Update(customer);
            this.Response.RegisterSuccess("Cost Estimate successfully removed.");

            return this.Response;
        }

        private static List<ProductListingAm> BuildCustomerCostEstimates(Domain.Customer.Customer customer, 
            Domain.Business.Business business)
        {
            List<ProductListingAm> productListingAms = new List<ProductListingAm>();

            foreach (Domain.ProductListing.ProductListing productListing in
                customer.ProductListings.Where(x => x.GetType() == typeof(CostEstimateListing)))
            {
                ProductListingAm productListingAm = new ProductListingAm();
                productListingAm.CustomerId = customer.Id;
                productListingAm.BusinessId = business.Id;
                productListingAm.DateTime = productListing.DateTime;
                productListingAm.ProductListingUniqueIdentifier = productListing.ProductListingUniqueIdentifier;

                List<ProductListingItemAm> productListingItemAms = new List<ProductListingItemAm>();

                foreach (ProductListingItem productListingItem in productListing.ProductListingItems)
                {
                    ProductListingItemAm productListingItemAm = new ProductListingItemAm();
                    productListingItemAm.Discount = productListingItem.TotalDiscount;
                    productListingItemAm.Quantity = productListingItem.Quantity;
                    productListingItemAm.Subtotal = productListingItem.TotalAmount;
                    productListingItemAm.ProductId = productListingItem.Product.Id;

                    productListingItemAms.Add(productListingItemAm);
                }

                productListingAm.ProductListingItems = productListingItemAms;
                productListingAms.Add(productListingAm);
            }

            return productListingAms;
        }

        private static List<ProductListingAm> BuildCustomerInvoices(Domain.Customer.Customer customer,
            Domain.Business.Business business)
        {
            List<ProductListingAm> productListingAms = new List<ProductListingAm>();

            foreach (Domain.ProductListing.ProductListing productListing in
                customer.ProductListings.Where(x => x.GetType() == typeof(InvoiceListing)))
            {
                ProductListingAm productListingAm = new ProductListingAm();
                productListingAm.CustomerId = customer.Id;
                productListingAm.BusinessId = business.Id;
                productListingAm.DateTime = productListing.DateTime;
                productListingAm.ProductListingUniqueIdentifier = productListing.ProductListingUniqueIdentifier;

                List<ProductListingItemAm> productListingItemAms = new List<ProductListingItemAm>();

                foreach (ProductListingItem productListingItem in productListing.ProductListingItems)
                {
                    ProductListingItemAm productListingItemAm = new ProductListingItemAm();
                    productListingItemAm.Discount = productListingItem.TotalDiscount;
                    productListingItemAm.Quantity = productListingItem.Quantity;
                    productListingItemAm.Subtotal = productListingItem.TotalAmount;
                    productListingItemAm.ProductId = productListingItem.Product.Id;

                    productListingItemAms.Add(productListingItemAm);
                }

                productListingAm.ProductListingItems = productListingItemAms;
                productListingAms.Add(productListingAm);
            }

            return productListingAms;
        }

        private static List<ProductListingAm> BuildCustomerCreditListings(Domain.Customer.Customer customer,
            Domain.Business.Business business)
        {
            List<ProductListingAm> productListingAms = new List<ProductListingAm>();

            foreach (Domain.ProductListing.ProductListing productListing in
                customer.ProductListings.Where(x => x.GetType() == typeof(CreditListing)))
            {
                ProductListingAm productListingAm = new ProductListingAm();
                productListingAm.CustomerId = customer.Id;
                productListingAm.BusinessId = business.Id;
                productListingAm.DateTime = productListing.DateTime;
                productListingAm.ProductListingUniqueIdentifier = productListing.ProductListingUniqueIdentifier;

                List<ProductListingItemAm> productListingItemAms = new List<ProductListingItemAm>();

                foreach (ProductListingItem productListingItem in productListing.ProductListingItems)
                {
                    ProductListingItemAm productListingItemAm = new ProductListingItemAm();
                    productListingItemAm.Discount = productListingItem.TotalDiscount;
                    productListingItemAm.Quantity = productListingItem.Quantity;
                    productListingItemAm.Subtotal = productListingItem.TotalAmount;
                    productListingItemAm.ProductId = productListingItem.Product.Id;

                    productListingItemAms.Add(productListingItemAm);
                }

                productListingAm.ProductListingItems = productListingItemAms;
                productListingAms.Add(productListingAm);
            }

            return productListingAms;
        }
        }
    }
