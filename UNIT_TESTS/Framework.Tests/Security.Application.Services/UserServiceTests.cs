using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Business;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Customer;
using KhanyisaIntel.Kbit.Framework.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Infrustructure.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;
using KhanyisaIntel.Kbit.Framework.Security.Application.Models;
using KhanyisaIntel.Kbit.Framework.Security.Application.Services.User;
using KhanyisaIntel.Kbit.Framework.Security.Domain.Role;
using KhanyisaIntel.Kbit.Framework.Security.Domain.User;
using KhanyisaIntel.Kbit.Framework.Security.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.Tests.TEST_UTILS;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities;

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

        #region Test Add
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
            request.ApplicationModel = null;

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
                Name = "TestingSomething",
                Code = "123456",
                Email = "test@test.co.za",
                AccountStatus = "Active Account Status",
                LicenseSpecification = "Perpertual License Specification",
                Password = "P@ssW0rd1",
                PasswordResetPolicy = "Never Reset Password Reset Policy",
                Role = nameof(SupermanRole)
            };

            request.ApplicationModel = userAm;

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
                Role = nameof(SupermanRole)
            };

            request.ApplicationModel = userAm;

            UserResponse response = target.Add(request);

            Assert.IsNotNull(response);
            Assert.AreEqual(ServiceResult.Success, response.ServiceResult);
            Assert.IsTrue(response.Message.Contains(" sucessfully saved."));

            response = target.Add(request);

            Assert.IsNotNull(response);
            Assert.AreEqual(ServiceResult.Exception, response.ServiceResult);
            Assert.IsTrue(response.Message.Contains("Record with name 'steveland@khanyisaintel.co.za' already exists."));
        }
        #endregion

        #region Test Update
        [TestMethod]
        public void TestUpdate_MustFailGivenNullRequest()
        {
            UnitTestContext.Initialize();

            IUserService target = this._iocContainer.Resolve<IUserService>();

            UserResponse response = target.Update(null);

            Assert.IsNotNull(response);
            Assert.AreEqual(ServiceResult.Error, response.ServiceResult);
            Assert.AreEqual("Null request recieved.", response.Message);
        }

        [TestMethod]
        public void TestUpdate_MustFailGivenNullUser()
        {
            UnitTestContext.Initialize();

            IUserService target = this._iocContainer.Resolve<IUserService>();

            UserServiceRequest request = new UserServiceRequest();
            request.ApplicationModel = null;

            UserResponse response = target.Update(request);

            Assert.IsNotNull(response);
            Assert.AreEqual(ServiceResult.Error, response.ServiceResult);
            Assert.AreEqual("User is a required field.", response.Message);
        }

        [TestMethod]
        public void TestUpdate_MustUpdateUserSuccessfully()
        {
            UnitTestContext.Initialize();

            IUserService target = this._iocContainer.Resolve<IUserService>();

            this._iocContainer.Resolve<IRoleRepository>().Add(new SupermanRole());
            
            UserResponse updatedResponse = target.Update(new UserServiceRequest()
            {
                ApplicationModel = new UserAm()
                {
                    Name = "Stevland",
                    Code = "123456",
                    Email = "stevland@updated.co.za",
                    AccountStatus = "Active Account Status",
                    LicenseSpecification = "Perpertual License Specification",
                    Password = "P@ssW0rd1",
                    PasswordResetPolicy = "Never Reset Password Reset Policy",
                    Role = nameof(SupermanRole),
                    Id = "59a5a057e3496b1becd3ab84"
                }
            });

            Assert.IsNotNull(updatedResponse);
            Assert.AreEqual(ServiceResult.Success, updatedResponse.ServiceResult);
            Assert.IsTrue(updatedResponse.Message.Contains(" sucessfully updated."));
        }

        [TestMethod]
        public void TestUpdate_MustFailUpdatingUserWithAnEmailAddressThatAlreadyExistsExists()
        {
            UnitTestContext.Initialize();

            IUserService target = this._iocContainer.Resolve<IUserService>();

            this._iocContainer.Resolve<IRoleRepository>().Add(new SupermanRole());

            UserServiceRequest request = new UserServiceRequest();

            UserAm userAm = new UserAm
            {
                Name = "newusertotest",
                Code = "123456",
                Email = "new@email.co.za",
                AccountStatus = "Active Account Status",
                LicenseSpecification = "Perpertual License Specification",
                Password = "P@ssW0rd1",
                PasswordResetPolicy = "Never Reset Password Reset Policy",
                Role = nameof(SupermanRole)
            };

            request.ApplicationModel = userAm;

            UserResponse response = target.Add(request);

            UserResponse updatedResponse = target.Update(new UserServiceRequest()
            {
                ApplicationModel = new UserAm()
                {
                    Name = "New User To Update",
                    Code = "123456",
                    Email = "new@email.co.za",
                    AccountStatus = "Active Account Status",
                    LicenseSpecification = "Perpertual License Specification",
                    Password = "P@ssW0rd1",
                    PasswordResetPolicy = "Never Reset Password Reset Policy",
                    Role = nameof(SupermanRole),
                    Id = "59adc20de349893070821ec6"
                }
            });

            Assert.IsNotNull(updatedResponse);
            Assert.AreEqual(ServiceResult.Exception, updatedResponse.ServiceResult);
        }
        #endregion

        #region Test GetAll
        [TestMethod]
        public void TestCanGeAllCustomer_MustGetAllSuccessfully()
        {
            UnitTestContext.Initialize();
            IUserService service = this._iocContainer.Resolve<IUserService>();

            UserServiceRequest serviceRequest = new UserServiceRequest();

            UserResponse userResponse = service.GetAll(serviceRequest);

            Assert.IsNotNull(userResponse);
            Assert.IsNotNull(userResponse.ApplicationModels);
            Assert.AreNotEqual(0, userResponse.ApplicationModels.Count());
            Assert.IsNotNull(userResponse.ApplicationModels.First().Name);
            Assert.IsNotNull(userResponse.ApplicationModels.First().Email);
            Assert.IsNotNull(userResponse.ApplicationModels.First().Code);
            Assert.IsNotNull(userResponse.ApplicationModels.First().Password);
            Assert.IsNotNull(userResponse.ApplicationModels.First().AccountStatus);
            Assert.IsNotNull(userResponse.ApplicationModels.First().LicenseSpecification);
            Assert.IsNotNull(userResponse.ApplicationModels.First().PasswordResetPolicy);
            Assert.IsNotNull(userResponse.ApplicationModels.First().Role);

            Assert.IsNotNull(userResponse.ApplicationModels.Last().Name);
            Assert.IsNotNull(userResponse.ApplicationModels.Last().Email);
            Assert.IsNotNull(userResponse.ApplicationModels.Last().Code);
            Assert.IsNotNull(userResponse.ApplicationModels.Last().Password);
            Assert.IsNotNull(userResponse.ApplicationModels.Last().AccountStatus);
            Assert.IsNotNull(userResponse.ApplicationModels.Last().LicenseSpecification);
            Assert.IsNotNull(userResponse.ApplicationModels.Last().PasswordResetPolicy);
            Assert.IsNotNull(userResponse.ApplicationModels.Last().Role);
        }
        #endregion

        #region Test GetById
        [TestMethod]
        public void TestCanGetCustomerById_MustGetByIdSuccessfully()
        {
            UnitTestContext.Initialize();
            IUserService service = this._iocContainer.Resolve<IUserService>();
            IDomainFactory<User, UserAm> factory = this._iocContainer.Resolve<IDomainFactory<User, UserAm>>();
            IUserRepository repository = this._iocContainer.Resolve<IUserRepository>();

            Assert.IsNotNull(service);

            UserAm userAm = new UserAm()
            {
                Name = "UNIT 1",
                Code = "OUT OF BOUNDS",
                Email = "VonBackstromBoulevards@gmail.co.za",
                AccountStatus = "Active Account Status",
                LicenseSpecification = "Perpertual License Specification",
                Password = "P@ssW0rd1",
                PasswordResetPolicy = "Never Reset Password Reset Policy",
                Role = nameof(SupermanRole)
            };

            User user = factory.BuildDomainEntityType(userAm);
            repository.Add(user);

            UserServiceRequest serviceRequest = new UserServiceRequest();
            serviceRequest.EntityId = user.Id;

            UserResponse customerResponse = service.GetById(serviceRequest);

            Assert.IsNotNull(customerResponse);
            Assert.AreEqual(userAm.Name, customerResponse.ApplicationModel.Name);
            Assert.AreEqual(userAm.Code, customerResponse.ApplicationModel.Code);
            Assert.AreEqual(userAm.Email, customerResponse.ApplicationModel.Email);
            Assert.AreEqual(userAm.AccountStatus, customerResponse.ApplicationModel.AccountStatus);
            Assert.AreEqual(userAm.LicenseSpecification, customerResponse.ApplicationModel.LicenseSpecification);
            Assert.AreEqual(userAm.Password, customerResponse.ApplicationModel.Password);
            Assert.AreEqual(userAm.PasswordResetPolicy, customerResponse.ApplicationModel.PasswordResetPolicy);
            Assert.AreEqual(userAm.Role.InsertSpaceAfterCapitalLetter(), customerResponse.ApplicationModel.Role);
        }
        #endregion
    }
}
