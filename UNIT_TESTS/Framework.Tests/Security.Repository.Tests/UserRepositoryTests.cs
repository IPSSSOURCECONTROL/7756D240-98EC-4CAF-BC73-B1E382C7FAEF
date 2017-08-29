using KhanyisaIntel.Kbit.Framework.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Security.Domain;
using KhanyisaIntel.Kbit.Framework.Security.Domain.LicenseSpecification;
using KhanyisaIntel.Kbit.Framework.Security.Domain.Role;
using KhanyisaIntel.Kbit.Framework.Security.Domain.User;
using KhanyisaIntel.Kbit.Framework.Security.Domain.User.AccountStatusTypes;
using KhanyisaIntel.Kbit.Framework.Security.Domain.User.PasswordTypes;
using KhanyisaIntel.Kbit.Framework.Security.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.Tests.TEST_UTILS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KhanyisaIntel.Kbit.Framework.Tests.Security.Repository.Tests
{
    [TestClass]
    public class UserRepositoryTests
    {
        private IAutoResolver _iocContainer;

        public UserRepositoryTests()
        {
            this._iocContainer = new IocContainer();
        }

        [TestMethod]
        public void TestCanResolveDependency()
        {
            IUserRepository target = this._iocContainer.Resolve<IUserRepository>();

            Assert.IsNotNull(target);
        }

        [TestMethod]
        public void TestCanAddGetModifyDeleteUser()
        {
            UnitTestContext.Initialize();

            IUserRepository repository = this._iocContainer.Resolve<IUserRepository>();
            //repository.SetSecurityContext(AutomatedTestingContext.SupermanAuthorizationContext);

            User user = SecurityDomianFactory.CreateUser(new CreateUserParameters()
            {
                AccountStatus = new ActiveAccountStatus(),
                Code = "CODE12",
                Email = "goodwillgumede@yahoo.co.za",
                Password = "P@ssWord1",
                Name = "Goodwill",
                PasswordResetPolicy = new DailyPasswordResetPolicy(),
                Role = new AdministratorRole()
            });

            user.SetLicense(new AnnualLicenseSpecification());

            repository.Add(user);

            User savedUser = repository.GetById(user.Id);

            Assert.IsNotNull(savedUser);
            Assert.AreEqual("Goodwill", savedUser.Name);

            savedUser.Name = "Mthokozisi";
            savedUser.Code = "DHC";
            savedUser.Email = "hdsddf@fsfklsdfsdf";

            repository.Update(savedUser);

            User updatedUser = repository.GetById(user.Id);

            Assert.IsNotNull(updatedUser);
            Assert.AreEqual("Mthokozisi", updatedUser.Name);

            repository.Delete(updatedUser);

            User deletedUser = repository.GetById(updatedUser.Id);

            Assert.IsNull(deletedUser);

            UnitTestContext.Initialize();
        }

        [TestMethod]
        public void TestAdduserQuicklysdsdfd()
        {
            UnitTestContext.Initialize();

            IUserRepository repository = this._iocContainer.Resolve<IUserRepository>();
            //repository.SetSecurityContext(AutomatedTestingContext.SupermanAuthorizationContext);

            User user = SecurityDomianFactory.CreateUser(new CreateUserParameters()
            {
                AccountStatus = new ActiveAccountStatus(),
                Code = "CODE12",
                Email = "goodwillgumede@yahoo.co.za",
                Password = "P@ssWord1",
                Name = "Goodwill",
                PasswordResetPolicy = new DailyPasswordResetPolicy(),
                Role = new AdministratorRole()
            });

            user.SetLicense(new AnnualLicenseSpecification());

            repository.Add(user);

            User savedUser = repository.GetById(user.Id);

            Assert.IsNotNull(savedUser);
            Assert.AreEqual("Goodwill", savedUser.Name);

            UnitTestContext.Initialize();
        }
    }
}
