using System;
using KhanyisaIntel.Kbit.Framework.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Exception;
using KhanyisaIntel.Kbit.Framework.Security.Application.Models;
using KhanyisaIntel.Kbit.Framework.Security.Domain.Factories.User;
using KhanyisaIntel.Kbit.Framework.Security.Domain.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Security.Domain.Factories.Tests
{
    [TestClass]
    public class UserFactoryTests
    {
        private IAutoResolver _iocContainer;

        public UserFactoryTests()
        {
            this._iocContainer = new IocContainer();

        }
        [TestMethod]
        public void TestCanResolveDependency()
        {
            IDomainFactory<User, UserAm> target = this._iocContainer.Resolve<IDomainFactory<User, UserAm>>();

            Assert.IsNotNull(target);
            Assert.IsInstanceOfType(target,typeof(IDomainFactory<User, UserAm>));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestBuildDomainEntityType_MustFailGivenNullArgument()
        {
            IDomainFactory<User, UserAm> target = this._iocContainer.Resolve<IDomainFactory<User, UserAm>>();
            target.BuildDomainEntityType(null);
        }

        [TestMethod]
        [ExpectedException(typeof(KbitRequiredFieldValidationException))]
        public void TestBuildDomainEntityType_MustFailGivenInvalidApplicationModelProperty()
        {
            IDomainFactory<User, UserAm> target = this._iocContainer.Resolve<IDomainFactory<User, UserAm>>();

            UserAm userAm = new UserAm();
            //All properties are not set, it will fail.

            target.BuildDomainEntityType(userAm);
        }

        [TestMethod]
        [ExpectedException(typeof(KbitRequiredFieldValidationException))]
        public void TestBuildDomainEntityType_MustFailGivenInvalidRole()
        {
            IDomainFactory<User, UserAm> target = this._iocContainer.Resolve<IDomainFactory<User, UserAm>>();

            UserAm userAm = new UserAm();
            userAm.Name = "Steveland";
            userAm.Code = null;
            userAm.Email = "steveland@khanyisaintel.co.za";
            userAm.AccountStatus = "Active Account Status";
            userAm.LicenseSpecification = "Perpertual License Specification";
            userAm.Password = "P@ssW0rd1";
            userAm.PasswordResetPolicy = "Never Reset Password Reset Policy";
            userAm.RoleId = "NonExistantRole";

            //All properties are not set, it will fail.

            target.BuildDomainEntityType(userAm);
        }
    }
}
