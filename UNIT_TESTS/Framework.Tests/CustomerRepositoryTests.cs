using System;
using System.Linq;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Customer;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;
using KhanyisaIntel.Kbit.Framework.Tests.TEST_UTILS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KhanyisaIntel.Kbit.Framework.Tests
{
    [TestClass]
    public class CustomerRepositoryTests
    {
        private IAutoResolver _iocContainer;

        public CustomerRepositoryTests()
        {
            this._iocContainer = new IocContainer();

        }
        [TestMethod]
        public void TestCanResolveCustomerRepository()
        {
            ICustomerRepository repository = this._iocContainer.Resolve<ICustomerRepository>();

            Assert.IsNotNull(repository);
            Assert.IsInstanceOfType(repository, typeof(ICustomerRepository));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAdd_MustFailGivenNullEntity()
        {
            UnitTestContext.Initialize();

            ICustomerRepository repository = this._iocContainer.Resolve<ICustomerRepository>();
            repository.Add(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdate_MustFailGivenNullEntity()
        {
            UnitTestContext.Initialize();

            ICustomerRepository repository = this._iocContainer.Resolve<ICustomerRepository>();
            repository.Update(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDelete_MustFailGivenNullEntity()
        {
            UnitTestContext.Initialize();

            ICustomerRepository repository = this._iocContainer.Resolve<ICustomerRepository>();
            repository.Delete(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestIsExists_MustFailGivenNullEntity()
        {
            UnitTestContext.Initialize();

            ICustomerRepository repository = this._iocContainer.Resolve<ICustomerRepository>();
            repository.IsExist(null);
        }

        [TestMethod]
        public void TestAdd_MustAddCustomerSuccessfully()
        {
            UnitTestContext.Initialize();

            ICustomerRepository repository = this._iocContainer.Resolve<ICustomerRepository>();
            IDomainFactory<Customer, CustomerAm> domainFactory =
                this._iocContainer.Resolve<IDomainFactory<Customer, CustomerAm>>();
            Customer customer = domainFactory.BuildDomainEntityType(new CustomerAm()
            {
                AddressLineOne = "UNIT 1",
                AddressLineTwo = "OUT OF BOUNDS",
                Street = "Von Backstrom Boulevard",
                Suburb = "Silverlakes",
                TownOrCity = "Pretoria",
                PostalCode = "0081",
                Email = "goodwillgumede@yahoo.co.za",
                CellphoneNumber = "0721248899",
                Bank = "FNB",
                AccountNumber = "6211134445267",
                BranchCode = "206658",
                Reference = "aasasdasd",
                Name = "Spar Silver Oaks",
                RepresentativeId = "789",
                BusinessId = "TEST123"
            });

            repository.Add(customer);

            Customer savedCustomer = repository.GetById(customer.Id);

            Assert.IsNotNull(savedCustomer);
            Assert.AreEqual(customer.Name, savedCustomer.Name);

            Assert.AreEqual(customer.Representative.Id, savedCustomer.Representative.Id);
            Assert.AreEqual(customer.Representative.Name, savedCustomer.Representative.Name);
            Assert.AreEqual(customer.Representative.Code, savedCustomer.Representative.Code);

            Assert.AreEqual(customer.Address.AddressLineOne, savedCustomer.Address.AddressLineOne);
            Assert.AreEqual(customer.Address.AddressLineOne, savedCustomer.Address.AddressLineOne);
            Assert.AreEqual(customer.Address.AddressLineTwo, savedCustomer.Address.AddressLineTwo);
            Assert.AreEqual(customer.Address.Street, savedCustomer.Address.Street);
            Assert.AreEqual(customer.Address.Suburb, savedCustomer.Address.Suburb);
            Assert.AreEqual(customer.Address.TownOrCity, savedCustomer.Address.TownOrCity);
            Assert.AreEqual(customer.Address.PostalCode, savedCustomer.Address.PostalCode);

            Assert.AreEqual(customer.ContactDetails.Email, savedCustomer.ContactDetails.Email);
            Assert.AreEqual(customer.ContactDetails.TelephoneNumber, savedCustomer.ContactDetails.TelephoneNumber);
            Assert.AreEqual(customer.ContactDetails.CellphoneNumber, savedCustomer.ContactDetails.CellphoneNumber);

            Assert.AreEqual(customer.BillingInformation.Bank, savedCustomer.BillingInformation.Bank);
            Assert.AreEqual(customer.BillingInformation.AccountNumber, savedCustomer.BillingInformation.AccountNumber);
            Assert.AreEqual(customer.BillingInformation.BranchCode, savedCustomer.BillingInformation.BranchCode);
            Assert.AreEqual(customer.BillingInformation.Reference, savedCustomer.BillingInformation.Reference);
        }

        [TestMethod]
        public void TestUpdate_MustUpdateCustomerSuccessfully()
        {
            UnitTestContext.Initialize();

            ICustomerRepository repository = this._iocContainer.Resolve<ICustomerRepository>();
            IDomainFactory<Customer, CustomerAm> domainFactory =
                this._iocContainer.Resolve<IDomainFactory<Customer, CustomerAm>>();
            Customer customer = domainFactory.BuildDomainEntityType(new CustomerAm()
            {
                AddressLineOne = "UNIT 1",
                AddressLineTwo = "OUT OF BOUNDS",
                Street = "Von Backstrom Boulevard",
                Suburb = "Silverlakes",
                TownOrCity = "Pretoria",
                PostalCode = "0081",
                Email = "goodwillgumede@yahoo.co.za",
                CellphoneNumber = "0721248899",
                Bank = "FNB",
                AccountNumber = "6211134445267",
                BranchCode = "206658",
                Reference = "aasasdasd",
                Name = "Spar Silver Oaks",
                RepresentativeId = "789",
                BusinessId = "123"
            });

            repository.Add(customer);

            Customer savedCustomer = repository.GetById(customer.Id);
            savedCustomer.Name = "Mthokozisi Mtolo";
            savedCustomer.Address.Suburb = "Randburg";

            repository.Update(savedCustomer);

            customer = repository.GetById(savedCustomer.Id);

            Assert.IsNotNull(savedCustomer);
            Assert.AreEqual(customer.Name, savedCustomer.Name);
            Assert.AreEqual(customer.Representative.Id, savedCustomer.Representative.Id);
            Assert.AreEqual(customer.Representative.Name, savedCustomer.Representative.Name);
            Assert.AreEqual(customer.Representative.Code, savedCustomer.Representative.Code);
            Assert.AreEqual(customer.Address.AddressLineOne, savedCustomer.Address.AddressLineOne);
            Assert.AreEqual(customer.Address.AddressLineOne, savedCustomer.Address.AddressLineOne);
            Assert.AreEqual(customer.Address.AddressLineTwo, savedCustomer.Address.AddressLineTwo);
            Assert.AreEqual(customer.Address.Street, savedCustomer.Address.Street);
            Assert.AreEqual(customer.Address.Suburb, savedCustomer.Address.Suburb);
            Assert.AreEqual(customer.Address.TownOrCity, savedCustomer.Address.TownOrCity);
            Assert.AreEqual(customer.Address.PostalCode, savedCustomer.Address.PostalCode);
            Assert.AreEqual(customer.ContactDetails.Email, savedCustomer.ContactDetails.Email);
            Assert.AreEqual(customer.ContactDetails.TelephoneNumber, savedCustomer.ContactDetails.TelephoneNumber);
            Assert.AreEqual(customer.ContactDetails.CellphoneNumber, savedCustomer.ContactDetails.CellphoneNumber);
            Assert.AreEqual(customer.BillingInformation.Bank, savedCustomer.BillingInformation.Bank);
            Assert.AreEqual(customer.BillingInformation.AccountNumber, savedCustomer.BillingInformation.AccountNumber);
            Assert.AreEqual(customer.BillingInformation.BranchCode, savedCustomer.BillingInformation.BranchCode);
            Assert.AreEqual(customer.BillingInformation.Reference, savedCustomer.BillingInformation.Reference);
        }

        [TestMethod]
        public void TestDelete_MustDeleteCustomerSuccessfully()
        {
            UnitTestContext.Initialize();
            ICustomerRepository repository = this._iocContainer.Resolve<ICustomerRepository>();
            IDomainFactory<Customer, CustomerAm> domainFactory =
                this._iocContainer.Resolve<IDomainFactory<Customer, CustomerAm>>();
            Customer customer = domainFactory.BuildDomainEntityType(new CustomerAm()
            {
                AddressLineOne = "UNIT 1",
                AddressLineTwo = "OUT OF BOUNDS",
                Street = "Von Backstrom Boulevard",
                Suburb = "Silverlakes",
                TownOrCity = "Pretoria",
                PostalCode = "0081",
                Email = "goodwillgumede@yahoo.co.za",
                CellphoneNumber = "0721248899",
                Bank = "FNB",
                AccountNumber = "6211134445267",
                BranchCode = "206658",
                Reference = "aasasdasd",
                Name = "Spar Silver Oaks",
                RepresentativeId = "789",
                BusinessId = "123"
            });

            repository.Add(customer);

            Customer savedCustomer = repository.GetById(customer.Id);

            Assert.IsNotNull(savedCustomer);

            repository.Delete(savedCustomer);

            savedCustomer = repository.GetById(customer.Id);

            Assert.IsNull(savedCustomer);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGetById_MustFailGivenNullId()
        {
            UnitTestContext.Initialize();
            ICustomerRepository repository = this._iocContainer.Resolve<ICustomerRepository>();

            Customer savedCustomer = repository.GetById(null);

            Assert.IsNull(savedCustomer);
        }

        [TestMethod]
        public void TestGetById_MustReturnNullCustomerGivenIdThatDoesNotExist()
        {
            UnitTestContext.Initialize();
            ICustomerRepository repository = this._iocContainer.Resolve<ICustomerRepository>();

            Customer savedCustomer = repository.GetById("asdasdsad");

            Assert.IsNull(savedCustomer);
        }

        [TestMethod]
        public void TestGetAll_CanGetAllCustomers()
        {
            UnitTestContext.Initialize();
            ICustomerRepository repository = this._iocContainer.Resolve<ICustomerRepository>();
            IDomainFactory<Customer, CustomerAm> domainFactory =
                this._iocContainer.Resolve<IDomainFactory<Customer, CustomerAm>>();
            Customer newCustomer = domainFactory.BuildDomainEntityType(new CustomerAm()
            {
                AddressLineOne = "UNIT 1",
                AddressLineTwo = "OUT OF BOUNDS",
                Street = "Von Backstrom Boulevard",
                Suburb = "Silverlakes",
                TownOrCity = "Pretoria",
                PostalCode = "0081",
                Email = "goodwillgumede@yahoo.co.za",
                CellphoneNumber = "0721248899",
                Bank = "FNB",
                AccountNumber = "6211134445267",
                BranchCode = "206658",
                Reference = "aasasdasd",
                Name = "Spar Silver Oaks",
                RepresentativeId = "789",
                BusinessId = "123"
            });

            repository.Add(newCustomer);

            var allCustomers = repository.GetAll();

            Assert.IsTrue(allCustomers.Count() > 0);

            foreach (Customer customer in allCustomers)
            {
                repository.Delete(customer);
            }

            allCustomers = repository.GetAll();

            Assert.IsNotNull(allCustomers);
            Assert.AreEqual(0, allCustomers.Count());
        }







        [TestMethod]
        public void TestAddAlotOfCustomers()
        {
            UnitTestContext.Initialize();

            ICustomerRepository repository = this._iocContainer.Resolve<ICustomerRepository>();

            IDomainFactory<Customer, CustomerAm> domainFactory =
                this._iocContainer.Resolve<IDomainFactory<Customer, CustomerAm>>();

            for (int i = 0; i < 10000; i++)
            {
                Customer customer = domainFactory.BuildDomainEntityType(new CustomerAm()
                {
                    AddressLineOne = "UNIT 1",
                    AddressLineTwo = "OUT OF BOUNDS",
                    Street = "Von Backstrom Boulevard",
                    Suburb = "Silverlakes",
                    TownOrCity = "Pretoria",
                    PostalCode = "0081",
                    Email = $"{i}@testData.com",
                    CellphoneNumber = "0721248899",
                    Bank = "FNB",
                    AccountNumber = "6211134445267",
                    BranchCode = "206658",
                    Reference = "aasasdasd",
                    Name = $"TESTCUSTOMER_{i}",
                    RepresentativeId = "789",
                    BusinessId = "TEST123"
                });


                repository.Add(customer);
            }
        }
    }
}
