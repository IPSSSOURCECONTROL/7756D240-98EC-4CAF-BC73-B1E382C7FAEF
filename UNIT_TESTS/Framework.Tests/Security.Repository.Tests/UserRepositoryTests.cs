using System;
using KhanyisaIntel.Kbit.Framework.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Exception;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Workflow.Exceptions;
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

        #region Add
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUserAdd_MustReturnArgumentExceptionGivenNullParameter()
        {
            UnitTestContext.Initialize();

            IUserRepository repository = this._iocContainer.Resolve<IUserRepository>();
            
            repository.Add(null);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityAlreadyExistException))]
        public void TestUserRepositoryAdd_MustFailDueToUserAlreadyExisting()
        {
            UnitTestContext.Initialize();

            IUserRepository repository = this._iocContainer.Resolve<IUserRepository>();
            //repository.SetSecurityContext(AutomatedTestingContext.SupermanAuthorizationContext);

            User user = SecurityDomianFactory.CreateUser(new CreateUserParameters()
            {
                AccountStatus = new ActiveAccountStatus(),
                Code = "123",
                Email = "goodwillgumede@yahoo.co.za",
                Password = "P@ssWord1",
                Name = "Goodwill",
                PasswordResetPolicy = new DailyPasswordResetPolicy(),
                Role = new AdministratorRole()
            });

            user.SetLicense(new AnnualLicenseSpecification());

            repository.Add(user);
            repository.Add(user);

            
            repository.Delete(user);

            UnitTestContext.Initialize();
        }

        [TestMethod]
        [ExpectedException(typeof(EntityAlreadyExistException))]
        public void TestUserRepositoryAdd_MustFailDueToEmailAlreadyExisting()
        {
            UnitTestContext.Initialize();

            IUserRepository repository = this._iocContainer.Resolve<IUserRepository>();
            //repository.SetSecurityContext(AutomatedTestingContext.SupermanAuthorizationContext);

            User user = SecurityDomianFactory.CreateUser(new CreateUserParameters()
            {
                AccountStatus = new ActiveAccountStatus(),
                Code = "123",
                Email = "123@yahoo.co.za",
                Password = "P@ssWord1",
                Name = "Test1",
                PasswordResetPolicy = new DailyPasswordResetPolicy(),
                Role = new AdministratorRole()
            });

            User user1 = SecurityDomianFactory.CreateUser(new CreateUserParameters()
            {
                AccountStatus = new ActiveAccountStatus(),
                Code = "123",
                Email = "123@yahoo.co.za",
                Password = "P@ssWord1",
                Name = "Test2",
                PasswordResetPolicy = new DailyPasswordResetPolicy(),
                Role = new AdministratorRole()
            });

            user.SetLicense(new AnnualLicenseSpecification());

            repository.Add(user);
            repository.Add(user1);


            repository.Delete(user);
            repository.Delete(user1);

            UnitTestContext.Initialize();
        }
        #endregion

        #region Update
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUserRepositoryUpdate_MustFailDueToNullUserParameter()
        {
            UnitTestContext.Initialize();

            IUserRepository repository = this._iocContainer.Resolve<IUserRepository>();
            //repository.SetSecurityContext(AutomatedTestingContext.SupermanAuthorizationContext);

            repository.Update(null);

            UnitTestContext.Initialize();
        }

        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestUserRepositoryUpdate_MustFailDueToUserNotExisting()
        {
            UnitTestContext.Initialize();

            IUserRepository repository = this._iocContainer.Resolve<IUserRepository>();
            //repository.SetSecurityContext(AutomatedTestingContext.SupermanAuthorizationContext);

            User user = SecurityDomianFactory.CreateUser(new CreateUserParameters()
            {
                AccountStatus = new ActiveAccountStatus(),
                Code = "789",
                Email = "abc@yahoo.co.za",
                Password = "P@ssWord1",
                Name = "Test1",
                PasswordResetPolicy = new DailyPasswordResetPolicy(),
                Role = new AdministratorRole()
            });

            user.SetLicense(new AnnualLicenseSpecification());

            repository.Update(user);

            UnitTestContext.Initialize();
        }

        [TestMethod]
        [ExpectedException(typeof(EntityAlreadyExistException))]
        public void TestUserRepositoryUpdate_MustFailDueToEmailAddressAlreadyExisting()
        {
            UnitTestContext.Initialize();

            IUserRepository repository = this._iocContainer.Resolve<IUserRepository>();
            //repository.SetSecurityContext(AutomatedTestingContext.SupermanAuthorizationContext);

            User user = SecurityDomianFactory.CreateUser(new CreateUserParameters()
            {
                AccountStatus = new ActiveAccountStatus(),
                Code = "789",
                Email = "abc@yahoo.co.za",
                Password = "P@ssWord1",
                Name = "Test1",
                PasswordResetPolicy = new DailyPasswordResetPolicy(),
                Role = new AdministratorRole()
            });

            user.SetLicense(new AnnualLicenseSpecification());

            repository.Add(user);
            repository.Update(user);

            UnitTestContext.Initialize();
        }
        #endregion

        #region Delete
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUserRepositoryDelete_MustFailDueToNullUserParameter()
        {
            UnitTestContext.Initialize();

            IUserRepository repository = this._iocContainer.Resolve<IUserRepository>();
            //repository.SetSecurityContext(AutomatedTestingContext.SupermanAuthorizationContext);
            
            repository.Delete(null);

            UnitTestContext.Initialize();
        }

        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestUserRepositoryDelete_MustFailDueToUserNotExisting()
        {
            UnitTestContext.Initialize();

            IUserRepository repository = this._iocContainer.Resolve<IUserRepository>();
            //repository.SetSecurityContext(AutomatedTestingContext.SupermanAuthorizationContext);

            User user = SecurityDomianFactory.CreateUser(new CreateUserParameters()
            {
                AccountStatus = new ActiveAccountStatus(),
                Code = "555",
                Email = "boo@yahoo.co.za",
                Password = "P@ssWord1",
                Name = "Test1",
                PasswordResetPolicy = new DailyPasswordResetPolicy(),
                Role = new AdministratorRole()
            });

            user.SetLicense(new AnnualLicenseSpecification());

            repository.Delete(user);

            UnitTestContext.Initialize();
        }
        #endregion
    }
}
