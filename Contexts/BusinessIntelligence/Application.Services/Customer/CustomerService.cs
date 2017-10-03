using System;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Customer;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.ProductListing;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Interfaces;
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
    }
}
