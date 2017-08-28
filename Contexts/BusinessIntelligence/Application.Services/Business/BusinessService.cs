using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Business
{
    public class BusinessService : ApplicationServiceBase2<
        BusinessServiceRequest,
        BusinessResponse,
        IBusinessRepository,
        Domain.Business.Business,
        BusinessAm>, IBusinessService
    {
    }
    //public class BusinessService : ApplicationServiceBase<BusinessResponse,
    //    IBusinessRepository,
    //    Domain.Business.Business,
    //    BusinessAm>, IBusinessService
    //{
    //    [ServiceRequestMethod]
    //    public BusinessResponse GetById(BusinessServiceRequest request)
    //    {
    //        if (string.IsNullOrWhiteSpace(request.EntityId))
    //        {
    //            this.Response.RegisterError(MessageFormatter.IsARequiredField(nameof(BusinessServiceRequest.EntityId)));
    //            return this.Response;
    //        }

    //        Domain.Business.Business domainModel = this.Repository.GetById(request.EntityId);

    //        if (domainModel == null)
    //        {
    //            this.Response.RegisterError(MessageFormatter.RecordWithIdDoesNotExist(request.EntityId));
    //        }
    //        else
    //        {
    //            this.Response.Business = this.DomainFactory.BuildApplicationModelType(domainModel);
    //            this.Response.RegisterSuccess();
    //        }

    //        return this.Response;
    //    }

    //    [ServiceRequestMethod]
    //    public BusinessResponse GetAll(BusinessServiceRequest request)
    //    {
    //        this.Response.BusinessCollection = this.DomainFactory.BuildApplicationModelTypes(this.Repository.GetAll());
    //        this.Response.RegisterSuccess();
    //        return this.Response;
    //    }

    //    [ServiceRequestMethod]
    //    public BusinessResponse Add(BusinessServiceRequest request)
    //    {
    //        if (request.Business == null)
    //        {
    //            this.Response.RegisterError(MessageFormatter.IsARequiredField(
    //                nameof(BusinessServiceRequest.Business)));
    //            return this.Response;
    //        }

    //        Domain.Business.Business business = this.DomainFactory.BuildDomainEntityType(request.Business);

    //        this.Repository.Add(business);

    //        this.Response.RegisterSuccess(
    //            MessageFormatter.EntitySuccessfullyAdded<Domain.Business.Business>(business.Id));

    //        return this.Response;
    //    }

    //    [ServiceRequestMethod]
    //    public BusinessResponse Update(BusinessServiceRequest request)
    //    {
    //        if (request.Business == null)
    //        {
    //            this.Response.RegisterError(MessageFormatter.IsARequiredField(
    //                nameof(BusinessServiceRequest.Business)));
    //            return this.Response;
    //        }

    //        Domain.Business.Business business = this.DomainFactory.BuildDomainEntityType(request.Business, false);

    //        this.Repository.Update(business);

    //        this.Response.RegisterSuccess(
    //            MessageFormatter.EntitySuccessfullyUpdated<Domain.Business.Business>(business.Id));

    //        return this.Response;
    //    }

    //    [ServiceRequestMethod]
    //    public BusinessResponse Delete(BusinessServiceRequest request)
    //    {
    //        if (request.Business == null)
    //        {
    //            this.Response.RegisterError(MessageFormatter.IsARequiredField(
    //                nameof(BusinessServiceRequest.Business)));
    //            return this.Response;
    //        }

    //        Domain.Business.Business business = this.DomainFactory.BuildDomainEntityType(request.Business);

    //        this.Repository.Delete(business);

    //        this.Response.RegisterSuccess(
    //            MessageFormatter.EntitySuccessfullyRemoved<Domain.Business.Business>(business.Id));

    //        return this.Response;
    //    }
    //}
}