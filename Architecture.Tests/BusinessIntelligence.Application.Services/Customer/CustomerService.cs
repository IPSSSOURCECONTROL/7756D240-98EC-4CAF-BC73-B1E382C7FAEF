using Architecture.Tests.BusinessIntelligence.Domain.Customer;
using Architecture.Tests.BusinessIntelligence.Domain.Factories.Customer;
using Architecture.Tests.Infrustructure.AOP.Attributes;
using Architecture.Tests.Infrustructure.Application;
using Architecture.Tests.Infrustructure.Utilities;

namespace Architecture.Tests.BusinessIntelligence.Application.Services.Customer
{
    public class CustomerService: ApplicationServiceBase<CustomerResponse>, ICustomerService
    {
        private ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            this._customerRepository = customerRepository;
        }

        [ServiceRequestMethod]
        public CustomerResponse GetById(CustomerServiceRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.EntityId))
            {
                this.Response.RegisterError(MessageFormatter.IsARequiredField(nameof(CustomerServiceRequest.EntityId)));
                return this.Response;
            }

            Domain.Customer.Customer customer = this._customerRepository.GetById(request.EntityId);

            if (customer == null)
            {
                this.Response.RegisterError(MessageFormatter.RecordWithIdDoesNotExist(request.EntityId));
            }
            else
            {
                this.Response.CustomerAm = CustomerFactory.BuildApplicationModel(customer);
                this.Response.RegisterSuccess();
            }

            return this.Response;
        }

        [ServiceRequestMethod]
        public CustomerResponse GetAll(CustomerServiceRequest request)
        {
            this.Response.CustomerAms = CustomerFactory.BuildApplicationModels(this._customerRepository.GetAll());
            return this.Response;
        }

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

            this._customerRepository.Add(customer);

            this.Response.RegisterSuccess(
                MessageFormatter.EntitySuccessfullyAdded<Domain.Customer.Customer>(customer.Id));

            return this.Response;
        }

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

            this._customerRepository.Update(customer);

            this.Response.RegisterSuccess(MessageFormatter.EntitySuccessfullyAdded<Domain.Customer.Customer>(customer.Id));

            return this.Response;
        }

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

            this._customerRepository.Delete(customer);

            this.Response.RegisterSuccess(MessageFormatter.EntitySuccessfullyAdded<Domain.Customer.Customer>(customer.Id));

            return this.Response;
        }
    }
}
