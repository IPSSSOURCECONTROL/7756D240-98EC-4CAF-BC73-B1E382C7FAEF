using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KhanyisaIntel.Kbit.Framework.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Configuration;
using KhanyisaIntel.Kbit.Framework.Infrustructure.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Reflection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Serialization;
using KhanyisaIntel.Kbit.Framework.Security.Application.Services.ApplicationFunction;
using KhanyisaIntel.Kbit.Framework.Security.Application.Services.Role;
using KhanyisaIntel.Kbit.Framework.Security.Domain.ApplicationFunction;
using KhanyisaIntel.Kbit.Framework.Security.Domain.LicenseSpecification;
using KhanyisaIntel.Kbit.Framework.Security.Domain.Role;
using KhanyisaIntel.Kbit.Framework.Security.Domain.User;
using KhanyisaIntel.Kbit.Framework.Security.Domain.User.AccountStatusTypes;
using KhanyisaIntel.Kbit.Framework.Security.Domain.User.PasswordTypes;
using KhanyisaIntel.Kbit.Framework.Security.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.Tests.TEST_UTILS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KhanyisaIntel.Kbit.Framework.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private Random _random = new Random();
        private IAutoResolver _iocContainer;

        public UnitTest1()
        {
            this._iocContainer = new IocContainer();
        }


        //[TestMethod]
        //public void Testasdasdasd()
        //{
        //    Address address = new Address("UNIT 1", "OUT OF BOUNDS",
        //        "Von Backstrom Boulevard", "Silverlakes", "Pretoria", "0081");

        //    ContactDetails contactDetails = new ContactDetails("goodwillgumede@yahoo.com",
        //        "0908897777", "0787776666");

        //    BillingInformation billingInformation = new BillingInformation("FNB",
        //        "6211134445267", "20568", "C200000");

        //    Business business = new Business("IPush Software Solutions", address, contactDetails, billingInformation);

        //    ProductListingWorkflowContext context = new ProductListingWorkflowContext();
        //    context.Entity = new CostEstimateListing(business, new Customer(address, contactDetails, new Representative("dsfsdf","sdfsdf"), billingInformation ) );

        //    IProductListingRepository repository = this._iocContainer.Resolve<IProductListingRepository>();

        //    repository.InvokeWorkflow(WorkflowOperation.Add, context);
        //}

        [TestMethod]
        public void TestCanAddRoleGetModifyAndDelete()
        {
            UnitTestContext.Initialize();
            IRoleRepository roleRepository = this._iocContainer.Resolve<IRoleRepository>();

            AdministratorRole administratorRole = new AdministratorRole();

            roleRepository.Add(administratorRole);

            Role saveAdministratorRole = roleRepository.GetById(administratorRole.Id);

            Assert.IsNotNull(saveAdministratorRole);

            Assert.IsTrue(saveAdministratorRole is AdministratorRole);

            Assert.AreEqual(administratorRole.Id, saveAdministratorRole.Id);

            roleRepository.Delete(administratorRole);

            Role deletedRole = roleRepository.GetById(administratorRole.Id);

            Assert.IsNull(deletedRole);
        }

        [TestMethod]
        public void TestCanGetAllRoles()
        {
            UnitTestContext.Initialize();
            IRoleRepository roleRepository = this._iocContainer.Resolve<IRoleRepository>();

            AdministratorRole administratorRole = new AdministratorRole();
            ManagerRole managerRole = new ManagerRole();
            LimitedRole limitedRole = new LimitedRole();
            NoRole noRole = new NoRole();

            roleRepository.Add(administratorRole);
            roleRepository.Add(managerRole);
            roleRepository.Add(limitedRole);
            roleRepository.Add(noRole);

            var roles = roleRepository.GetAll().ToList();

            Assert.IsNotNull(roles);

            Assert.IsTrue(roles.Count() == 4);

            foreach (Role role in roles)
            {
                roleRepository.Delete(role);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMustFailGivenNullEntityRole()
        {
            IRoleRepository roleRepository = this._iocContainer.Resolve<IRoleRepository>();
            roleRepository.Add(null);
        }

        [TestMethod]
        public void TestBlockUser()
        {
            User user = new User(
                "Goodwill",
                "CODE123",
                "goodwillgumede@yahoo.co.za", new Password("P@ssW0rd1", new DailyPasswordResetPolicy()),
                new AdministratorRole(), new InactiveAccountStatus("inactive account"));
            user.SetLicense(new AnnualLicenseSpecification());

            Assert.IsFalse(user.IsAccountActive());
            Assert.IsTrue(user.AccountStatus is InactiveAccountStatus);
            Assert.IsTrue(user.License is AnnualLicenseSpecification);
            Assert.AreEqual("P@ssW0rd1", user.Password.Value);
            Assert.IsTrue(user.Password.PasswordResetPolicy is DailyPasswordResetPolicy);
            Assert.IsTrue(user.Role is AdministratorRole);

            user.Activate();

            Assert.IsTrue(user.IsAccountActive());
            Assert.IsTrue(user.AccountStatus is ActiveAccountStatus);

            user.CheckLicense();
            Assert.IsTrue(user.IsLicenseValid());
        }

        [TestMethod]
        public void TestCanCreateClassFromAssembly()
        {
            IObjectActivator objectActivator = this._iocContainer.Resolve<IObjectActivator>();

            Role role = (Role)objectActivator.CreateInstanceOf<Role>("SupermanRole");

            Assert.IsInstanceOfType(role, typeof(SupermanRole));
        }

        [TestMethod]
        public void TestRoleService_GetByIdMustReurnErrorGivenNullRequest()
        {
            UnitTestContext.Initialize();
            IRoleService roleService = this._iocContainer.Resolve<IRoleService>();

            RoleResponse response = roleService.GetById(null);

            Assert.AreEqual(ServiceResult.Error, response.ServiceResult);
            Assert.AreEqual("Null request recieved.", response.Message);
        }

        [TestMethod]
        public void TestRoleService_GetById_MustReturnErrorGivenInvalidRoleId_EmptyString()
        {
            UnitTestContext.Initialize();
            IRoleService roleService = this._iocContainer.Resolve<IRoleService>();

            RoleResponse response = roleService.GetById(new RoleServiceRequest());

            Assert.AreEqual(ServiceResult.Error, response.ServiceResult);
            Assert.IsNull(response.ApplicationModel);
            Assert.AreEqual("Role with Id '' not found.", response.Message);
        }

        [TestMethod]
        public void TestRoleService_GetById_MustReturnErrorGivenInvalidRoleId_NonExistantRoleId()
        {
            UnitTestContext.Initialize();
            IRoleService roleService = this._iocContainer.Resolve<IRoleService>();

            RoleResponse response = roleService.GetById(new RoleServiceRequest() { EntityId = "TEST" });

            Assert.AreEqual(ServiceResult.Error, response.ServiceResult);
            Assert.IsNull(response.ApplicationModel);
            Assert.AreEqual("Role with Id 'TEST' not found.", response.Message);
        }

        [TestMethod]
        public void TestCanReadCustomApplicationConfigSection()
        {
            ApplicationFunctionsConfiguration applicationFunctionsConfiguration = new ApplicationFunctionsConfiguration();
            applicationFunctionsConfiguration.Functions.Add(new Function()
            {
                Name = "Customer Definition",
                NameSpace = "KhanyisaIntel.Kbit.Framework.Security.Domain.ApplicationFunction.ApplicationFunctions.BusinessIntelligence.CustomerFunctions"
            });
            applicationFunctionsConfiguration.Functions.Add(new Function()
            {
                Name = "User Definition",
                NameSpace = "KhanyisaIntel.Kbit.Framework.Security.Domain.ApplicationFunction.ApplicationFunctions.Security.UserFunctions"
            });
            applicationFunctionsConfiguration.BoundedContexts.Add(new BoundedContext()
            {
                Name = "BusinessIntelligence.Domain"
            });
            applicationFunctionsConfiguration.BoundedContexts.Add(new BoundedContext()
            {
                Name = "Security.Domain"
            });

            IObjectSerializer objectSerializer = new ObjectSerializer();
            objectSerializer.Serialize(typeof(ApplicationFunctionsConfiguration), applicationFunctionsConfiguration);

            ApplicationFunctionsConfiguration deserialized =
                objectSerializer.Deserialize<ApplicationFunctionsConfiguration>(File.ReadAllText(Environment.CurrentDirectory + "//ApplicationFunctionsConfiguration.xml"));

            Assert.IsNotNull(deserialized);
        }

        [TestMethod]
        public void TestCanAddReadUpdateDeleteApplicationFunctions()
        {
            UnitTestContext.Initialize();
            IApplicationFunctionService service = this._iocContainer.Resolve<IApplicationFunctionService>();

            ApplicationFunctionServiceRequest request = new ApplicationFunctionServiceRequest();
            request.ApplicationFunctionsConfigurationPath = Environment.CurrentDirectory + "//ApplicationFunctionsConfiguration.xml";

            ApplicationFunctionResponse response = service.Update(request);

            Assert.IsNotNull(response);
            Assert.AreEqual("Successfully updated all application functions. ", response.Message);

            IApplicationFunctionRepository repository = this._iocContainer.Resolve<IApplicationFunctionRepository>();

            IEnumerable<ApplicationFunction> savedApplicationFunctions = repository.GetAll();

            Assert.IsNotNull(savedApplicationFunctions);
            Assert.AreEqual(2, savedApplicationFunctions.Count());
            Assert.IsTrue(savedApplicationFunctions.Any(x => x.Name == "Customer Definition"));
            Assert.IsTrue(savedApplicationFunctions.Any(x => x.Name == "User Definition"));

            ApplicationFunction customerApplicationFunction = savedApplicationFunctions.First(x => x.Name == "Customer Definition");

            Assert.IsTrue(customerApplicationFunction.FunctionClassification.Count() == 4);

            ApplicationFunction userApplicationFunction = savedApplicationFunctions.First(x => x.Name == "User Definition");

            Assert.IsTrue(userApplicationFunction.FunctionClassification.Count() == 4);

            customerApplicationFunction.Name = "Update Name Test 1";
            userApplicationFunction.Name = "Update Name Test 2";

            repository.Update(customerApplicationFunction);
            repository.Update(userApplicationFunction);


            IEnumerable<ApplicationFunction> updatedApplicationFunctions = repository.GetAll();

            Assert.IsNotNull(updatedApplicationFunctions);
            Assert.AreEqual(2, updatedApplicationFunctions.Count());

            Assert.IsTrue(updatedApplicationFunctions.Any(x => x.Name == "Update Name Test 1"));
            Assert.IsTrue(updatedApplicationFunctions.Any(x => x.Name == "Update Name Test 2"));

            ApplicationFunction updatedCustomerApplicationFunction = updatedApplicationFunctions.First(x => x.Name == "Update Name Test 1");

            Assert.IsTrue(updatedCustomerApplicationFunction.FunctionClassification.Count() == 4);

            ApplicationFunction updatedUserApplicationFunction = updatedApplicationFunctions.First(x => x.Name == "Update Name Test 2");

            Assert.IsTrue(updatedUserApplicationFunction.FunctionClassification.Count() == 4);


            repository.Delete(updatedCustomerApplicationFunction);
            repository.Delete(updatedUserApplicationFunction);

            IEnumerable<ApplicationFunction> deletedApplicationFunctions = repository.GetAll();

            Assert.IsTrue(!deletedApplicationFunctions.Any());
        }
    }
}
