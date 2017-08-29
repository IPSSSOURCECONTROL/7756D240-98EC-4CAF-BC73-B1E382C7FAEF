using System;
using System.Collections.Generic;
using System.Linq;
using KhanyisaIntel.Kbit.Framework.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.DependencyInjection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Exception;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Workflow.Exceptions;
using KhanyisaIntel.Kbit.Framework.Security.Domain.Role;
using KhanyisaIntel.Kbit.Framework.Security.Repository.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Framework.Tests.Security.Repository.Tests
{
    [TestClass]
    public class RoleRepositoryTests
    {
        private readonly IAutoResolver _autoResolver;

        public RoleRepositoryTests()
        {
            _autoResolver = new IocContainer();
        }

        [TestMethod]
        public void TestCanResolveDependency()
        {
            IRoleRepository target = _autoResolver.Resolve<IRoleRepository>();

            Assert.IsNotNull(target);
            Assert.IsInstanceOfType(target, typeof(IRoleRepository));
        }

        #region Test Add
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestRoleRepositoryAdd_MustThrowArgumentNullExceptionGivenNullParameter()
        {
            IRoleRepository target = _autoResolver.Resolve<IRoleRepository>();
            target.Add(null);
        }

        [TestMethod]
        public void TestRoleRepositoryAdd_MustCreateANewRole()
        {
            Role expectedRole = new AdministratorRole("ExistingRoleRecord1");

            IRoleRepository target = _autoResolver.Resolve<IRoleRepository>();
            target.Add(expectedRole);
            
            Role actualRole = target.GetById(expectedRole.Id);

            Assert.IsNotNull(actualRole);
            Assert.AreEqual(expectedRole.Id, actualRole.Id);
            Assert.IsInstanceOfType(actualRole, typeof(AdministratorRole));
            target.Delete(expectedRole);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityAlreadyExistException))]
        public void TestRoleRepositoryAdd_MustNotCreateANewRoleDueToRoleRecordAlreadyExisting()
        {
            Role expectedRole = new AdministratorRole("ExistingRoleRecord1");

            IRoleRepository target = _autoResolver.Resolve<IRoleRepository>();
            target.Add(expectedRole);
            target.Add(expectedRole);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityAlreadyExistException))]
        public void TestRoleRepositoryAdd_MustNotCreateANewRoleDueToRoleNameAlreadyExisting()
        {
            Role expectedAdminRole = new AdministratorRole("ExistingRoleName");
            Role expectedNoRole = new NoRole("ExistingRoleName");

            IRoleRepository target = _autoResolver.Resolve<IRoleRepository>();
            target.Add(expectedAdminRole);
            target.Add(expectedNoRole);
        }
        #endregion

        #region Test Delete
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestRoleRepositoryDelete_MustThrowArgumentNullExceptionGivenNullParameter()
        {
            IRoleRepository target = _autoResolver.Resolve<IRoleRepository>();
            target.Delete(null);
        }

        [TestMethod]
        public void TestRoleRepositoryDelete_MustDeleteExistingRole()
        {
            Role expectedNoRole = new NoRole("RoleToDeleteSuccessfully");

            IRoleRepository target = _autoResolver.Resolve<IRoleRepository>();

            target.Add(expectedNoRole);
            target.Delete(expectedNoRole);

            Role actualRole = target.GetById(expectedNoRole.Id);

            Assert.IsNull(actualRole);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestRoleRepositoryDelete_MustNotDeleteProductDueToRoleNotExisting()
        {
            Role expectedNoRole = new NoRole("RoleThatDoesNotExist");

            IRoleRepository target = _autoResolver.Resolve<IRoleRepository>();
            
            target.Delete(expectedNoRole);
        }
        #endregion

        #region Test GetAll
        [TestMethod]
        public void TestRoleRepositoryGetAll_MustReturnAllExistingRoles()
        {
            Role role1 = new LimitedRole("AdminRole1");
            Role role2 = new ManagerRole("AdminRole2");
            
            IRoleRepository target = _autoResolver.Resolve<IRoleRepository>();
            target.Add(role1);
            target.Add(role2);

            IEnumerable<Role> actualRoleList = target.GetAll();

            Assert.IsNotNull(actualRoleList);
            Assert.AreNotEqual(0, actualRoleList.Count());
            Assert.AreEqual(actualRoleList.Select(x => x.Id).LastOrDefault(), role2.Id);

            target.Delete(role1);
            target.Delete(role2);
        }
        #endregion

        #region Test GetByID
        [TestMethod]
        public void TestRoleRepositoryGetByID_MustReturnAnExistingRole()
        {
            Role role1 = new LimitedRole("AdminRole1");

            IRoleRepository target = _autoResolver.Resolve<IRoleRepository>();
            target.Add(role1);

            Role actualRole = target.GetById(role1.Id);

            Assert.IsNotNull(actualRole);
            Assert.IsInstanceOfType(role1, typeof(LimitedRole));
            Assert.AreEqual(role1.Id, actualRole.Id);

            target.Delete(role1);
        }
        #endregion

        #region Test Update
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestRoleRepositoryUpdate_MustThrowArgumentNullExceptionGivenNullParameter()
        {
            IRoleRepository target = _autoResolver.Resolve<IRoleRepository>();
            target.Update(null);
        }

        [TestMethod]
        public void TestRoleRepositoryUpdate_MustUpdateAnExistingRole()
        {
            Role role1 = new LimitedRole("AdminRole1");

            IRoleRepository target = _autoResolver.Resolve<IRoleRepository>();
            target.Add(role1);

            role1 = new SupermanRole("AdminRole1");

            target.Update(role1);

            Role updatedRole = target.GetById(role1.Id);

            Assert.IsNotNull(updatedRole);
            Assert.IsInstanceOfType(role1, typeof(SupermanRole));

            target.Delete(role1);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityDoesNotExistException))]
        public void TestRoleRepositoryUpdate_MustThrowExceptionDueToRoleNotExisting()
        {
            Role expectedRole = new ManagerRole("AdminRole1");

            IRoleRepository target = _autoResolver.Resolve<IRoleRepository>();
            target.Update(expectedRole);
        }
        #endregion
    }
}
