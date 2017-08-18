using System;
using System.Configuration;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Exception;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities.Encryption;
using KhanyisaIntel.Kbit.Framework.Security.Domain.AspNet;
using KhanyisaIntel.Kbit.Framework.Security.Repository.Database;
using KhanyisaIntel.Kbit.Framework.Security.Repository.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Framework.Tests.Security.Repository.Tests
{
    [TestClass]
    public class AspNetPrincipleUserRepositoryTests
    {
        private IAutoResolver _iocContainer;

        public AspNetPrincipleUserRepositoryTests()
        {
            this._iocContainer = new IocContainer();
        }

        // Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void MyTestInitialize()
        {
            //will drop and recreate the test database after each test...
            SecurityDatabaseContext context = new SecurityDatabaseContext(
              ConfigurationManager.ConnectionStrings["KBITDB"].ConnectionString);
        }
        
        // Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
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
            IAspNetPrincipleUserRepository repository = this._iocContainer.Resolve<IAspNetPrincipleUserRepository>();
            repository.Add(null);
        }

        [TestMethod]
        public void TestAdd_MustAddSuccessfully()
        {
            IAspNetPrincipleUserRepository repository = this._iocContainer.Resolve<IAspNetPrincipleUserRepository>();

            AspNetPrincipleUser entity =new AspNetPrincipleUser();
            entity.UserName = "Test User";
            entity.LowerCaseUserName = entity.UserName.ToLower();
            entity.EmailAddress = "tesTt@test.co.za";
            entity.LowerCaseEmailAddress = entity.EmailAddress.ToLower();
            entity.EmailAddressConfirmed = false;
            entity.PhoneNumber = null;
            entity.PhoneNumberConfirmed = false;
            entity.PasswordHash = AspNetCryptology.HashPassword("P@ssW0rd1");
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
            Assert.IsTrue(AspNetCryptology.VerifyHashedPassword(saveEntity.PasswordHash, "P@ssW0rd1"));
            Assert.AreEqual(entity.SecurityStamp, saveEntity.SecurityStamp);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityAlreadyExistException))]
        public void TestAdd_MustFailToAddGivenEntityAlreadyExists()
        {
            IAspNetPrincipleUserRepository repository = this._iocContainer.Resolve<IAspNetPrincipleUserRepository>();

            AspNetPrincipleUser entity = new AspNetPrincipleUser();
            entity.UserName = "Test User";
            entity.LowerCaseUserName = entity.UserName.ToLower();
            entity.EmailAddress = "tesTt@test.co.za";
            entity.LowerCaseEmailAddress = entity.EmailAddress.ToLower();
            entity.EmailAddressConfirmed = false;
            entity.PhoneNumber = null;
            entity.PhoneNumberConfirmed = false;
            entity.PasswordHash = AspNetCryptology.HashPassword("P@ssW0rd1");
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
            IAspNetPrincipleUserRepository repository = this._iocContainer.Resolve<IAspNetPrincipleUserRepository>();

            AspNetPrincipleUser entity = new AspNetPrincipleUser();
            entity.UserName = "Test User";
            entity.LowerCaseUserName = entity.UserName.ToLower();
            entity.EmailAddress = "tesTt@test.co.za";
            entity.LowerCaseEmailAddress = entity.EmailAddress.ToLower();
            entity.EmailAddressConfirmed = false;
            entity.PhoneNumber = null;
            entity.PhoneNumberConfirmed = false;
            entity.PasswordHash = AspNetCryptology.HashPassword("P@ssW0rd1");
            entity.SecurityStamp = Guid.NewGuid().ToString();
            entity.LockoutEnabled = false;
            entity.AccessFailedCount = 0;
            entity.TwoFactorEnabled = false;

            AspNetPrincipleUser entity2 = new AspNetPrincipleUser();
            entity2.EmailAddress = entity.EmailAddress;

            repository.Add(entity);

            repository.Add(entity2);
        }
    }
}
