using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Business;
using KhanyisaIntel.Kbit.Framework.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Infrustructure.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Framework.Tests
{
    [TestClass]
    public class BusinessTests
    {
        private IAutoResolver _iocContainer;

        public BusinessTests()
        {
            this._iocContainer = new IocContainer();

        }
        [TestMethod]
        public void TestCanAddBusiness()
        {
            IBusinessService service = this._iocContainer.Resolve<IBusinessService>();

            Assert.IsNotNull(service);

            BusinessResponse response = service.Add(new BusinessServiceRequest()
            {
                Business = new BusinessAm()
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
                }
            });


            Assert.IsNotNull(response);
            Assert.AreEqual(ServiceResult.Success, response.ServiceResult);
            Assert.IsNotNull(response.Message);
        }


    }
}
