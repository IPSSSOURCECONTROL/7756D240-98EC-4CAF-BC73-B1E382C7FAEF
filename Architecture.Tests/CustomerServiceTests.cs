using Architecture.Tests.BusinessIntelligence.Application.Models;
using Architecture.Tests.BusinessIntelligence.Application.Services.Customer;
using Architecture.Tests.DependencyInjection;
using Architecture.Tests.Infrustructure.Application;
using Architecture.Tests.Infrustructure.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Architecture.Tests
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
    }
}
