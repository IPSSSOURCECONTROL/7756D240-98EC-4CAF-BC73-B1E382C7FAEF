using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Factories.Customer;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Customer
{
    public class CustomerService: ApplicationServiceBase<CustomerResponse, ICustomerRepository>, ICustomerService
    {
        [AuthorizeAction]
        [ServiceRequestMethod]
        public CustomerResponse GetById(CustomerServiceRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.EntityId))
            {
                this.Response.RegisterError(MessageFormatter.IsARequiredField(nameof(CustomerServiceRequest.EntityId)));
                return this.Response;
            }

            Domain.Customer.Customer domainModel = this.Repository.GetById(request.EntityId);

            if (domainModel == null)
            {
                this.Response.RegisterError(MessageFormatter.RecordWithIdDoesNotExist(request.EntityId));
            }
            else
            {
                this.Response.CustomerAm = CustomerFactory.BuildApplicationModel(domainModel);
                this.Response.RegisterSuccess();
            }

            return this.Response;
        }

        [AuthorizeAction]
        [ServiceRequestMethod]
        public CustomerResponse GetAll(CustomerServiceRequest request)
        {
            this.Response.CustomerAms = CustomerFactory.BuildApplicationModels(this.Repository.GetAll());
            this.Response.RegisterSuccess();
            return this.Response;
        }

        [AuthorizeAction]
        [ServiceRequestMethod]
        public CustomerResponse Add(CustomerServiceRequest request)
        {
            if (request.Customer == null)
            {
                this.Response.RegisterError(MessageFormatter.IsARequiredField(
                    nameof(CustomerServiceRequest.Customer)));
                return this.Response;
            }

            Domain.Customer.Customer customer = CustomerFactory.BuildNewCustomer(request.Customer);

            this.Repository.Add(customer);

            this.Response.RegisterSuccess(
                MessageFormatter.EntitySuccessfullyAdded<Domain.Customer.Customer>(customer.Id));

            return this.Response;
        }

        [AuthorizeAction]
        [ServiceRequestMethod]
        public CustomerResponse Update(CustomerServiceRequest request)
        {
            if (request.Customer == null)
            {
                this.Response.RegisterError(MessageFormatter.IsARequiredField(
                    nameof(CustomerServiceRequest.Customer)));
                return this.Response;
            }

            Domain.Customer.Customer customer = CustomerFactory.BuildNewCustomer(request.Customer);

            this.Repository.Update(customer);

            this.Response.RegisterSuccess(MessageFormatter.EntitySuccessfullyAdded<Domain.Customer.Customer>(customer.Id));

            return this.Response;
        }

        [AuthorizeAction]
        [ServiceRequestMethod]
        public CustomerResponse Delete(CustomerServiceRequest request)
        {
            if (request.Customer == null)
            {
                this.Response.RegisterError(MessageFormatter.IsARequiredField(
                    nameof(CustomerServiceRequest.Customer)));
                return this.Response;
            }

            Domain.Customer.Customer customer = CustomerFactory.BuildNewCustomer(request.Customer);

            this.Repository.Delete(customer);

            this.Response.RegisterSuccess(MessageFormatter.EntitySuccessfullyAdded<Domain.Customer.Customer>(customer.Id));

            return this.Response;
        }
    }
}
