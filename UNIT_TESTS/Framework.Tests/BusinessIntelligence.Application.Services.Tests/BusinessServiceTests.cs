using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Business;
using KhanyisaIntel.Kbit.Framework.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Framework.Tests.BusinessIntelligence.Application.Services.Tests
{
    [TestClass]
    public class BusinessServiceTests
    {
        private readonly IAutoResolver _autoResolver;

        public BusinessServiceTests()
        {
            _autoResolver = new IocContainer();
        }

        [TestMethod]
        public void TestCanResolveDependency()
        {
            IBusinessService target = _autoResolver.Resolve<IBusinessService>();

            Assert.IsNotNull(target);
            Assert.IsInstanceOfType(target, typeof(IBusinessService));
        }

        #region Test Add
        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        //public void TestRoleRepositoryAdd_MustThrowArgumentNullExceptionGivenNullParameter()
        //{
        //    IBusinessService target = _autoResolver.Resolve<IBusinessService>();
        //    target.Add(null);
        //}

        [TestMethod]
        public void TestBusinessServiceAdd_MustCreateANewBusinessService()
        {
            BusinessServiceRequest businessServiceRequest = new BusinessServiceRequest();
            BusinessAm businessAm = new BusinessAm
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
            };
            businessServiceRequest.AuthorizationContext.BusinessId = "BusinessId1";
            businessServiceRequest.AuthorizationContext.UserId = "UserId1";
            businessServiceRequest.EntityId = "111";
            businessServiceRequest.Business = businessAm;

            IBusinessService target = _autoResolver.Resolve<IBusinessService>();
            target.Add(businessServiceRequest);

            BusinessResponse businessResponse = target.GetById(businessServiceRequest);

            Assert.IsNotNull(businessResponse);
            Assert.AreEqual("BusinessName", businessResponse.Business.Name);
        }

        [TestMethod]
        public void TestBusinessServiceAdd_MustNotCreateANewBusinessDueToNullBusinessObject()
        {
            BusinessServiceRequest businessServiceRequest = new BusinessServiceRequest();
            BusinessAm businessAm = null;
            businessServiceRequest.Business = businessAm;

            IBusinessService target = _autoResolver.Resolve<IBusinessService>();
            target.Add(businessServiceRequest);
            
            BusinessResponse businessResponse = target.GetById(businessServiceRequest);

            Assert.IsNotNull(businessResponse);
        }

        #endregion


    }
}
