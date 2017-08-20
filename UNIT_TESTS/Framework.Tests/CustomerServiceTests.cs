using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Customer;
using KhanyisaIntel.Kbit.Framework.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Infrustructure.DependencyInjection;
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

            CustomerResponse response = service.Add(new CustomerServiceRequest() {Customer = null});

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

            CustomerResponse response = service.Add(new CustomerServiceRequest() {Customer = new CustomerAm()
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
                RepresentativeId = "789",
                RepresentativeName = "Goodwill",
                RepresentativeCode = "88878"
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
                Customer = new CustomerAm()
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
                    RepresentativeCode = "88878",
                    BusinessId = "TEST123"
                }
            });


            Assert.IsNotNull(response);
            Assert.AreEqual(ServiceResult.Success, response.ServiceResult);
            Assert.IsNotNull(response.Message);
        }

        //[TestMethod]
        //public void TestCanGetCustomer()
        //{
        //    UnitTestContext.Initialize();
        //    ICustomerService service = this._iocContainer.Resolve<ICustomerService>();

        //    Assert.IsNotNull(service);

        //    CustomerResponse response2 = service.GetById(new CustomerServiceRequest()
        //    {
        //        EntityId ="3d2b1db8-4dff-4447-b80d-f72914eddfbe"
        //    });

        //    Assert.AreEqual(ServiceResult.Success,response2.ServiceResult);
        //    Assert.IsNotNull(response2.CustomerAm);
        //    Assert.AreEqual("Spar Silver Oaks", response2.CustomerAm.Name);

        //}
    }
}
