using System.Linq;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Customer;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Customer;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Infrustructure.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;
using KhanyisaIntel.Kbit.Framework.Tests.TEST_UTILS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KhanyisaIntel.Kbit.Framework.Tests
{
    [TestClass]
    public class CustomerServiceTests
    {
        private IAutoResolver _iocContainer;

        public CustomerServiceTests()
        {
            this._iocContainer = new IocContainer();

        }
        [TestMethod]
        public void TestCanResolveDependency()
        {
            ICustomerService service = this._iocContainer.Resolve<ICustomerService>();

            Assert.IsNotNull(service);
        }

        #region Test Add
        [TestMethod]
        public void TestCanAddCustomer_MustFailGIvenNullRequest()
        {
            UnitTestContext.Initialize();
            ICustomerService service = this._iocContainer.Resolve<ICustomerService>();

            Assert.IsNotNull(service);

            CustomerResponse response = service.Add(null);

            Assert.IsNotNull(response);
            Assert.AreEqual(ServiceResult.Error, response.ServiceResult);
            Assert.AreEqual("Null request recieved.", response.Message);
        }

        [TestMethod]
        public void TestCanAddCustomer_MustFailGIvenInvalidApplicationModel()
        {
            UnitTestContext.Initialize();
            ICustomerService service = this._iocContainer.Resolve<ICustomerService>();

            Assert.IsNotNull(service);

            CustomerResponse response = service.Add(new CustomerServiceRequest() {ApplicationModel = null});

            Assert.IsNotNull(response);
            Assert.AreEqual(ServiceResult.Error, response.ServiceResult);
            Assert.AreEqual("Customer is a required field.", response.Message);
        }

        [TestMethod]
        public void TestCanAddCustomer_MustFailGIvenInvalidRequireCustomerField_AddressLineOne()
        {
            UnitTestContext.Initialize();
            ICustomerService service = this._iocContainer.Resolve<ICustomerService>();

            Assert.IsNotNull(service);

            CustomerResponse response = service.Add(new CustomerServiceRequest() {ApplicationModel = new CustomerAm()
            {
                //AddressLineOne = "UNIT 1",
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
                RepresentativeId = "59a5a057e3496b1becd3ab84"
            } });


            Assert.IsNotNull(response);
            Assert.AreEqual(ServiceResult.Error, response.ServiceResult);
            Assert.AreEqual("Address Line One is a required field.", response.Message);
        }

        [TestMethod]
        public void TestCanAddCustomer_MustAddSuccessfully()
        {
            UnitTestContext.Initialize();
            ICustomerService service = this._iocContainer.Resolve<ICustomerService>();

            Assert.IsNotNull(service);

            CustomerResponse response = service.Add(new CustomerServiceRequest()
            {
                ApplicationModel = new CustomerAm()
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
                    RepresentativeId = "59a5a057e3496b1becd3ab84",
                    BusinessId = "TEST123"
                }
            });
            
            Assert.IsNotNull(response);
            Assert.AreEqual(ServiceResult.Success, response.ServiceResult);
            Assert.IsNotNull(response.Message);
        }

        [TestMethod]
        public void TestCanAddCustomer_MustFailGIvenInvalidRequireCustomerField_RepresentativeId()
        {
            UnitTestContext.Initialize();
            ICustomerService service = this._iocContainer.Resolve<ICustomerService>();

            Assert.IsNotNull(service);

            CustomerResponse response = service.Add(new CustomerServiceRequest()
            {
                ApplicationModel = new CustomerAm()
                {
                    //AddressLineOne = "UNIT 1",
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
                    RepresentativeId = null
                }
            });
            
            Assert.IsNotNull(response);
            Assert.AreEqual(ServiceResult.Error, response.ServiceResult);
            Assert.AreEqual("Representative Id is a required field.", response.Message);
        }
        #endregion

        #region Test Update
        [TestMethod]
        public void TestCanUpdateCustomer_MustFailGIvenNullRequest()
        {
            UnitTestContext.Initialize();
            ICustomerService service = this._iocContainer.Resolve<ICustomerService>();

            Assert.IsNotNull(service);

            CustomerResponse response = service.Update(null);

            Assert.IsNotNull(response);
            Assert.AreEqual(ServiceResult.Error, response.ServiceResult);
            Assert.AreEqual("Null request recieved.", response.Message);
        }

        [TestMethod]
        public void TestCanUpdateCustomer_MustFailGIvenInvalidApplicationModel()
        {
            UnitTestContext.Initialize();
            ICustomerService service = this._iocContainer.Resolve<ICustomerService>();

            Assert.IsNotNull(service);

            CustomerResponse response = service.Update(new CustomerServiceRequest() { ApplicationModel = null });

            Assert.IsNotNull(response);
            Assert.AreEqual(ServiceResult.Error, response.ServiceResult);
            Assert.AreEqual("Customer is a required field.", response.Message);
        }

        [TestMethod]
        public void TestCanUpdateCustomer_MustFailGIvenInvalidRequireCustomerField_AddressLineOne()
        {
            UnitTestContext.Initialize();
            ICustomerService service = this._iocContainer.Resolve<ICustomerService>();

            Assert.IsNotNull(service);

            CustomerResponse response = service.Update(new CustomerServiceRequest()
            {
                ApplicationModel = new CustomerAm()
                {
                    //AddressLineOne = "UNIT 1",
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
                    RepresentativeId = "59a5a057e3496b1becd3ab84"
                }
            });


            Assert.IsNotNull(response);
            Assert.AreEqual(ServiceResult.Error, response.ServiceResult);
            Assert.AreEqual("Address Line One is a required field.", response.Message);
        }

        [TestMethod]
        public void TestCanUpdateCustomer_MustUpdateSuccessfully()
        {
            UnitTestContext.Initialize();
            ICustomerService service = this._iocContainer.Resolve<ICustomerService>();

            Assert.IsNotNull(service);

            CustomerResponse response = service.Update(new CustomerServiceRequest()
            {
                ApplicationModel = new CustomerAm()
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
                    RepresentativeId = "59a5a057e3496b1becd3ab84",
                    BusinessId = "TEST123",
                    Id = "59ac6620e3496b32f4e408ef"
                }
            });
            
            Assert.IsNotNull(response);
            Assert.AreEqual(ServiceResult.Success, response.ServiceResult);
            Assert.IsNotNull(response.Message);
        }

        #endregion
        
        #region Test GetAll
        [TestMethod]
        public void TestCanGeAllCustomer_MustGetAllSuccessfully()
        {
            UnitTestContext.Initialize();
            ICustomerService service = this._iocContainer.Resolve<ICustomerService>();
            
            CustomerServiceRequest serviceRequest = new CustomerServiceRequest();

            CustomerResponse customerResponse = service.GetAll(serviceRequest);

            Assert.IsNotNull(customerResponse);
            Assert.IsNotNull(customerResponse.ApplicationModels);
            Assert.AreNotEqual(0, customerResponse.ApplicationModels.Count());
            Assert.IsNotNull(customerResponse.ApplicationModels.First().AddressLineOne);
            Assert.IsNotNull(customerResponse.ApplicationModels.First().AddressLineTwo);
            Assert.IsNotNull(customerResponse.ApplicationModels.First().AccountNumber);
            Assert.IsNotNull(customerResponse.ApplicationModels.First().BusinessId);
            Assert.IsNotNull(customerResponse.ApplicationModels.First().CellphoneNumber);
            Assert.IsNotNull(customerResponse.ApplicationModels.First().RepresentativeId);
            Assert.IsNotNull(customerResponse.ApplicationModels.First().Bank);
            Assert.IsNotNull(customerResponse.ApplicationModels.First().CellphoneNumber);
            Assert.IsNotNull(customerResponse.ApplicationModels.First().BranchCode);
            Assert.IsNotNull(customerResponse.ApplicationModels.First().Email);
            Assert.IsNotNull(customerResponse.ApplicationModels.First().Name);
            Assert.IsNotNull(customerResponse.ApplicationModels.First().PostalCode);
            Assert.IsNotNull(customerResponse.ApplicationModels.First().Reference);
            Assert.IsNotNull(customerResponse.ApplicationModels.First().Street);
            Assert.IsNotNull(customerResponse.ApplicationModels.First().Suburb);
            Assert.IsNotNull(customerResponse.ApplicationModels.First().TownOrCity);

            Assert.IsNotNull(customerResponse.ApplicationModels.Last().AddressLineOne);
            Assert.IsNotNull(customerResponse.ApplicationModels.Last().AddressLineTwo);
            Assert.IsNotNull(customerResponse.ApplicationModels.Last().AccountNumber);
            Assert.IsNotNull(customerResponse.ApplicationModels.Last().BusinessId);
            Assert.IsNotNull(customerResponse.ApplicationModels.Last().CellphoneNumber);
            Assert.IsNotNull(customerResponse.ApplicationModels.Last().RepresentativeId);
            Assert.IsNotNull(customerResponse.ApplicationModels.Last().Bank);
            Assert.IsNotNull(customerResponse.ApplicationModels.Last().CellphoneNumber);
            Assert.IsNotNull(customerResponse.ApplicationModels.Last().BranchCode);
            Assert.IsNotNull(customerResponse.ApplicationModels.Last().Email);
            Assert.IsNotNull(customerResponse.ApplicationModels.Last().Name);
            Assert.IsNotNull(customerResponse.ApplicationModels.Last().PostalCode);
            Assert.IsNotNull(customerResponse.ApplicationModels.Last().Reference);
            Assert.IsNotNull(customerResponse.ApplicationModels.Last().Street);
            Assert.IsNotNull(customerResponse.ApplicationModels.Last().Suburb);
            Assert.IsNotNull(customerResponse.ApplicationModels.Last().TownOrCity);
        }
        #endregion

        #region Test GetById

        [TestMethod]
        public void TestCanGetCustomerById_MustGetByIdSuccessfully()
        {
            UnitTestContext.Initialize();
            ICustomerService service = this._iocContainer.Resolve<ICustomerService>();
            IDomainFactory<Customer, CustomerAm> factory = this._iocContainer.Resolve<IDomainFactory<Customer, CustomerAm>>();
            ICustomerRepository repository = this._iocContainer.Resolve<ICustomerRepository>();

            Assert.IsNotNull(service);

            CustomerAm customerAm = new CustomerAm()
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
                RepresentativeId = "59a5a057e3496b1becd3ab84",
                BusinessId = "TEST123"
            };

            Customer customer = factory.BuildDomainEntityType(customerAm);
            customer.AssignRepresentative(new Representative("asdasdasd", "asdasdasd", "codeasdsd"));
            repository.Add(customer);

            CustomerServiceRequest serviceRequest = new CustomerServiceRequest();
            serviceRequest.EntityId = customer.Id;

            CustomerResponse customerResponse = service.GetById(serviceRequest);

            Assert.IsNotNull(customerResponse);
            Assert.IsNotNull(customerResponse.ApplicationModels);
            Assert.AreNotEqual(1, customerResponse.ApplicationModels.Count());
            Assert.IsNotNull(customerResponse.ApplicationModel.AddressLineOne);
            Assert.IsNotNull(customerResponse.ApplicationModel.AddressLineTwo);
            Assert.IsNotNull(customerResponse.ApplicationModel.AccountNumber);
            Assert.IsNotNull(customerResponse.ApplicationModel.BusinessId);
            Assert.IsNotNull(customerResponse.ApplicationModel.CellphoneNumber);
            Assert.IsNotNull(customerResponse.ApplicationModel.RepresentativeId);
            Assert.IsNotNull(customerResponse.ApplicationModel.Bank);
            Assert.IsNotNull(customerResponse.ApplicationModel.CellphoneNumber);
            Assert.IsNotNull(customerResponse.ApplicationModel.BranchCode);
            Assert.IsNotNull(customerResponse.ApplicationModel.Email);
            Assert.IsNotNull(customerResponse.ApplicationModel.Name);
            Assert.IsNotNull(customerResponse.ApplicationModel.PostalCode);
            Assert.IsNotNull(customerResponse.ApplicationModel.Reference);
            Assert.IsNotNull(customerResponse.ApplicationModel.Street);
            Assert.IsNotNull(customerResponse.ApplicationModel.Suburb);
            Assert.IsNotNull(customerResponse.ApplicationModel.TownOrCity);
        }

        #endregion

    }
}
