using System.Collections.Generic;
using System.Reflection;
using Architecture.Tests.BusinessIntelligence.Application.Models;
using Architecture.Tests.BusinessIntelligence.Domain.Customer;
using Architecture.Tests.Infrustructure.Validation;

namespace Architecture.Tests.BusinessIntelligence.Domain.Factories.Customer
{
    public class CustomerFactory
    {
        public static Domain.Customer.Customer BuildNewCustomer(CustomerAm model)
        {
            Validator.CheckReferenceTypeForNull(model, nameof(model),
                MethodBase.GetCurrentMethod(), typeof(CustomerFactory));

            model.Validate();

            Address address = new Address(model.AddressLineOne, model.AddressLineTwo,
                model.Street, model.Suburb, model.TownOrCity,
                model.PostalCode);

            ContactDetails contactDetails = new ContactDetails(model.Email,
                model.TelephoneNumber, model.CellphoneNumber);

            BillingInformation billingInformation = new BillingInformation(model.Bank,
                model.AccountNumber, model.BranchCode, model.Reference);

            Representative representative = new Representative(model.RepresentativeId,
                model.RepresentativeName, model.RepresentativeCode);

            return new Domain.Customer.Customer(model.Name, address,contactDetails, representative, billingInformation);
        }

        public static CustomerAm BuildApplicationModel(Domain.Customer.Customer customer)
        {
            CustomerAm customerAm = new CustomerAm();
            customerAm.Id = customer.Id;
            customerAm.Name = customer.Name;
            customerAm.AddressLineOne = customer.Address.AddressLineOne;
            customerAm.AddressLineTwo = customer.Address.AddressLineTwo;
            customerAm.Street = customer.Address.Street;
            customerAm.Suburb = customer.Address.Suburb;
            customerAm.TownOrCity = customer.Address.TownOrCity;
            customerAm.PostalCode = customer.Address.PostalCode;

            customerAm.TelephoneNumber = customer.ContactDetails.TelephoneNumber;
            customerAm.CellphoneNumber = customer.ContactDetails.CellphoneNumber;
            customerAm.Email = customer.ContactDetails.Email;

            customerAm.Bank = customer.BillingInformation.Bank;
            customerAm.BranchCode = customer.BillingInformation.BranchCode;
            customerAm.AccountNumber = customer.BillingInformation.AccountNumber;
            customerAm.Reference = customer.BillingInformation.Reference;

            customerAm.RepresentativeId = customer.Representative.Id;
            customerAm.RepresentativeName = customer.Representative.Name;
            customerAm.RepresentativeCode = customer.Representative.Code;

            return customerAm;
        }

        public static IEnumerable<CustomerAm> BuildApplicationModels(IEnumerable<Domain.Customer.Customer> customers)
        {
            List<CustomerAm> customerAms = new List<CustomerAm>();

            foreach (Domain.Customer.Customer customer in customers)
            {
                customerAms.Add(BuildApplicationModel(customer));
            }

            return customerAms;
        }
    }
}
