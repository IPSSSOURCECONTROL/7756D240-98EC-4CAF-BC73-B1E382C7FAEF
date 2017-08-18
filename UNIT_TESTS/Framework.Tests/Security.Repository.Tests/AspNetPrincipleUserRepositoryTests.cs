using System;
using System.Configuration;
using KhanyisaIntel.Kbit.Framework.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Encryption;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Exception;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Workflow.Exceptions;
using KhanyisaIntel.Kbit.Framework.Security.Domain.AspNet;
using KhanyisaIntel.Kbit.Framework.Security.Repository.Database;
using KhanyisaIntel.Kbit.Framework.Security.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.Tests.TEST_UTILS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KhanyisaIntel.Kbit.Framework.Tests.Security.Repository.Tests
{
    [TestClass]
    public class AspNetPrincipleUserRepositoryTests
    {
        private IAutoResolver _iocContainer;
        private readonly IAspNetCryptology _aspNetCryptology;
        public AspNetPrincipleUserRepositoryTests()
        {
            this._iocContainer = new IocContainer();
            this._aspNetCryptology = this._iocContainer.Resolve<IAspNetCryptology>();
        }

        [TestMethod]
        public void TestCanResolveDependency()
        {
            IAspNetPrincipleUserRepository target = this._iocContainer.Resolve<IAspNetPrincipleUserRepository>();

            Assert.IsNotNull(target);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAdd_MustFailGivenNullEntity()
        {
            UnitTestContext.Initialize();
            IAspNetPrincipleUserRepository repository = this._iocContainer.Resolve<IAspNetPrincipleUserRepository>();
            repository.Add(null);
        }

        [TestMethod]
        public void TestAdd_MustAddSuccessfully()
        {
            UnitTestContext.Initialize();
            IAspNetPrincipleUserRepository repository = this._iocContainer.Resolve<IAspNetPrincipleUserRepository>();
 
            AspNetPrincipleUser entity =new AspNetPrincipleUser();
            entity.UserName = "Test User";
            entity.LowerCaseUserName = entity.UserName.ToLower();
            entity.EmailAddress = "tesTt@test.co.za";
            entity.LowerCaseEmailAddress = entity.EmailAddress.ToLower();
            entity.EmailAddressConfirmed = false;
            entity.PhoneNumber = null;
            entity.PhoneNumberConfirmed = false;
            entity.PasswordHash = this._aspNetCryptology.HashPassword("P@ssW0rd1");
            entity.SecurityStamp = Guid.NewGuid().ToString();
            entity.LockoutEnabled = false;
            entity.AccessFailedCount = 0;
            entity.TwoFactorEnabled = false;

            repository.Add(entity);

            AspNetPrincipleUser saveEntity = repository.GetById(entity.Id);

            Assert.IsNotNull(saveEntity);
            Assert.AreEqual(entity.UserName, saveEntity.UserName);
            Assert.AreEqual(entity.LowerCaseUserName, saveEntity.LowerCaseUserName);
            Assert.AreEqual(entity.EmailAddress, saveEntity.EmailAddress);
            Assert.AreEqual(entity.LowerCaseEmailAddress, saveEntity.LowerCaseEmailAddress);
            Assert.IsTrue(this._aspNetCryptology.VerifyHashedPassword(saveEntity.PasswordHash, "P@ssW0rd1"));
            Assert.AreEqual(entity.SecurityStamp, saveEntity.SecurityStamp);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityAlreadyExistException))]
        public void TestAdd_MustFailToAddGivenEntityAlreadyExists()
        {
            UnitTestContext.Initialize();
            IAspNetPrincipleUserRepository repository = this._iocContainer.Resolve<IAspNetPrincipleUserRepository>();

            AspNetPrincipleUser entity = new AspNetPrincipleUser();
            entity.UserName = "Test User";
            entity.LowerCaseUserName = entity.UserName.ToLower();
            entity.EmailAddress = "tesTt@test.co.za";
            entity.LowerCaseEmailAddress = entity.EmailAddress.ToLower();
            entity.EmailAddressConfirmed = false;
            entity.PhoneNumber = null;
            entity.PhoneNumberConfirmed = false;
            entity.PasswordHash = this._aspNetCryptology.HashPassword("P@ssW0rd1");
            entity.SecurityStamp = Guid.NewGuid().ToString();
            entity.LockoutEnabled = false;
            entity.AccessFailedCount = 0;
            entity.TwoFactorEnabled = false;

            repository.Add(entity);
            repository.Add(entity);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityAlreadyExistException))]
        public void TestAdd_MustFailToAddGivenEntityEmailAddressAlreadyExists()
        {
            UnitTestContext.Initialize();
            IAspNetPrincipleUserRepository repository = this._iocContainer.Resolve<IAspNetPrincipleUserRepository>();

            AspNetPrincipleUser entity = new AspNetPrincipleUser();
            entity.UserName = "Test User";
            entity.LowerCaseUserName = entity.UserName.ToLower();
            entity.EmailAddress = "tesTt@test.co.za";
            entity.LowerCaseEmailAddress = entity.EmailAddress.ToLower();
            entity.EmailAddressConfirmed = false;
            entity.PhoneNumber = null;
            entity.PhoneNumberConfirmed = false;
            entity.PasswordHash = this._aspNetCryptology.HashPassword("P@ssW0rd1");
            entity.SecurityStamp = Guid.NewGuid().ToString();
            entity.LockoutEnabled = false;
            entity.AccessFailedCount = 0;
            entity.TwoFactorEnabled = false;

            AspNetPrincipleUser entity2 = new AspNetPrincipleUser();
            entity2.EmailAddress = entity.EmailAddress;

            repository.Add(entity);

            repository.Add(entity2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdate_MustFailGivenNullEntity()
        {
            UnitTestContext.Initialize();
            IAspNetPrincipleUserRepository repository = this._iocContainer.Resolve<IAspNetPrincipleUserRepository>();
            repository.Update(null);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestUpdate_MustFailToUpdateGivenEntityDoesNotExist()
        {
            UnitTestContext.Initialize();
            IAspNetPrincipleUserRepository repository = this._iocContainer.Resolve<IAspNetPrincipleUserRepository>();

            AspNetPrincipleUser entity = new AspNetPrincipleUser();
            entity.UserName = "Test User";
            entity.LowerCaseUserName = entity.UserName.ToLower();
            entity.EmailAddress = "tesTt@test.co.za";
            entity.LowerCaseEmailAddress = entity.EmailAddress.ToLower();
            entity.EmailAddressConfirmed = false;
            entity.PhoneNumber = null;
            entity.PhoneNumberConfirmed = false;
            entity.PasswordHash = this._aspNetCryptology.HashPassword("P@ssW0rd1");
            entity.SecurityStamp = Guid.NewGuid().ToString();
            entity.LockoutEnabled = false;
            entity.AccessFailedCount = 0;
            entity.TwoFactorEnabled = false;

            repository.Update(entity);
        }

        [TestMethod]
        public void TestUpdate_MustAddAndUpdateEntitySuccessfully()
        {
            UnitTestContext.Initialize();
            IAspNetPrincipleUserRepository repository = this._iocContainer.Resolve<IAspNetPrincipleUserRepository>();

            AspNetPrincipleUser entity = new AspNetPrincipleUser();
            entity.UserName = "Test User";
            entity.LowerCaseUserName = entity.UserName.ToLower();
            entity.EmailAddress = "tesTt@test.co.za";
            entity.LowerCaseEmailAddress = entity.EmailAddress.ToLower();
            entity.EmailAddressConfirmed = false;
            entity.PhoneNumber = null;
            entity.PhoneNumberConfirmed = false;
            entity.PasswordHash = this._aspNetCryptology.HashPassword("P@ssW0rd1");
            entity.SecurityStamp = Guid.NewGuid().ToString();
            entity.LockoutEnabled = false;
            entity.AccessFailedCount = 0;
            entity.TwoFactorEnabled = false;

            repository.Add(entity);

            AspNetPrincipleUser saveEntity = repository.GetById(entity.Id);

            Assert.IsNotNull(saveEntity);
            Assert.AreEqual(entity.UserName, saveEntity.UserName);
            Assert.AreEqual(entity.LowerCaseUserName, saveEntity.LowerCaseUserName);
            Assert.AreEqual(entity.EmailAddress, saveEntity.EmailAddress);
            Assert.AreEqual(entity.LowerCaseEmailAddress, saveEntity.LowerCaseEmailAddress);
            Assert.IsTrue(this._aspNetCryptology.VerifyHashedPassword(saveEntity.PasswordHash, "P@ssW0rd1"));
            Assert.AreEqual(entity.SecurityStamp, saveEntity.SecurityStamp);

            entity.UserName = "Goodwill Gumede";
            entity.EmailAddress = "asdasdsd@asdasdasd.co.za";

            repository.Update(entity);

            AspNetPrincipleUser updatedEntity = repository.GetById(entity.Id);

            Assert.IsNotNull(updatedEntity);
            Assert.AreEqual(entity.UserName, updatedEntity.UserName);
            Assert.AreEqual(entity.LowerCaseUserName, updatedEntity.LowerCaseUserName);
            Assert.AreEqual(entity.EmailAddress, updatedEntity.EmailAddress);
            Assert.AreEqual(entity.LowerCaseEmailAddress, updatedEntity.LowerCaseEmailAddress);
            Assert.IsTrue(this._aspNetCryptology.VerifyHashedPassword(updatedEntity.PasswordHash, "P@ssW0rd1"));
            Assert.AreEqual(entity.SecurityStamp, updatedEntity.SecurityStamp);
        }
    }
}
