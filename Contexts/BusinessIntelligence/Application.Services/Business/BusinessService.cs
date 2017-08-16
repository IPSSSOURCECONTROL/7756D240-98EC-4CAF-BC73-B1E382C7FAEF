using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Business
{
    public class BusinessService : ApplicationServiceBase<BusinessResponse, IBusinessRepository>, IBusinessService
    {
        private readonly IDomainFactory<Domain.Business.Business, BusinessAm> _domainFactory;

        public BusinessService(IDomainFactory<Domain.Business.Business, BusinessAm> domainFactory)
        {
            this._domainFactory = domainFactory;
        }

        [ServiceRequestMethod]
        public BusinessResponse GetById(BusinessServiceRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.EntityId))
            {
                this.Response.RegisterError(MessageFormatter.IsARequiredField(nameof(BusinessServiceRequest.EntityId)));
                return this.Response;
            }

            Domain.Business.Business domainModel = this.Repository.GetById(request.EntityId);

            if (domainModel == null)
            {
                this.Response.RegisterError(MessageFormatter.RecordWithIdDoesNotExist(request.EntityId));
            }
            else
            {
                this.Response.Business = this._domainFactory.BuildApplicationModelType(domainModel);
                this.Response.RegisterSuccess();
            }

            return this.Response;
        }

        [ServiceRequestMethod]
        public BusinessResponse GetAll(BusinessServiceRequest request)
        {
            this.Response.BusinessCollection = this._domainFactory.BuildApplicationModelTypes(this.Repository.GetAll());
            this.Response.RegisterSuccess();
            return this.Response;
        }

        [ServiceRequestMethod]
        public BusinessResponse Add(BusinessServiceRequest request)
        {
            if (request.Business == null)
            {
                this.Response.RegisterError(MessageFormatter.IsARequiredField(
                    nameof(BusinessServiceRequest.Business)));
                return this.Response;
            }

            Domain.Business.Business business = this._domainFactory.BuildDomainEntityType(request.Business);

            this.Repository.Add(business);

            this.Response.RegisterSuccess(
                MessageFormatter.EntitySuccessfullyAdded<Domain.Business.Business>(business.Id));

            return this.Response;
        }

        [ServiceRequestMethod]
        public BusinessResponse Update(BusinessServiceRequest request)
        {
            if (request.Business == null)
            {
                this.Response.RegisterError(MessageFormatter.IsARequiredField(
                    nameof(BusinessServiceRequest.Business)));
                return this.Response;
            }

            Domain.Business.Business business = this._domainFactory.BuildDomainEntityType(request.Business, false);

            this.Repository.Update(business);

            this.Response.RegisterSuccess(
                MessageFormatter.EntitySuccessfullyUpdated<Domain.Business.Business>(business.Id));

            return this.Response;
        }

        [ServiceRequestMethod]
        public BusinessResponse Delete(BusinessServiceRequest request)
        {
            if (request.Business == null)
            {
                this.Response.RegisterError(MessageFormatter.IsARequiredField(
                    nameof(BusinessServiceRequest.Business)));
                return this.Response;
            }

            Domain.Business.Business business = this._domainFactory.BuildDomainEntityType(request.Business);

            this.Repository.Delete(business);

            this.Response.RegisterSuccess(
                MessageFormatter.EntitySuccessfullyRemoved<Domain.Business.Business>(business.Id));

            return this.Response;
        }
    }
}