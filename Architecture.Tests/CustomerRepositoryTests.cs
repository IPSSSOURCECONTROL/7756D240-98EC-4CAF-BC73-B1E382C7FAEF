using System;
using System.Linq;
using Architecture.Tests.BusinessIntelligence.Application.Models;
using Architecture.Tests.BusinessIntelligence.Domain.Customer;
using Architecture.Tests.BusinessIntelligence.Domain.Factories.Customer;
using Architecture.Tests.DependencyInjection;
using Architecture.Tests.Infrustructure.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Architecture.Tests
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
            ICustomerRepository repository = this._iocContainer.Resolve<ICustomerRepository>();
            repository.Add(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdate_MustFailGivenNullEntity()
        {
            ICustomerRepository repository = this._iocContainer.Resolve<ICustomerRepository>();
            repository.Update(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDelete_MustFailGivenNullEntity()
        {
            ICustomerRepository repository = this._iocContainer.Resolve<ICustomerRepository>();
            repository.Delete(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestIsExists_MustFailGivenNullEntity()
        {
            ICustomerRepository repository = this._iocContainer.Resolve<ICustomerRepository>();
            repository.IsExist(null);
        }

        [TestMethod]
        public void TestAdd_MustAddCustomerSuccessfully()
        {
            ICustomerRepository repository = this._iocContainer.Resolve<ICustomerRepository>();

            Customer customer = CustomerFactory.BuildNewCustomer(new CustomerAm()
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
                RepresentativeName = "Goodwill",
                RepresentativeCode = "88878"
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
            ICustomerRepository repository = this._iocContainer.Resolve<ICustomerRepository>();

            Customer customer = CustomerFactory.BuildNewCustomer(new CustomerAm()
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
                RepresentativeName = "Goodwill",
                RepresentativeCode = "88878"
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
            ICustomerRepository repository = this._iocContainer.Resolve<ICustomerRepository>();

            Customer customer = CustomerFactory.BuildNewCustomer(new CustomerAm()
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
                RepresentativeName = "Goodwill",
                RepresentativeCode = "88878"
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
            ICustomerRepository repository = this._iocContainer.Resolve<ICustomerRepository>();

            Customer savedCustomer = repository.GetById(null);

            Assert.IsNull(savedCustomer);
        }

        [TestMethod]
        public void TestGetById_MustReturnNullCustomerGivenIdThatDoesNotExist()
        {
            ICustomerRepository repository = this._iocContainer.Resolve<ICustomerRepository>();

            Customer savedCustomer = repository.GetById("asdasdsad");

            Assert.IsNull(savedCustomer);
        }

        [TestMethod]
        public void TestGetAll_CanGetAllCustomers()
        {
            ICustomerRepository repository = this._iocContainer.Resolve<ICustomerRepository>();

            Customer newCustomer = CustomerFactory.BuildNewCustomer(new CustomerAm()
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
                RepresentativeName = "Goodwill",
                RepresentativeCode = "88878"
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
    }
}
