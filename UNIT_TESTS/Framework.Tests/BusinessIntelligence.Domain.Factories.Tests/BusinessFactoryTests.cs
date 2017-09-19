using System;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Business;
using KhanyisaIntel.Kbit.Framework.Infrustructure.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KhanyisaIntel.Kbit.Framework.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;
using KhanyisaIntel.Kbit.Framework.Security.Application.Models;
using KhanyisaIntel.Kbit.Framework.Security.Domain.User;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Exception;

namespace KhanyisaIntel.Kbit.Framework.Tests.BusinessIntelligence.Domain.Factories.Tests
{
    [TestClass]
    public class BusinessFactoryTests
    {
        private IAutoResolver _iocContainer;

        public BusinessFactoryTests()
        {
            this._iocContainer = new IocContainer();
        }

        [TestMethod]
        public void TestCanResolveDependency()
        {
            IDomainFactory<Business, BusinessAm> target = this._iocContainer.Resolve<IDomainFactory<Business, BusinessAm>>();

            Assert.IsNotNull(target);
            Assert.IsInstanceOfType(target, typeof(IDomainFactory<Business, BusinessAm>));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestBuildDomainEntityType_MustFailGivenNullArgument()
        {
            IDomainFactory<Business, BusinessAm> target = this._iocContainer.Resolve<IDomainFactory<Business, BusinessAm>>();
            target.BuildDomainEntityType(null);
        }

        [TestMethod]
        [ExpectedException(typeof(KbitRequiredFieldValidationException))]
        public void TestBuildDomainEntityType_MustFailGivenInvalidApplicationModelProperty()
        {
            IDomainFactory<Business, BusinessAm> target = this._iocContainer.Resolve<IDomainFactory<Business, BusinessAm>>();

            BusinessAm businessAm = new BusinessAm();
            //All properties are not set, it will fail.

            target.BuildDomainEntityType(businessAm);
        }

        [TestMethod]
        [ExpectedException(typeof(KbitRequiredFieldValidationException))]
        public void TestBuildDomainEntityType_MustFailGivenNullAddressLineOne()
        {
            IDomainFactory<Business, BusinessAm> target = this._iocContainer.Resolve<IDomainFactory<Business, BusinessAm>>();

            BusinessAm businessAm = new BusinessAm
            {
                AddressLineOne = null,
                AddressLineTwo = "AddressLineTwo",
                Street = "StreetName",
                Suburb = "SuburbName",
                TownOrCity = "TownOrCity",
                PostalCode = "PostalCode",
                Email = "steveland@khanyisaintel.co.za",
                TelephoneNumber = "0745555555",
                CellphoneNumber = "0111115623",
                Bank = "BankName",
                AccountNumber = "123456789",
                BranchCode = "123123",
                Reference = "Reference1",
                Name = "Steveland",
            };

            target.BuildDomainEntityType(businessAm);
        }

        [TestMethod]
        public void TestBuildDomainEntityType_MustSuccessfullyCreateDomainEntity()
        {
            IDomainFactory<Business, BusinessAm> target = this._iocContainer.Resolve<IDomainFactory<Business, BusinessAm>>();

            BusinessAm businessAm = new BusinessAm
            {
                AddressLineOne = "AddressLineOne",
                AddressLineTwo = "AddressLineTwo",
                Street = "StreetName",
                Suburb = "SuburbName",
                TownOrCity = "TownOrCity",
                PostalCode = "PostalCode",
                Email = "steveland@khanyisaintel.co.za",
                TelephoneNumber = "0745555555",
                CellphoneNumber = "0111115623",
                Bank = "BankName",
                AccountNumber = "123456789",
                BranchCode = "123123",
                Reference = "Reference1",
                Name = "Steveland",
            };

            Business business = target.BuildDomainEntityType(businessAm);
            
            Assert.IsNotNull(business);
            Assert.AreEqual(businessAm.Name, business.Name);
            Assert.AreEqual(businessAm.AddressLineOne, business.Address.AddressLineOne);
            Assert.AreEqual(businessAm.AddressLineTwo, business.Address.AddressLineTwo);
            Assert.AreEqual(businessAm.Street, business.Address.Street);
            Assert.AreEqual(businessAm.Suburb, business.Address.Suburb);
            Assert.AreEqual(businessAm.TownOrCity, business.Address.TownOrCity);
            Assert.AreEqual(businessAm.PostalCode, business.Address.PostalCode);
            Assert.AreEqual(businessAm.Email, business.ContactDetails.Email);
            Assert.AreEqual(businessAm.TelephoneNumber, business.ContactDetails.TelephoneNumber);
            Assert.AreEqual(businessAm.CellphoneNumber, business.ContactDetails.CellphoneNumber);
            Assert.AreEqual(businessAm.Bank, business.BillingInformation.Bank);
            Assert.AreEqual(businessAm.AccountNumber, business.BillingInformation.AccountNumber);
            Assert.AreEqual(businessAm.BranchCode, business.BillingInformation.BranchCode);
            Assert.AreEqual(businessAm.Reference, business.BillingInformation.Reference);
        }

        [TestMethod]
        public void TestBuildDomainEntityType_MustSuccessfullyCreateApplicationModelFromDomainEntity()
        {
            IDomainFactory<Business, BusinessAm> target = this._iocContainer.Resolve<IDomainFactory<Business, BusinessAm>>();

            BusinessAm customerAm = new BusinessAm()
            {
                AddressLineOne = "hfajdfhn",
                AddressLineTwo = "ajghdf",
                Street = "akgljsl",
                Suburb = "Suburb",
                TownOrCity = "TownOrCity",
                PostalCode = "dfgdskl",
                Email = "Email@gmail.com",
                TelephoneNumber = "0745551632",
                CellphoneNumber = "0112134567",
                Bank = "Bank",
                AccountNumber = "123456",
                BranchCode = "BranchCode",
                Reference = "Reference",
                Name = "Name"
            };

            Business business = target.BuildDomainEntityType(customerAm);

            Assert.IsNotNull(customerAm);
            Assert.AreEqual(customerAm.AddressLineOne, business.Address.AddressLineOne);
            Assert.AreEqual(customerAm.AddressLineTwo, business.Address.AddressLineTwo);
            Assert.AreEqual(customerAm.Street, business.Address.Street);
            Assert.AreEqual(customerAm.Suburb, business.Address.Suburb);
            Assert.AreEqual(customerAm.TownOrCity, business.Address.TownOrCity);
            Assert.AreEqual(customerAm.PostalCode, business.Address.PostalCode);
            Assert.AreEqual(customerAm.Email, business.ContactDetails.Email);
            Assert.AreEqual(customerAm.TelephoneNumber, business.ContactDetails.TelephoneNumber);
            Assert.AreEqual(customerAm.CellphoneNumber, business.ContactDetails.CellphoneNumber);
            Assert.AreEqual(customerAm.Bank, business.BillingInformation.Bank);
            Assert.AreEqual(customerAm.AccountNumber, business.BillingInformation.AccountNumber);
            Assert.AreEqual(customerAm.BranchCode, business.BillingInformation.BranchCode);
            Assert.AreEqual(customerAm.Reference, business.BillingInformation.Reference);
            Assert.AreEqual(customerAm.Name, business.Name);

            BusinessAm applicationModel = target.BuildApplicationModelType(business);

            Assert.IsNotNull(applicationModel);
            Assert.IsNotNull(customerAm);
            Assert.AreEqual(customerAm.AddressLineOne, applicationModel.AddressLineOne);
            Assert.AreEqual(customerAm.AddressLineTwo, applicationModel.AddressLineTwo);
            Assert.AreEqual(customerAm.Street, applicationModel.Street);
            Assert.AreEqual(customerAm.Suburb, applicationModel.Suburb);
            Assert.AreEqual(customerAm.TownOrCity, applicationModel.TownOrCity);
            Assert.AreEqual(customerAm.PostalCode, applicationModel.PostalCode);
            Assert.AreEqual(customerAm.Email, applicationModel.Email);
            Assert.AreEqual(customerAm.TelephoneNumber, applicationModel.TelephoneNumber);
            Assert.AreEqual(customerAm.CellphoneNumber, applicationModel.CellphoneNumber);
            Assert.AreEqual(customerAm.Bank, applicationModel.Bank);
            Assert.AreEqual(customerAm.AccountNumber, applicationModel.AccountNumber);
            Assert.AreEqual(customerAm.BranchCode, applicationModel.BranchCode);
            Assert.AreEqual(customerAm.Reference, applicationModel.Reference);
            Assert.AreEqual(customerAm.Name, applicationModel.Name);
        }
    }
}
