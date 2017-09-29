using System.Collections.Generic;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Factories.Business
{
    public class BusinessFactory: IDomainFactory<Domain.Business.Business, BusinessAm>
    {
        [ValidateMethodArguments]
        public Domain.Business.Business BuildDomainEntityType(BusinessAm applicationModel, bool isNew)
        {
            applicationModel.Validate();

            Address address = new Address(applicationModel.AddressLineOne, applicationModel.AddressLineTwo,
                applicationModel.Street, applicationModel.Suburb, applicationModel.TownOrCity,
                applicationModel.PostalCode);

            ContactDetails contactDetails = new ContactDetails(applicationModel.Email,
                applicationModel.TelephoneNumber, applicationModel.CellphoneNumber);

            BillingInformation billingInformation = new BillingInformation(applicationModel.Bank,
                applicationModel.AccountNumber, applicationModel.BranchCode,
                applicationModel.Reference);

            if (isNew)
            {
                Domain.Business.Business business = new Domain.Business.Business(applicationModel.Name, address,
                contactDetails, billingInformation);
                
                if(applicationModel.IsActive)
                    business.Activate();
                else
                    business.Deactivate();

                return business;
            }
            else
            {
                Domain.Business.Business business = new Domain.Business.Business(applicationModel.Name, address,
                    contactDetails, billingInformation);
                business.Id = applicationModel.Id;

                if (applicationModel.IsActive)
                    business.Activate();
                else
                    business.Deactivate();

                return business;
            }
        }

        [ValidateMethodArguments]
        public BusinessAm BuildApplicationModelType(Domain.Business.Business domainEntity)
        {
            BusinessAm applicationModel = new BusinessAm();
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
            applicationModel.IsActive = domainEntity.IsActive;

            return applicationModel;
        }

        [ValidateMethodArguments]
        public IEnumerable<BusinessAm> BuildApplicationModelTypes(IEnumerable<Domain.Business.Business> domainEntities)
        {
            List<BusinessAm> applicationModels = new List<BusinessAm>();

            foreach (Domain.Business.Business business in domainEntities)
            {
                applicationModels.Add(this.BuildApplicationModelType(business));
            }

            return applicationModels;
        }
    }
}
