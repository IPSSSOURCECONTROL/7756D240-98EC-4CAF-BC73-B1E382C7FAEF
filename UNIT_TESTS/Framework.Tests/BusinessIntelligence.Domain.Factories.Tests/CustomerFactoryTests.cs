using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KhanyisaIntel.Kbit.Framework.Infrustructure.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Customer;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Exception;

namespace KhanyisaIntel.Kbit.Framework.Tests.BusinessIntelligence.Domain.Factories.Tests
{
    [TestClass]
    public class CustomerFactoryTests
    {
        private IAutoResolver _iocContainer;

        public CustomerFactoryTests()
        {
            this._iocContainer = new IocContainer();

        }
        [TestMethod]
        public void TestCanResolveDependency()
        {
            IDomainFactory<Customer, CustomerAm> target = this._iocContainer.Resolve<IDomainFactory<Customer, CustomerAm>>();

            Assert.IsNotNull(target);
            Assert.IsInstanceOfType(target, typeof(IDomainFactory<Customer, CustomerAm>));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestBuildDomainEntityType_MustFailGivenNullArgument()
        {
            IDomainFactory<Customer, CustomerAm> target = this._iocContainer.Resolve<IDomainFactory<Customer, CustomerAm>>();
            target.BuildDomainEntityType(null);
        }

        [TestMethod]
        [ExpectedException(typeof(KbitRequiredFieldValidationException))]
        public void TestBuildDomainEntityType_MustFailGivenInvalidApplicationModelProperty()
        {
            IDomainFactory<Customer, CustomerAm> target = this._iocContainer.Resolve<IDomainFactory<Customer, CustomerAm>>();

            CustomerAm customerAm = new CustomerAm();
            //All properties are not set, it will fail.

            target.BuildDomainEntityType(customerAm);
        }

        [TestMethod]
        [ExpectedException(typeof(KbitRequiredFieldValidationException))]
        public void TestBuildDomainEntityType_MustFailGivenNullAccountNumber()
        {
            IDomainFactory<Customer, CustomerAm> target = this._iocContainer.Resolve<IDomainFactory<Customer, CustomerAm>>();

            CustomerAm userAm = new CustomerAm
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
                AccountNumber = null,
                BranchCode = "BranchCode",
                Reference = "Reference",
                Name = "Name",
                RepresentativeId = "Rep1",
                BusinessId = "BusinessId1"
            };

            target.BuildDomainEntityType(userAm);
        }

        [TestMethod]
        public void TestBuildDomainEntityType_MustSuccessfullyCreateDomainEntity()
        {
            IDomainFactory<Customer, CustomerAm> target = this._iocContainer.Resolve<IDomainFactory<Customer, CustomerAm>>();

            CustomerAm customerAm = new CustomerAm
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
                Name = "Name",
                RepresentativeId = "59a827c0e3496b4f08e3f12b",
                BusinessId = "BusinessId1"
            };

            Customer customer = target.BuildDomainEntityType(customerAm);

            Assert.IsNotNull(customerAm);
            Assert.AreEqual(customerAm.AddressLineOne, customer.Address.AddressLineOne);
            Assert.AreEqual(customerAm.AddressLineTwo, customer.Address.AddressLineTwo);
            Assert.AreEqual(customerAm.Street, customer.Address.Street);
            Assert.AreEqual(customerAm.Suburb, customer.Address.Suburb);
            Assert.AreEqual(customerAm.TownOrCity, customer.Address.TownOrCity);
            Assert.AreEqual(customerAm.PostalCode, customer.Address.PostalCode);
            Assert.AreEqual(customerAm.Email, customer.ContactDetails.Email);
            Assert.AreEqual(customerAm.TelephoneNumber, customer.ContactDetails.TelephoneNumber);
            Assert.AreEqual(customerAm.CellphoneNumber, customer.ContactDetails.CellphoneNumber);
            Assert.AreEqual(customerAm.Bank, customer.BillingInformation.Bank);
            Assert.AreEqual(customerAm.AccountNumber, customer.BillingInformation.AccountNumber);
            Assert.AreEqual(customerAm.BranchCode, customer.BillingInformation.BranchCode);
            Assert.AreEqual(customerAm.Reference, customer.BillingInformation.Reference);
            Assert.AreEqual(customerAm.Name, customer.Name);
            Assert.AreEqual(customerAm.BusinessId, customer.BusinessId);
        }

        [TestMethod]
        public void TestBuildDomainEntityType_MustSuccessfullyCreateApplicationModelFromDomainEntity()
        {
            IDomainFactory<Customer, CustomerAm> target = this._iocContainer.Resolve<IDomainFactory<Customer, CustomerAm>>();

            CustomerAm customerAm = new CustomerAm
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
                Name = "Name",
                RepresentativeId = "59a82a58e349754f08f51eba",
                BusinessId = "BusinessId1"
            };

            Customer customer = target.BuildDomainEntityType(customerAm);
            customer.AssignRepresentative(new Representative("asdasdasd", "asdasdasd", "codeasdsd"));

            Assert.IsNotNull(customerAm);
            Assert.AreEqual(customerAm.AddressLineOne, customer.Address.AddressLineOne);
            Assert.AreEqual(customerAm.AddressLineTwo, customer.Address.AddressLineTwo);
            Assert.AreEqual(customerAm.Street, customer.Address.Street);
            Assert.AreEqual(customerAm.Suburb, customer.Address.Suburb);
            Assert.AreEqual(customerAm.TownOrCity, customer.Address.TownOrCity);
            Assert.AreEqual(customerAm.PostalCode, customer.Address.PostalCode);
            Assert.AreEqual(customerAm.Email, customer.ContactDetails.Email);
            Assert.AreEqual(customerAm.TelephoneNumber, customer.ContactDetails.TelephoneNumber);
            Assert.AreEqual(customerAm.CellphoneNumber, customer.ContactDetails.CellphoneNumber);
            Assert.AreEqual(customerAm.Bank, customer.BillingInformation.Bank);
            Assert.AreEqual(customerAm.AccountNumber, customer.BillingInformation.AccountNumber);
            Assert.AreEqual(customerAm.BranchCode, customer.BillingInformation.BranchCode);
            Assert.AreEqual(customerAm.Reference, customer.BillingInformation.Reference);
            Assert.AreEqual(customerAm.Name, customer.Name);
            Assert.AreEqual(customerAm.BusinessId, customer.BusinessId);

            CustomerAm applicationModel = target.BuildApplicationModelType(customer);

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
            Assert.AreEqual(customerAm.BusinessId, applicationModel.BusinessId);
        }
    }
}
