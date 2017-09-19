using System;
using System.Collections.Generic;
using System.Linq;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Business;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Exception;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Workflow.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Framework.Tests.BusinessIntelligence.Repository.Tests
{
    [TestClass]
    public class BusinessRepositoryTests
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

        private readonly BillingInformation _billingInformation = new BillingInformation("Some Bank",
            "123456789",
            "321",
            "Reference123");

        public BusinessRepositoryTests()
        {
            _autoResolver = new IocContainer();

        }

        [TestMethod]
        public void TestCanResolveDependency()
        {
            IBusinessRepository target = _autoResolver.Resolve<IBusinessRepository>();

            Assert.IsNotNull(target);
            Assert.IsInstanceOfType(target, typeof(IBusinessRepository));
        }

        #region Test Add
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestBusinessRepositoryAdd_MustThrowArgumentNullExceptionGivenNullParameter()
        {
            IBusinessRepository target = _autoResolver.Resolve<IBusinessRepository>();
            target.Add(null);
        }

        [TestMethod]
        public void TestBusinessRepositoryAdd_MustCreateANewBusiness()
        {
            Business expectedBusiness = new Business("Test Add New Business", this._address, this._contactDetails,
                this._billingInformation);

            IBusinessRepository target = _autoResolver.Resolve<IBusinessRepository>();
            target.Add(expectedBusiness);

            Business actualCustomer = target.GetById(expectedBusiness.Id);

            Assert.IsNotNull(actualCustomer);
            Assert.AreEqual(expectedBusiness.Name, actualCustomer.Name);
            Assert.AreEqual(expectedBusiness.Address.AddressLineOne, actualCustomer.Address.AddressLineOne);
            Assert.AreEqual(expectedBusiness.Address.AddressLineTwo, actualCustomer.Address.AddressLineTwo);
            Assert.AreEqual(expectedBusiness.Address.Street, actualCustomer.Address.Street);
            Assert.AreEqual(expectedBusiness.Address.Suburb, actualCustomer.Address.Suburb);
            Assert.AreEqual(expectedBusiness.Address.PostalCode, actualCustomer.Address.PostalCode);
            Assert.AreEqual(expectedBusiness.Address.TownOrCity, actualCustomer.Address.TownOrCity);
            Assert.AreEqual(expectedBusiness.BillingInformation.AccountNumber, actualCustomer.BillingInformation.AccountNumber);
            Assert.AreEqual(expectedBusiness.BillingInformation.Bank, actualCustomer.BillingInformation.Bank);
            Assert.AreEqual(expectedBusiness.BillingInformation.BranchCode, actualCustomer.BillingInformation.BranchCode);
            Assert.AreEqual(expectedBusiness.BillingInformation.Reference, actualCustomer.BillingInformation.Reference);
            Assert.AreEqual(expectedBusiness.ContactDetails.CellphoneNumber, actualCustomer.ContactDetails.CellphoneNumber);
            Assert.AreEqual(expectedBusiness.ContactDetails.Email, actualCustomer.ContactDetails.Email);
            Assert.AreEqual(expectedBusiness.ContactDetails.TelephoneNumber, actualCustomer.ContactDetails.TelephoneNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityAlreadyExistException))]
        public void TestBusinessRepositoryAdd_MustNotCreateANewBusinessDueToBusinessAlreadyExisting()
        {
            Business expectedBusiness = new Business("Test Add Existing Business", this._address, this._contactDetails,
                this._billingInformation);

            IBusinessRepository target = _autoResolver.Resolve<IBusinessRepository>();
            target.Add(expectedBusiness);
            target.Add(expectedBusiness);
        }
        #endregion

        #region Test Delete
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestBusinessRepositoryDelete_MustThrowArgumentNullExceptionGivenNullParameter()
        {
            IBusinessRepository target = _autoResolver.Resolve<IBusinessRepository>();
            target.Delete(null);
        }

        [TestMethod]
        public void TestBusinessRepositoryDelete_MustDeleteExistingBusiness()
        {
            Business expectedBusiness = new Business("Test Delete Existing Business", this._address, this._contactDetails,
                this._billingInformation);

            IBusinessRepository target = _autoResolver.Resolve<IBusinessRepository>();
            target.Add(expectedBusiness);
            target.Delete(expectedBusiness);

            Business actualBusiness = target.GetById(expectedBusiness.Id);

            Assert.IsNull(actualBusiness);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestBusinessRepositoryDelete_MustNotDeleteBusinessDueToBusinessNotExisting()
        {
            Business expectedBusiness = new Business("Test Delete Existing Business", this._address, this._contactDetails,
                this._billingInformation);

            IBusinessRepository target = _autoResolver.Resolve<IBusinessRepository>();
            target.Delete(expectedBusiness);
        }
        #endregion

        #region Test GetAll
        [TestMethod]
        public void TestBusinessRepositoryGetAll_MustReturnAllExistingBusiness()
        {
            Business expectedBusiness1 = new Business("Get Business 1", this._address, this._contactDetails,
                this._billingInformation);
            Business expectedBusiness2 = new Business("Get Business 2", this._address, this._contactDetails,
                this._billingInformation);

            IBusinessRepository target = _autoResolver.Resolve<IBusinessRepository>();
            target.Add(expectedBusiness1);
            target.Add(expectedBusiness2);

            IEnumerable<Business> actualCustomerList = target.GetAll();

            Assert.IsNotNull(actualCustomerList);
            Assert.AreNotEqual(0, actualCustomerList.Count());
            Assert.IsNotNull(actualCustomerList.FirstOrDefault().Name);
            Assert.IsNotNull(actualCustomerList.FirstOrDefault().Address);
            Assert.IsNotNull(actualCustomerList.FirstOrDefault().BillingInformation);
            Assert.IsNotNull(actualCustomerList.FirstOrDefault().ContactDetails);
            Assert.IsNotNull(actualCustomerList.FirstOrDefault().Address.AddressLineOne);
            Assert.IsNotNull(actualCustomerList.FirstOrDefault().Address.AddressLineTwo);
            Assert.IsNotNull(actualCustomerList.FirstOrDefault().Address.PostalCode);
            Assert.IsNotNull(actualCustomerList.FirstOrDefault().Address.Street);
            Assert.IsNotNull(actualCustomerList.FirstOrDefault().Address.Suburb);
            Assert.IsNotNull(actualCustomerList.FirstOrDefault().Address.TownOrCity);
            Assert.IsNotNull(actualCustomerList.FirstOrDefault().BillingInformation.AccountNumber);
            Assert.IsNotNull(actualCustomerList.FirstOrDefault().BillingInformation.Bank);
            Assert.IsNotNull(actualCustomerList.FirstOrDefault().BillingInformation.BranchCode);
            Assert.IsNotNull(actualCustomerList.FirstOrDefault().BillingInformation.Reference);
            Assert.IsNotNull(actualCustomerList.FirstOrDefault().ContactDetails.Email);
            Assert.IsNotNull(actualCustomerList.FirstOrDefault().ContactDetails.CellphoneNumber);
            Assert.IsNotNull(actualCustomerList.FirstOrDefault().ContactDetails.TelephoneNumber);
            Assert.IsNotNull(actualCustomerList.LastOrDefault().Name);
            Assert.IsNotNull(actualCustomerList.LastOrDefault().Address);
            Assert.IsNotNull(actualCustomerList.LastOrDefault().BillingInformation);
            Assert.IsNotNull(actualCustomerList.LastOrDefault().ContactDetails);
            Assert.IsNotNull(actualCustomerList.LastOrDefault().Address.AddressLineOne);
            Assert.IsNotNull(actualCustomerList.LastOrDefault().Address.AddressLineTwo);
            Assert.IsNotNull(actualCustomerList.LastOrDefault().Address.PostalCode);
            Assert.IsNotNull(actualCustomerList.LastOrDefault().Address.Street);
            Assert.IsNotNull(actualCustomerList.LastOrDefault().Address.Suburb);
            Assert.IsNotNull(actualCustomerList.LastOrDefault().Address.TownOrCity);
            Assert.IsNotNull(actualCustomerList.LastOrDefault().BillingInformation.AccountNumber);
            Assert.IsNotNull(actualCustomerList.LastOrDefault().BillingInformation.Bank);
            Assert.IsNotNull(actualCustomerList.LastOrDefault().BillingInformation.BranchCode);
            Assert.IsNotNull(actualCustomerList.LastOrDefault().BillingInformation.Reference);
            Assert.IsNotNull(actualCustomerList.LastOrDefault().ContactDetails.Email);
            Assert.IsNotNull(actualCustomerList.LastOrDefault().ContactDetails.CellphoneNumber);
            Assert.IsNotNull(actualCustomerList.LastOrDefault().ContactDetails.TelephoneNumber);

            target.Delete(expectedBusiness1);
            target.Delete(expectedBusiness2);
        }
        #endregion

        #region Test GetByID
        [TestMethod]
        public void TestBusinessRepositoryGetByID_MustReturnAnExistingBusiness()
        {
            Business expectedBusiness = new Business("Test Delete Existing Business", this._address, this._contactDetails,
                this._billingInformation);

            IBusinessRepository target = _autoResolver.Resolve<IBusinessRepository>();
            target.Add(expectedBusiness);

            Business actualBusiness = target.GetById(expectedBusiness.Id);

            Assert.IsNotNull(actualBusiness);
            Assert.AreEqual(expectedBusiness.Id, actualBusiness.Id);
            Assert.AreEqual(expectedBusiness.Name, actualBusiness.Name);
            Assert.AreEqual("Test Delete Existing Business", actualBusiness.Name);
            Assert.AreEqual(expectedBusiness.Address.AddressLineOne, actualBusiness.Address.AddressLineOne);
            Assert.AreEqual(expectedBusiness.Address.AddressLineTwo, actualBusiness.Address.AddressLineTwo);
            Assert.AreEqual(expectedBusiness.Address.PostalCode, actualBusiness.Address.PostalCode);
            Assert.AreEqual(expectedBusiness.Address.Street, actualBusiness.Address.Street);
            Assert.AreEqual(expectedBusiness.Address.Suburb, actualBusiness.Address.Suburb);
            Assert.AreEqual(expectedBusiness.Address.TownOrCity, actualBusiness.Address.TownOrCity);
            Assert.AreEqual(expectedBusiness.ContactDetails.Email, actualBusiness.ContactDetails.Email);
            Assert.AreEqual(expectedBusiness.ContactDetails.CellphoneNumber, actualBusiness.ContactDetails.CellphoneNumber);
            Assert.AreEqual(expectedBusiness.ContactDetails.TelephoneNumber, actualBusiness.ContactDetails.TelephoneNumber);
            Assert.AreEqual(expectedBusiness.BillingInformation.Bank, actualBusiness.BillingInformation.Bank);
            Assert.AreEqual(expectedBusiness.BillingInformation.AccountNumber, actualBusiness.BillingInformation.AccountNumber);
            Assert.AreEqual(expectedBusiness.BillingInformation.BranchCode, actualBusiness.BillingInformation.BranchCode);
            Assert.AreEqual(expectedBusiness.BillingInformation.Reference, actualBusiness.BillingInformation.Reference);
        }
        #endregion

        #region Test Is Exists
        [TestMethod]
        public void TestBusinessRepositoryIsExists_MustReturnTrueGivenAnExistingBusiness()
        {
            Business expectedBusiness = new Business("Test Existing Business", this._address, this._contactDetails,
                this._billingInformation);

            IBusinessRepository target = _autoResolver.Resolve<IBusinessRepository>();
            target.Add(expectedBusiness);

            bool actualResult = target.IsExist(expectedBusiness);

            Assert.AreEqual(true, actualResult);
        }

        [TestMethod]
        public void TestBusinessRepositoryIsExists_MustReturnFalseGivenANonExistingBusiness()
        {
            Business expectedBusiness = new Business("Test Non-Existing Business", this._address, this._contactDetails,
                this._billingInformation);

            IBusinessRepository target = _autoResolver.Resolve<IBusinessRepository>();

            bool actualResult = target.IsExist(expectedBusiness);

            Assert.AreEqual(false, actualResult);
        }
        #endregion

        #region Test Update
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestBusinessRepositoryUpdate_MustThrowArgumentNullExceptionGivenNullParameter()
        {
            IBusinessRepository target = _autoResolver.Resolve<IBusinessRepository>();
            target.Update(null);
        }

        [TestMethod]
        public void TestBusinessRepositoryUpdate_MustUpdateAnExistingBusiness()
        {
            Business initialBusiness = new Business("Test Updating Existing Business", this._address, this._contactDetails,
                this._billingInformation);

            IBusinessRepository target = _autoResolver.Resolve<IBusinessRepository>();
            target.Add(initialBusiness);

            Address updatedAddress = new Address("UpdatedAddressLineOne"
                , "UpdatedAddressLineTwo"
                , "UpdatedStreet"
                , "UpdatedSuburb"
                , "UpdatedTownOrCity"
                , "UpdatedPostalCode");

            Business updatingBusiness = new Business("Test Updated Existing Business", updatedAddress, this._contactDetails,
                this._billingInformation);

            updatingBusiness.Id = initialBusiness.Id;
            initialBusiness = updatingBusiness;

            target.Update(initialBusiness);
            Business updatedCustomer = target.GetById(initialBusiness.Id);

            Assert.IsNotNull(updatedCustomer);
            Assert.AreEqual(initialBusiness.Name, updatedCustomer.Name);
            Assert.AreEqual("UpdatedAddressLineOne", updatedCustomer.Address.AddressLineOne);
            Assert.AreEqual("Test Updated Existing Business", updatedCustomer.Name);
            Assert.AreEqual(initialBusiness.BillingInformation.Bank, updatedCustomer.BillingInformation.Bank);
            Assert.AreEqual(initialBusiness.BillingInformation.AccountNumber, updatedCustomer.BillingInformation.AccountNumber);
            Assert.AreEqual(initialBusiness.BillingInformation.BranchCode, updatedCustomer.BillingInformation.BranchCode);
            Assert.AreEqual(initialBusiness.BillingInformation.Reference, updatedCustomer.BillingInformation.Reference);
            Assert.AreEqual(initialBusiness.Address.PostalCode, updatedCustomer.Address.PostalCode);
            Assert.AreEqual(initialBusiness.Address.Street, updatedCustomer.Address.Street);
            Assert.AreEqual(initialBusiness.Address.AddressLineOne, updatedCustomer.Address.AddressLineOne);
            Assert.AreEqual(initialBusiness.Address.AddressLineTwo, updatedCustomer.Address.AddressLineTwo);
            Assert.AreEqual(initialBusiness.Address.Suburb, updatedCustomer.Address.Suburb);
            Assert.AreEqual(initialBusiness.Address.TownOrCity, updatedCustomer.Address.TownOrCity);
            Assert.AreEqual(initialBusiness.ContactDetails.TelephoneNumber, updatedCustomer.ContactDetails.TelephoneNumber);
            Assert.AreEqual(initialBusiness.ContactDetails.CellphoneNumber, updatedCustomer.ContactDetails.CellphoneNumber);
            Assert.AreEqual(initialBusiness.ContactDetails.Email, updatedCustomer.ContactDetails.Email);
            Assert.AreEqual("UpdatedPostalCode", updatedCustomer.Address.PostalCode);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestBusinessRepositoryUpdate_MustThrowExceptionDueToBusinessNotExisting()
        {
            Business expectedBusiness = new Business("Test Updating Non-Existent Business", this._address, this._contactDetails,
                this._billingInformation);

            IBusinessRepository target = _autoResolver.Resolve<IBusinessRepository>();
            target.Update(expectedBusiness);
        }
        #endregion
    }
}
