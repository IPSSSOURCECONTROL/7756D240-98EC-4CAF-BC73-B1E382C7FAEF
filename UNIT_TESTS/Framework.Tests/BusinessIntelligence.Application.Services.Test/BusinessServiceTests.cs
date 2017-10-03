using System.Linq;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Business;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Business;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Infrustructure.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;
using KhanyisaIntel.Kbit.Framework.Tests.TEST_UTILS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Framework.Tests.BusinessIntelligence.Application.Services.Tests
{
    [TestClass]
    public class BusinessServiceTests
    {
        private readonly IAutoResolver _iocContainer;

        public BusinessServiceTests()
        {
            _iocContainer = new IocContainer();
        }

        [TestMethod]
        public void TestCanResolveDependency()
        {
            IBusinessService target = _iocContainer.Resolve<IBusinessService>();

            Assert.IsNotNull(target);
            Assert.IsInstanceOfType(target, typeof(IBusinessService));
        }

        #region Test Add

        [TestMethod]
        public void TestBusinessServiceAdd_MustFailGIvenNullRequest()
        {
            UnitTestContext.Initialize();
            IBusinessService service = this._iocContainer.Resolve<IBusinessService>();

            Assert.IsNotNull(service);

            BusinessResponse response = service.Add(null);

            Assert.IsNotNull(response);
            Assert.AreEqual(ServiceResult.Error, response.ServiceResult);
            Assert.AreEqual("Null request recieved.", response.Message);
        }

        [TestMethod]
        public void TestCanAddCustomer_MustFailGIvenInvalidApplicationModel()
        {
            UnitTestContext.Initialize();
            IBusinessService service = this._iocContainer.Resolve<IBusinessService>();

            Assert.IsNotNull(service);

            BusinessResponse response = service.Add(new BusinessServiceRequest() { ApplicationModel = null });

            Assert.IsNotNull(response);
            Assert.AreEqual(ServiceResult.Error, response.ServiceResult);
            Assert.AreEqual("Business is a required field.", response.Message);
        }

        [TestMethod]
        public void TestBusinessServiceAdd_MustAddSuccessfully()
        {
            UnitTestContext.Initialize();
            IBusinessService service = this._iocContainer.Resolve<IBusinessService>();

            Assert.IsNotNull(service);

            BusinessResponse response = service.Add(new BusinessServiceRequest()
            {
                ApplicationModel = new BusinessAm
                {
                    AccountNumber = "123",
                    AddressLineOne = "Address Line One",
                    AddressLineTwo = "Address Line Two",
                    Street = "SomeStreet",
                    Suburb = "SomeSuburb",
                    TownOrCity = "Some Town / City",
                    Bank = "Bank1",
                    BranchCode = "BranchCode",
                    CellphoneNumber = "0741234567",
                    Email = "myemailaddress@gmail.com",
                    Reference = "Reference1",
                    Id = "1212123335698",
                    Name = "BusinessName",
                    PostalCode = "Postal Code"
                }
            });


            Assert.IsNotNull(response);
            Assert.AreEqual(ServiceResult.Success, response.ServiceResult);
            Assert.IsNotNull(response.Message);
        }

        [TestMethod]
        public void TestBusinessServiceAdd_MustNotCreateANewBusinessDueToNullBusinessObject()
        {
            BusinessServiceRequest businessServiceRequest = new BusinessServiceRequest();
            BusinessAm businessAm = null;
            businessServiceRequest.ApplicationModel = businessAm;

            IBusinessService target = _iocContainer.Resolve<IBusinessService>();
            target.Add(businessServiceRequest);
            
            BusinessResponse businessResponse = target.GetById(businessServiceRequest);

            Assert.IsNotNull(businessResponse);
        }

        [TestMethod]
        public void TestCanAddCustomer_MustFailGIvenInvalidRequireCustomerField_AddressLineOne()
        {
            UnitTestContext.Initialize();
            IBusinessService service = this._iocContainer.Resolve<IBusinessService>();

            Assert.IsNotNull(service);

            BusinessResponse response = service.Add(new BusinessServiceRequest()
            {
                ApplicationModel = new BusinessAm()
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
                    Name = "Spar Silver Oaks"
                }
            });
            
            Assert.IsNotNull(response);
            Assert.AreEqual(ServiceResult.Error, response.ServiceResult);
            Assert.AreEqual("Address Line One is a required field.", response.Message);
        }

        #endregion

        #region Test Update
        [TestMethod]
        public void TestCanUpdateBusiness_MustFailGIvenNullRequest()
        {
            UnitTestContext.Initialize();
            IBusinessService service = this._iocContainer.Resolve<IBusinessService>();

            Assert.IsNotNull(service);

            BusinessResponse response = service.Update(null);

            Assert.IsNotNull(response);
            Assert.AreEqual(ServiceResult.Error, response.ServiceResult);
            Assert.AreEqual("Null request recieved.", response.Message);
        }

        [TestMethod]
        public void TestCanUpdateBusiness_MustFailGIvenInvalidApplicationModel()
        {
            UnitTestContext.Initialize();
            IBusinessService service = this._iocContainer.Resolve<IBusinessService>();

            Assert.IsNotNull(service);

            BusinessResponse response = service.Update(new BusinessServiceRequest() { ApplicationModel = null });

            Assert.IsNotNull(response);
            Assert.AreEqual(ServiceResult.Error, response.ServiceResult);
            Assert.AreEqual("Business is a required field.", response.Message);
        }

        [TestMethod]
        public void TestCanUpdateBusiness_MustFailGIvenInvalidRequireCustomerField_AddressLineOne()
        {
            UnitTestContext.Initialize();
            IBusinessService service = this._iocContainer.Resolve<IBusinessService>();

            Assert.IsNotNull(service);

            BusinessResponse response = service.Update(new BusinessServiceRequest()
            {
                ApplicationModel = new BusinessAm()
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
                    Name = "Spar Silver Oaks"
                }
            });


            Assert.IsNotNull(response);
            Assert.AreEqual(ServiceResult.Error, response.ServiceResult);
            Assert.AreEqual("Address Line One is a required field.", response.Message);
        }

        [TestMethod]
        public void TestCanUpdateBusiness_MustUpdateSuccessfully()
        {
            UnitTestContext.Initialize();
            IBusinessService service = this._iocContainer.Resolve<IBusinessService>();

            Assert.IsNotNull(service);

            BusinessResponse response = service.Update(new BusinessServiceRequest()
            {
                ApplicationModel = new BusinessAm()
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
                    Id = "59ac7020e349692874eace76"
                }
            });
            
            Assert.IsNotNull(response);
            Assert.AreEqual(ServiceResult.Success, response.ServiceResult);
            Assert.IsNotNull(response.Message);
        }

        #endregion

        #region Test GetAll
        [TestMethod]
        public void TestCanGeAllBusiness_MustGetAllSuccessfully()
        {
            UnitTestContext.Initialize();
            IBusinessService service = this._iocContainer.Resolve<IBusinessService>();

            BusinessServiceRequest serviceRequest = new BusinessServiceRequest();

            BusinessResponse businessResponse = service.GetAll(serviceRequest);

            Assert.IsNotNull(businessResponse);
            Assert.IsNotNull(businessResponse.ApplicationModels);
            Assert.AreNotEqual(0, businessResponse.ApplicationModels.Count());
            Assert.IsNotNull(businessResponse.ApplicationModels.First().AddressLineOne);
            Assert.IsNotNull(businessResponse.ApplicationModels.First().AddressLineTwo);
            Assert.IsNotNull(businessResponse.ApplicationModels.First().AccountNumber);
            Assert.IsNotNull(businessResponse.ApplicationModels.First().CellphoneNumber);
            Assert.IsNotNull(businessResponse.ApplicationModels.First().Bank);
            Assert.IsNotNull(businessResponse.ApplicationModels.First().CellphoneNumber);
            Assert.IsNotNull(businessResponse.ApplicationModels.First().BranchCode);
            Assert.IsNotNull(businessResponse.ApplicationModels.First().Email);
            Assert.IsNotNull(businessResponse.ApplicationModels.First().Name);
            Assert.IsNotNull(businessResponse.ApplicationModels.First().PostalCode);
            Assert.IsNotNull(businessResponse.ApplicationModels.First().Reference);
            Assert.IsNotNull(businessResponse.ApplicationModels.First().Street);
            Assert.IsNotNull(businessResponse.ApplicationModels.First().Suburb);
            Assert.IsNotNull(businessResponse.ApplicationModels.First().TownOrCity);

            Assert.IsNotNull(businessResponse.ApplicationModels.Last().AddressLineOne);
            Assert.IsNotNull(businessResponse.ApplicationModels.Last().AddressLineTwo);
            Assert.IsNotNull(businessResponse.ApplicationModels.Last().AccountNumber);
            Assert.IsNotNull(businessResponse.ApplicationModels.Last().CellphoneNumber);
            Assert.IsNotNull(businessResponse.ApplicationModels.Last().Bank);
            Assert.IsNotNull(businessResponse.ApplicationModels.Last().CellphoneNumber);
            Assert.IsNotNull(businessResponse.ApplicationModels.Last().BranchCode);
            Assert.IsNotNull(businessResponse.ApplicationModels.Last().Email);
            Assert.IsNotNull(businessResponse.ApplicationModels.Last().Name);
            Assert.IsNotNull(businessResponse.ApplicationModels.Last().PostalCode);
            Assert.IsNotNull(businessResponse.ApplicationModels.Last().Reference);
            Assert.IsNotNull(businessResponse.ApplicationModels.Last().Street);
            Assert.IsNotNull(businessResponse.ApplicationModels.Last().Suburb);
            Assert.IsNotNull(businessResponse.ApplicationModels.Last().TownOrCity);
        }
        #endregion

        #region Test GetById

        [TestMethod]
        public void TestCanGetCustomerById_MustGetByIdSuccessfully()
        {
            UnitTestContext.Initialize();
            IBusinessService service = this._iocContainer.Resolve<IBusinessService>();
            IDomainFactory<Business, BusinessAm> factory = this._iocContainer.Resolve<IDomainFactory<Business, BusinessAm>>();
            IBusinessRepository repository = this._iocContainer.Resolve<IBusinessRepository>();

            Assert.IsNotNull(service);

            BusinessAm businessAm = new BusinessAm()
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
                Name = "Spar Silver Oaks"
            };

            Business business = factory.BuildDomainEntityType(businessAm);
            repository.Add(business);

            BusinessServiceRequest serviceRequest = new BusinessServiceRequest();
            serviceRequest.EntityId = business.Id;

            BusinessResponse businessResponse = service.GetById(serviceRequest);

            Assert.IsNotNull(businessResponse);
            Assert.IsNotNull(businessResponse.ApplicationModels);
            Assert.IsNotNull(businessResponse.ApplicationModel.AddressLineOne);
            Assert.IsNotNull(businessResponse.ApplicationModel.AddressLineTwo);
            Assert.IsNotNull(businessResponse.ApplicationModel.AccountNumber);
            Assert.IsNotNull(businessResponse.ApplicationModel.CellphoneNumber);
            Assert.IsNotNull(businessResponse.ApplicationModel.Bank);
            Assert.IsNotNull(businessResponse.ApplicationModel.CellphoneNumber);
            Assert.IsNotNull(businessResponse.ApplicationModel.BranchCode);
            Assert.IsNotNull(businessResponse.ApplicationModel.Email);
            Assert.IsNotNull(businessResponse.ApplicationModel.Name);
            Assert.IsNotNull(businessResponse.ApplicationModel.PostalCode);
            Assert.IsNotNull(businessResponse.ApplicationModel.Reference);
            Assert.IsNotNull(businessResponse.ApplicationModel.Street);
            Assert.IsNotNull(businessResponse.ApplicationModel.Suburb);
            Assert.IsNotNull(businessResponse.ApplicationModel.TownOrCity);
        }

        #endregion

    }
}
