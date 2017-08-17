﻿using System;
using KhanyisaIntel.Kbit.Framework.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Exception;
using KhanyisaIntel.Kbit.Framework.Security.Application.Models;
using KhanyisaIntel.Kbit.Framework.Security.Domain.Factories.User;
using KhanyisaIntel.Kbit.Framework.Security.Domain.Role;
using KhanyisaIntel.Kbit.Framework.Security.Domain.User;
using KhanyisaIntel.Kbit.Framework.Security.Domain.User.AccountStatusTypes;
using KhanyisaIntel.Kbit.Framework.Security.Repository.Interfaces;
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
        [ExpectedException(typeof(ArgumentException))]
        public void TestBuildDomainEntityType_MustFailGivenInvalidRole()
        {
            IDomainFactory<User, UserAm> target = this._iocContainer.Resolve<IDomainFactory<User, UserAm>>();

            UserAm userAm = new UserAm
            {
                Name = "Steveland",
                Code = null,
                Email = "steveland@khanyisaintel.co.za",
                AccountStatus = "Active Account Status",
                LicenseSpecification = "Perpertual License Specification",
                Password = "P@ssW0rd1",
                PasswordResetPolicy = "Never Reset Password Reset Policy",
                RoleId = "NonExistantRole"
            };

            target.BuildDomainEntityType(userAm);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestBuildDomainEntityType_MustSuccessfullyCreateDomainEntity()
        {
            IDomainFactory<User, UserAm> target = this._iocContainer.Resolve<IDomainFactory<User, UserAm>>();

            IRoleRepository repository = this._iocContainer.Resolve<IRoleRepository>();
            repository.Add(new SupermanRole());

            UserAm userAm = new UserAm
            {
                Name = "Steveland",
                Code = null,
                Email = "steveland@khanyisaintel.co.za",
                AccountStatus = "Active Account Status",
                LicenseSpecification = "Perpertual License Specification",
                Password = "P@ssW0rd1",
                PasswordResetPolicy = "Never Reset Password Reset Policy",
                RoleId = "NonExistantRole"
            };

            User user = target.BuildDomainEntityType(userAm);

            Assert.IsNotNull(user);
            Assert.AreEqual(userAm.Name, user.Name);
            Assert.AreEqual(user.Code, user.Code);
            Assert.AreEqual(userAm.Email, user.Email);
            Assert.IsNotInstanceOfType(user.AccountStatus, typeof(ActiveAccountStatus));
        }
    }
}
