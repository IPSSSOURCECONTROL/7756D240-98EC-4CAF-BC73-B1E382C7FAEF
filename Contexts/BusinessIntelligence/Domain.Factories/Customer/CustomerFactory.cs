using System.Collections.Generic;
using System.Reflection;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Customer;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Validation;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Factories.Customer
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

            return new Domain.Customer.Customer(model.Name, address,contactDetails, representative, billingInformation, model.BusinessId);
        }

        public static CustomerAm BuildApplicationModel(Domain.Customer.Customer customer)
        {
            CustomerAm applicationModel = new CustomerAm();
            applicationModel.Id = customer.Id;
            applicationModel.Name = customer.Name;
            applicationModel.AddressLineOne = customer.Address.AddressLineOne;
            applicationModel.AddressLineTwo = customer.Address.AddressLineTwo;
            applicationModel.Street = customer.Address.Street;
            applicationModel.Suburb = customer.Address.Suburb;
            applicationModel.TownOrCity = customer.Address.TownOrCity;
            applicationModel.PostalCode = customer.Address.PostalCode;

            applicationModel.TelephoneNumber = customer.ContactDetails.TelephoneNumber;
            applicationModel.CellphoneNumber = customer.ContactDetails.CellphoneNumber;
            applicationModel.Email = customer.ContactDetails.Email;

            applicationModel.Bank = customer.BillingInformation.Bank;
            applicationModel.BranchCode = customer.BillingInformation.BranchCode;
            applicationModel.AccountNumber = customer.BillingInformation.AccountNumber;
            applicationModel.Reference = customer.BillingInformation.Reference;

            applicationModel.RepresentativeId = customer.Representative.Id;
            applicationModel.RepresentativeName = customer.Representative.Name;
            applicationModel.RepresentativeCode = customer.Representative.Code;
            applicationModel.BusinessId = customer.BusinessId;

            return applicationModel;
        }

        public static IEnumerable<CustomerAm> BuildApplicationModels(IEnumerable<Domain.Customer.Customer> customers)
        {
            List<CustomerAm> applicationModels = new List<CustomerAm>();

            foreach (Domain.Customer.Customer customer in customers)
            {
                applicationModels.Add(BuildApplicationModel(customer));
            }

            return applicationModels;
        }
    }
}
