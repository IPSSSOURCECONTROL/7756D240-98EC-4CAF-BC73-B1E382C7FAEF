using System;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Product
{
    public class ProductService : ApplicationServiceBase<ProductResponse, IProductRepository>,
        IProductService
    {
        private readonly IDomainFactory<Domain.Product.Product, ProductAm> _domainFactory;

        public ProductService(IDomainFactory<Domain.Product.Product, ProductAm> domainFactory)
        {
            _domainFactory = domainFactory;
        }

        [ServiceRequestMethod]
        public ProductResponse GetById(ProductServiceRequest request)
        {
            if (string.IsNullOrWhiteSpace((request.EntityId)))
            {
                this.Response.RegisterError(MessageFormatter.IsARequiredField(nameof(ProductServiceRequest.EntityId)));
                return this.Response;
            }

            Domain.Product.Product domainModel = this.Repository.GetById(request.EntityId);

            if (domainModel == null)
            {
                this.Response.RegisterError(MessageFormatter.RecordWithIdDoesNotExist(request.EntityId));
            }
            else
            {
                this.Response.Product = this._domainFactory.BuildApplicationModelType(domainModel);
                this.Response.RegisterSuccess();
            }

            return this.Response;

        }

        [ServiceRequestMethod]
        public ProductResponse GetAll(ProductServiceRequest request)
        {
            this.Response.ProductCollection = this._domainFactory.BuildApplicationModelTypes(this.Repository.GetAll());
            this.Response.RegisterSuccess();
            return this.Response;
        }

        [ServiceRequestMethod]
        public ProductResponse Add(ProductServiceRequest request)
        {
            if (request.Product == null)
            {
                this.Response.RegisterError(MessageFormatter.IsARequiredField(
                    nameof(ProductServiceRequest.Product)));
                return this.Response;
            }

            Domain.Product.Product product = this._domainFactory.BuildDomainEntityType(request.Product);

            this.Repository.Add(product);

            this.Response.RegisterSuccess(
                MessageFormatter.EntitySuccessfullyAdded<Domain.Business.Business>(product.Id));

            return this.Response;
        }

        [ServiceRequestMethod]
        public ProductResponse Update(ProductServiceRequest request)
        {
            if (request.Product == null)
            {
                this.Response.RegisterError(MessageFormatter.IsARequiredField(
                    nameof(ProductServiceRequest.Product)));
                return this.Response;
            }

            Domain.Product.Product product = this._domainFactory.BuildDomainEntityType(request.Product, false);

            this.Repository.Update(product);

            this.Response.RegisterSuccess(
                MessageFormatter.EntitySuccessfullyUpdated<Domain.Business.Business>(product.Id));

            return this.Response;
        }

        [ServiceRequestMethod]
        public ProductResponse Delete(ProductServiceRequest request)
        {
            if (request.Product == null)
            {
                this.Response.RegisterError(MessageFormatter.IsARequiredField(
                    nameof(ProductServiceRequest.Product)));
                return this.Response;
            }

            Domain.Product.Product product = this._domainFactory.BuildDomainEntityType(request.Product);

            this.Repository.Delete(product);

            this.Response.RegisterSuccess(
                MessageFormatter.EntitySuccessfullyRemoved<Domain.Business.Business>(product.Id));

            return this.Response;
        }
    }
}