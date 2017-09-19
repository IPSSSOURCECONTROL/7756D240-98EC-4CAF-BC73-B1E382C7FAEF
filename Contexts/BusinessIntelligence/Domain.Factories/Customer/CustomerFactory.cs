using System.Collections.Generic;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Factories.Customer
{
    public class CustomerFactory: IDomainFactory<Domain.Customer.Customer, CustomerAm>
    {
        [ValidateMethodArguments]
        public Domain.Customer.Customer BuildDomainEntityType(CustomerAm applicationModel, bool isNew = true)
        {
            applicationModel.Validate();

            Address address = new Address(applicationModel.AddressLineOne, 
                applicationModel.AddressLineTwo,
                applicationModel.Street, applicationModel.Suburb, 
                applicationModel.TownOrCity,
                applicationModel.PostalCode);

            ContactDetails contactDetails = new ContactDetails(applicationModel.Email,
                applicationModel.TelephoneNumber, applicationModel.CellphoneNumber);

            BillingInformation billingInformation = new BillingInformation(applicationModel.Bank,
                applicationModel.AccountNumber, 
                applicationModel.BranchCode, applicationModel.Reference);

            Domain.Customer.Customer customer=  new Domain.Customer.Customer(
                applicationModel.Name, 
                address, contactDetails, 
                billingInformation, applicationModel.BusinessId);

            if (!isNew)
            {
                customer.Id = applicationModel.Id;
            }

            return customer;
        }

        public CustomerAm BuildApplicationModelType(Domain.Customer.Customer domainEntity)
        {
            CustomerAm applicationModel = new CustomerAm();
            applicationModel.Id = domainEntity.Id;
            applicationModel.Name = domainEntity.Name;
            applicationModel.AddressLineOne = domainEntity.Address.AddressLineOne;
            applicationModel.AddressLineTwo = domainEntity.Address.AddressLineTwo;
            applicationModel.Street = domainEntity.Address.Street;
            applicationModel.Suburb = domainEntity.Address.Suburb;
            applicationModel.TownOrCity = domainEntity.Address.TownOrCity;
            applicationModel.PostalCode = domainEntity.Address.PostalCode;

            applicationModel.TelephoneNumber = domainEntity.ContactDetails.TelephoneNumber;
            applicationModel.CellphoneNumber = domainEntity.ContactDetails.CellphoneNumber;
            applicationModel.Email = domainEntity.ContactDetails.Email;

            applicationModel.Bank = domainEntity.BillingInformation.Bank;
            applicationModel.BranchCode = domainEntity.BillingInformation.BranchCode;
            applicationModel.AccountNumber = domainEntity.BillingInformation.AccountNumber;
            applicationModel.Reference = domainEntity.BillingInformation.Reference;

            applicationModel.RepresentativeId = domainEntity.Representative.Id;
            applicationModel.BusinessId = domainEntity.BusinessId;

            return applicationModel;
        }

        public IEnumerable<CustomerAm> BuildApplicationModelTypes(IEnumerable<Domain.Customer.Customer> domainEntities)
        {
            List<CustomerAm> applicationModels = new List<CustomerAm>();

            foreach (Domain.Customer.Customer customer in domainEntities)
            {
                applicationModels.Add(this.BuildApplicationModelType(customer));
            }

            return applicationModels;
        }
    }
}
