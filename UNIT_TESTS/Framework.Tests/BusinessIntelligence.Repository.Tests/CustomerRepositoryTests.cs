using System;
using System.Collections.Generic;
using System.Linq;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Customer;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Exception;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Workflow.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Framework.Tests.BusinessIntelligence.Repository.Tests
{
    [TestClass]
    public class CustomerRepositoryTests
    {
        private readonly IAutoResolver _autoResolver;

        private readonly Address _address = new Address("Address Line 1",
                "Address Line 2",
                "Postal Code",
                "Street Name",
                "Suburb Name",
                "Town Or City Name");

        private readonly ContactDetails _contactDetails = new ContactDetails("email@gmail.com",
            "0111231234",
            "0740740741");

        private readonly Representative _representative = new Representative("123",
            "Representative 1",
            "123456");

        private readonly BillingInformation _billingInformation = new BillingInformation("Some Bank",
            "123456789", 
            "321", 
            "Reference123");

        public CustomerRepositoryTests()
        {
            _autoResolver = new IocContainer();
        }

        [TestMethod]
        public void TestCanResolveDependency()
        {
            ICustomerRepository target = _autoResolver.Resolve<ICustomerRepository>();

            Assert.IsNotNull(target);
            Assert.IsInstanceOfType(target, typeof(ICustomerRepository));
        }

        #region Test Add
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCustomerRepositoryAdd_MustThrowArgumentNullExceptionGivenNullParameter()
        {
            ICustomerRepository target = _autoResolver.Resolve<ICustomerRepository>();
            target.Add(null);
        }

        [TestMethod]
        public void TestCustomerRepositoryAdd_MustCreateANewCustomer()
        {
            Customer expectedCustomer = new Customer("My Customer 1" , "test Code",this._address, this._contactDetails, this._representative,
                this._billingInformation, "BusinessId123");

            ICustomerRepository target = _autoResolver.Resolve<ICustomerRepository>();
            target.Add(expectedCustomer);

            Customer actualCustomer = target.GetById(expectedCustomer.Id);

            Assert.IsNotNull(actualCustomer);
            Assert.AreEqual(expectedCustomer.Name, actualCustomer.Name);
            Assert.AreEqual(expectedCustomer.Address.AddressLineOne, actualCustomer.Address.AddressLineOne);
            Assert.AreEqual(expectedCustomer.Address.AddressLineTwo, actualCustomer.Address.AddressLineTwo);
            Assert.AreEqual(expectedCustomer.Address.PostalCode, actualCustomer.Address.PostalCode);
            Assert.AreEqual(expectedCustomer.Address.Street, actualCustomer.Address.Street);
            Assert.AreEqual(expectedCustomer.Address.Suburb, actualCustomer.Address.Suburb);
            Assert.AreEqual(expectedCustomer.Address.TownOrCity, actualCustomer.Address.TownOrCity);
            Assert.AreEqual(expectedCustomer.BillingInformation.AccountNumber, actualCustomer.BillingInformation.AccountNumber);
            Assert.AreEqual(expectedCustomer.BillingInformation.Bank, actualCustomer.BillingInformation.Bank);
            Assert.AreEqual(expectedCustomer.BillingInformation.BranchCode, actualCustomer.BillingInformation.BranchCode);
            Assert.AreEqual(expectedCustomer.BillingInformation.Reference, actualCustomer.BillingInformation.Reference);
            Assert.AreEqual(expectedCustomer.ContactDetails.CellphoneNumber, actualCustomer.ContactDetails.CellphoneNumber);
            Assert.AreEqual(expectedCustomer.ContactDetails.TelephoneNumber, actualCustomer.ContactDetails.TelephoneNumber);
            Assert.AreEqual(expectedCustomer.ContactDetails.Email, actualCustomer.ContactDetails.Email);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityAlreadyExistException))]
        public void TestCustomerRepositoryAdd_MustNotCreateANewCustomerDueToCustomerAlreadyExisting()
        {
            Customer expectedCustomer = new Customer("My Customer Already Existing", "asdasd",this._address, this._contactDetails, this._representative,
                this._billingInformation, "BusinessId123");

            ICustomerRepository target = _autoResolver.Resolve<ICustomerRepository>();
            target.Add(expectedCustomer);
            target.Add(expectedCustomer);
        }
        #endregion

        #region Test Delete
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCustomerRepositoryDelete_MustThrowArgumentNullExceptionGivenNullParameter()
        {
            ICustomerRepository target = _autoResolver.Resolve<ICustomerRepository>();
            target.Delete(null);
        }

        [TestMethod]
        public void TestCustomerRepositoryDelete_MustDeleteExistingCustomer()
        {
            Customer expectedCustomer = new Customer("My Customer To Delete", "asdsd",this._address, this._contactDetails, this._representative,
                this._billingInformation, "BusinessId123");

            ICustomerRepository target = _autoResolver.Resolve<ICustomerRepository>();
            target.Add(expectedCustomer);
            target.Delete(expectedCustomer);

            Customer actualCustomer = target.GetById(expectedCustomer.Id);

            Assert.IsNull(actualCustomer);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestCustomerRepositoryDelete_MustNotDeleteCustomerDueToCustomerNotExisting()
        {
            Customer expectedCustomer = new Customer("My Customer That Does Not Exist","asdsd",this._address, this._contactDetails, this._representative,
                this._billingInformation, "BusinessId123");

            ICustomerRepository target = _autoResolver.Resolve<ICustomerRepository>();
            target.Delete(expectedCustomer);
        }
        #endregion

        #region Test GetAll
        [TestMethod]
        public void TestCustomerRepositoryGetAll_MustReturnAllExistingCustomers()
        {
            Customer expectedCustomer1 = new Customer("Get All Customer 1", "asdsd",this._address, this._contactDetails, this._representative,
                this._billingInformation, "BusinessId123");
            Customer expectedCustomer2 = new Customer("Get All Customer 2", "asdsd",this._address, this._contactDetails, this._representative,
                this._billingInformation, "BusinessId456");

            ICustomerRepository target = _autoResolver.Resolve<ICustomerRepository>();
            target.Add(expectedCustomer1);
            target.Add(expectedCustomer2);

            IEnumerable<Customer> actualCustomerList = target.GetAll();

            Assert.IsNotNull(actualCustomerList);
            Assert.AreNotEqual(0, actualCustomerList.Count());

            target.Delete(expectedCustomer1);
            target.Delete(expectedCustomer2);
        }
        #endregion

        #region Test GetByID
        [TestMethod]
        public void TestCustomerRepositoryGetByID_MustReturnAnExistingCustomer()
        {
            Customer expectedCustomer = new Customer("Test Get By ID", "asdsd",this._address, this._contactDetails, this._representative,
                this._billingInformation, "BusinessId123");

            ICustomerRepository target = _autoResolver.Resolve<ICustomerRepository>();
            target.Add(expectedCustomer);

            Customer actualCustomer = target.GetById(expectedCustomer.Id);

            Assert.IsNotNull(actualCustomer);
            Assert.AreEqual(expectedCustomer.Name, actualCustomer.Name);
            Assert.AreEqual(expectedCustomer.Address.AddressLineOne, actualCustomer.Address.AddressLineOne);
            Assert.AreEqual(expectedCustomer.Address.AddressLineTwo, actualCustomer.Address.AddressLineTwo);
            Assert.AreEqual(expectedCustomer.Address.PostalCode, actualCustomer.Address.PostalCode);
            Assert.AreEqual(expectedCustomer.Address.Street, actualCustomer.Address.Street);
            Assert.AreEqual(expectedCustomer.Address.Suburb, actualCustomer.Address.Suburb);
            Assert.AreEqual(expectedCustomer.Address.TownOrCity, actualCustomer.Address.TownOrCity);
            Assert.AreEqual(expectedCustomer.BillingInformation.AccountNumber, actualCustomer.BillingInformation.AccountNumber);
            Assert.AreEqual(expectedCustomer.BillingInformation.Bank, actualCustomer.BillingInformation.Bank);
            Assert.AreEqual(expectedCustomer.BillingInformation.BranchCode, actualCustomer.BillingInformation.BranchCode);
            Assert.AreEqual(expectedCustomer.BillingInformation.Reference, actualCustomer.BillingInformation.Reference);
            Assert.AreEqual(expectedCustomer.ContactDetails.CellphoneNumber, actualCustomer.ContactDetails.CellphoneNumber);
            Assert.AreEqual(expectedCustomer.ContactDetails.TelephoneNumber, actualCustomer.ContactDetails.TelephoneNumber);
            Assert.AreEqual(expectedCustomer.ContactDetails.Email, actualCustomer.ContactDetails.Email);

        }
        #endregion

        #region Test Is Exists
        [TestMethod]
        public void TestCustomerRepositoryIsExists_MustReturnTrueGivenAnExistingCustomer()
        {
            Customer expectedCustomer = new Customer("Test Is Exist Customer", "asdsd",this._address, this._contactDetails, this._representative,
                this._billingInformation, "BusinessId123");

            ICustomerRepository target = _autoResolver.Resolve<ICustomerRepository>();
            target.Add(expectedCustomer);

            bool actualResult = target.IsExist(expectedCustomer);

            Assert.AreEqual(true, actualResult);

        }

        [TestMethod]
        public void TestCustomerRepositoryIsExists_MustReturnFalseGivenANonExistingCustomer()
        {
            Customer expectedCustomer = new Customer("Test Is Exist Customer", "asdsd",this._address, this._contactDetails, this._representative,
                this._billingInformation, "BusinessId123");

            ICustomerRepository target = _autoResolver.Resolve<ICustomerRepository>();

            bool actualResult = target.IsExist(expectedCustomer);

            Assert.AreEqual(false, actualResult);

        }
        #endregion

        #region Test Update
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCustomerRepositoryUpdate_MustThrowArgumentNullExceptionGivenNullParameter()
        {
            ICustomerRepository target = _autoResolver.Resolve<ICustomerRepository>();
            target.Update(null);
        }

        [TestMethod]
        public void TestCustomerRepositoryUpdate_MustUpdateAnExistingCustomer()
        {
            Customer initialCustomer = new Customer("Test Is Exist Customer", "asdsd",this._address, this._contactDetails, this._representative,
                this._billingInformation, "BusinessId123");

            ICustomerRepository target = _autoResolver.Resolve<ICustomerRepository>();
            target.Add(initialCustomer);

            Address updatedAddress = new Address("UpdatedAddressLineOne"
                , "UpdatedAddressLineTwo"
                , "UpdatedStreet"
                , "UpdatedSuburb"
                , "UpdatedTownOrCity"
                , "UpdatedPostalCode");

            Customer updatingCustomer = new Customer("Test Is Exist Customer","asdsd", updatedAddress, this._contactDetails, this._representative,
                this._billingInformation, "BusinessId123");

            updatingCustomer.Id = initialCustomer.Id;
            initialCustomer = updatingCustomer;

            target.Update(initialCustomer);
            Customer updatedCustomer = target.GetById(initialCustomer.Id);

            Assert.IsNotNull(updatedCustomer);
            Assert.AreEqual("UpdatedAddressLineOne", updatedCustomer.Address.AddressLineOne);
            Assert.AreEqual("Test Is Exist Customer", updatedCustomer.Name);
            Assert.AreEqual("UpdatedPostalCode", updatedCustomer.Address.PostalCode);
            Assert.AreEqual(initialCustomer.Address.AddressLineOne, updatedCustomer.Address.AddressLineOne);
            Assert.AreEqual(initialCustomer.Address.AddressLineTwo, updatedCustomer.Address.AddressLineTwo);
            Assert.AreEqual(initialCustomer.Address.PostalCode, updatedCustomer.Address.PostalCode);
            Assert.AreEqual(initialCustomer.Address.Street, updatedCustomer.Address.Street);
            Assert.AreEqual(initialCustomer.Address.Suburb, updatedCustomer.Address.Suburb);
            Assert.AreEqual(initialCustomer.Address.TownOrCity, updatedCustomer.Address.TownOrCity);
            Assert.AreEqual(initialCustomer.BillingInformation.AccountNumber, updatedCustomer.BillingInformation.AccountNumber);
            Assert.AreEqual(initialCustomer.BillingInformation.Bank, updatedCustomer.BillingInformation.Bank);
            Assert.AreEqual(initialCustomer.BillingInformation.BranchCode, updatedCustomer.BillingInformation.BranchCode);
            Assert.AreEqual(initialCustomer.BillingInformation.Reference, updatedCustomer.BillingInformation.Reference);
            Assert.AreEqual(initialCustomer.ContactDetails.CellphoneNumber, updatedCustomer.ContactDetails.CellphoneNumber);
            Assert.AreEqual(initialCustomer.ContactDetails.TelephoneNumber, updatedCustomer.ContactDetails.TelephoneNumber);
            Assert.AreEqual(initialCustomer.ContactDetails.Email, updatedCustomer.ContactDetails.Email);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestCustomerRepositoryUpdate_MustThrowExceptionDueToCustomerNotExisting()
        {
            Customer expectedCustomer = new Customer("Test Non-Existent Customer", "asdsd",this._address, this._contactDetails, this._representative,
                this._billingInformation, "BusinessId123");

            ICustomerRepository target = _autoResolver.Resolve<ICustomerRepository>();
            target.Update(expectedCustomer);
        }
        #endregion
    }
}
