using System.Configuration;
using KhanyisaIntel.Kbit.Framework.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Infrustructure.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Security.Application.Models;
using KhanyisaIntel.Kbit.Framework.Security.Application.Services.User;
using KhanyisaIntel.Kbit.Framework.Security.Domain.Role;
using KhanyisaIntel.Kbit.Framework.Security.Repository.Database;
using KhanyisaIntel.Kbit.Framework.Security.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.Tests.TEST_UTILS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KhanyisaIntel.Kbit.Framework.Tests.Security.Application.Services
{
    [TestClass]
    public class UserServiceTests
    {
        private IAutoResolver _iocContainer;

        public UserServiceTests()
        {
            this._iocContainer = new IocContainer();
        }

        [TestMethod]
        public void TestCanResolveDependency()
        {
            UnitTestContext.Initialize();
            IUserService target = this._iocContainer.Resolve<IUserService>();

            Assert.IsNotNull(target);
            Assert.IsInstanceOfType(target, typeof(IUserService));
        }

        [TestMethod]
        public void TestAdd_MustFailGivenNullRequest()
        {
            UnitTestContext.Initialize();

            IUserService target = this._iocContainer.Resolve<IUserService>();

            UserResponse response = target.Add(null);

            Assert.IsNotNull(response);
            Assert.AreEqual(ServiceResult.Error, response.ServiceResult);
            Assert.AreEqual("Null request recieved.",response.Message);
        }

        [TestMethod]
        public void TestAdd_MustFailGivenNullUser()
        {
            UnitTestContext.Initialize();

            IUserService target = this._iocContainer.Resolve<IUserService>();

            UserServiceRequest request = new UserServiceRequest();
            request.User = null;

            UserResponse response = target.Add(request);

            Assert.IsNotNull(response);
            Assert.AreEqual(ServiceResult.Error, response.ServiceResult);
            Assert.AreEqual("User is a required field.", response.Message);
        }

        [TestMethod]
        public void TestAdd_MustAddUserSuccessfully()
        {
            UnitTestContext.Initialize();

            IUserService target = this._iocContainer.Resolve<IUserService>();

            this._iocContainer.Resolve<IRoleRepository>().Add(new SupermanRole());

            UserServiceRequest request = new UserServiceRequest();

            UserAm userAm = new UserAm
            {
                Name = "Steveland",
                Code = null,
                Email = "steveland@khanyisaintel.co.za",
                AccountStatus = "Active Account Status",
                LicenseSpecification = "Perpertual License Specification",
                Password = "P@ssW0rd1",
                PasswordResetPolicy = "Never Reset Password Reset Policy",
                RoleId = nameof(SupermanRole)
            };

            request.User = userAm;

            UserResponse response = target.Add(request);

            Assert.IsNotNull(response);
            Assert.AreEqual(ServiceResult.Success, response.ServiceResult);
            Assert.IsTrue(response.Message.Contains(" sucessfully saved."));
        }

        [TestMethod]
        public void TestAdd_MustFailAAddingUSerWithAnEmailAddressThatAlreadyExistsExists()
        {
            UnitTestContext.Initialize();

            IUserService target = this._iocContainer.Resolve<IUserService>();

            this._iocContainer.Resolve<IRoleRepository>().Add(new SupermanRole());

            UserServiceRequest request = new UserServiceRequest();

            UserAm userAm = new UserAm
            {
                Name = "Steveland",
                Code = null,
                Email = "steveland@khanyisaintel.co.za",
                AccountStatus = "Active Account Status",
                LicenseSpecification = "Perpertual License Specification",
                Password = "P@ssW0rd1",
                PasswordResetPolicy = "Never Reset Password Reset Policy",
                RoleId = nameof(SupermanRole)
            };

            request.User = userAm;

            UserResponse response = target.Add(request);

            Assert.IsNotNull(response);
            Assert.AreEqual(ServiceResult.Success, response.ServiceResult);
            Assert.IsTrue(response.Message.Contains(" sucessfully saved."));

            response = target.Add(request);

            Assert.IsNotNull(response);
            Assert.AreEqual(ServiceResult.Exception, response.ServiceResult);
            Assert.IsTrue(response.Message.Contains("Record with name 'steveland@khanyisaintel.co.za' already exists."));
        }
    }
}
