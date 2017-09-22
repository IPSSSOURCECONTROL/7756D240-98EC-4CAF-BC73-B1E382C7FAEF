using System.Configuration;
using System.Linq;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Business;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Customer;
using KhanyisaIntel.Kbit.Framework.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Infrustructure.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Security.Application.Models;
using KhanyisaIntel.Kbit.Framework.Security.Application.Services.User;
using KhanyisaIntel.Kbit.Framework.Security.Domain.Role;
using KhanyisaIntel.Kbit.Framework.Security.Repository.Database;
using KhanyisaIntel.Kbit.Framework.Security.Repository.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KhanyisaIntel.Kbit.Framework.Tests.TEST_UTILS
{
    public static class UnitTestContext
    {
        private static IAutoResolver _iocContainer;

        static UnitTestContext()
        {
     
        }

        public static void Initialize()
        {
            SecurityDatabaseContext context = new SecurityDatabaseContext(
              ConfigurationManager.ConnectionStrings["KBITDB"].ConnectionString);
        }

        public static void SetUpTestData()
        {
            _iocContainer = new IocContainer();

            IBusinessService service = _iocContainer.Resolve<IBusinessService>();
            _iocContainer.Resolve<IRoleRepository>().Add(new LimitedRole());
            _iocContainer.Resolve<IRoleRepository>().Add(new ManagerRole());
            _iocContainer.Resolve<IRoleRepository>().Add(new AdministratorRole());
            _iocContainer.Resolve<IRoleRepository>().Add(new SupermanRole());
            _iocContainer.Resolve<IRoleRepository>().Add(new NoRole());

            for (int i = 0; i < 20; i++)
            {
                BusinessResponse response = service.Add(new BusinessServiceRequest()
                {
                    ApplicationModel = new BusinessAm()
                    {
                        AddressLineOne = "UNIT 1",
                        AddressLineTwo = "OUT OF BOUNDS",
                        Street = "Von Backstrom Boulevard",
                        Suburb = "Silverlakes",
                        TownOrCity = "Pretoria",
                        PostalCode = "0081",
                        Email = $"testemail{i}@yahoo.co.za",
                        CellphoneNumber = "0721248899",
                        Bank = "FNB",
                        AccountNumber = "6211134445267",
                        BranchCode = "206658",
                        Reference = "aasasdasd",
                        Name = $"TEST BUSINESS NAME {i}"
                    }
                });

                Assert.IsNotNull(response);
                Assert.AreEqual(ServiceResult.Success, response.ServiceResult);
                //Assert.IsNotNull(response.Message);

                response = service.GetAll(new BusinessServiceRequest());

                Assert.IsNotNull(response);
                Assert.AreEqual(ServiceResult.Success, response.ServiceResult);

                BusinessAm savedBusinessAm =
                    response.ApplicationModels.FirstOrDefault(x => x.Name == $"TEST BUSINESS NAME {i}");


                LoadUsersForBusiness(savedBusinessAm);
            }
        }


        private static void LoadUsersForBusiness(BusinessAm savedBusinessAm)
        {
            for (int i = 0; i < 20; i++)
            {
                IUserService target = _iocContainer.Resolve<IUserService>();
                UserServiceRequest request = new UserServiceRequest();

                UserAm userAm = new UserAm
                {
                    Name = $"TESTUSER_{i}",
                    Code = null,
                    Email = $"testuser{i}@{savedBusinessAm.Name.Replace(" ", string.Empty).ToLower()}.co.za",
                    AccountStatus = "Active Account Status",
                    LicenseSpecification = "Perpertual License Specification",
                    Password = "P@ssW0rd1",
                    PasswordResetPolicy = "Never Reset Password Reset Policy",
                    Role = nameof(LimitedRole),
                    BusinessId = savedBusinessAm.Id
                };

                request.ApplicationModel = userAm;

                UserResponse response = target.Add(request);

                Assert.IsNotNull(response);
                Assert.AreEqual(ServiceResult.Success, response.ServiceResult);
                Assert.IsTrue(response.Message.Contains(" sucessfully saved."));

                request.AuthorizationContext.BusinessId = savedBusinessAm.Id;

                UserAm savedUser = target.GetAll(request).ApplicationModels.FirstOrDefault(x => x.Email == userAm.Email);

                LoadCustomersForBusiness(savedBusinessAm, savedUser);
            }
        }

        private static void LoadCustomersForBusiness(BusinessAm savedBusinessAm, UserAm savedUser)
        {
            for (int i = 0; i < 10; i++)
            {
                ICustomerService service = _iocContainer.Resolve<ICustomerService>();

                Assert.IsNotNull(service);

                CustomerResponse response = service.Add(new CustomerServiceRequest()
                {
                    ApplicationModel = new CustomerAm()
                    {
                        AddressLineOne = "UNIT 1",
                        AddressLineTwo = "OUT OF BOUNDS",
                        Street = "Von Backstrom Boulevard",
                        Suburb = "Silverlakes",
                        TownOrCity = "Pretoria",
                        PostalCode = "0081",
                        Email = $"customer{i}@yahoo.co.za",
                        CellphoneNumber = "0721248899",
                        Bank = "FNB",
                        AccountNumber = "6211134445267",
                        BranchCode = "206658",
                        Reference = "aasasdasd",
                        Name = $"Test Customer Name {savedUser.Name}{i}",
                        RepresentativeId = savedUser.Id,
                        BusinessId = savedBusinessAm.Id
                    }
                });

                Assert.IsNotNull(response);
                Assert.AreEqual(ServiceResult.Success, response.ServiceResult);
                Assert.IsNotNull(response.Message);
            }
        }
    }
}
