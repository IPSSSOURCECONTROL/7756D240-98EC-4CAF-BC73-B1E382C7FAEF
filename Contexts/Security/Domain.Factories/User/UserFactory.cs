﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;
using KhanyisaIntel.Kbit.Framework.Infrustructure.MongoDb;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Reflection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Validation;
using KhanyisaIntel.Kbit.Framework.Security.Application.Models;
using KhanyisaIntel.Kbit.Framework.Security.Domain.User.AccountStatusTypes;
using KhanyisaIntel.Kbit.Framework.Security.Domain.User.PasswordTypes;
using KhanyisaIntel.Kbit.Framework.Security.Repository.Interfaces;

namespace KhanyisaIntel.Kbit.Framework.Security.Domain.Factories.User
{
    public class UserFactory: IDomainFactory<Domain.User.User, UserAm>
    {
        [MandatoryInjection]
        public IDatabaseContext DatabaseContext { get; set; }

        [MandatoryInjection]
        public IObjectActivator ObjectActivator { get; set; }

        [ValidateMethodArguments]
        public Domain.User.User BuildDomainEntityType(UserAm applicationModel, bool isNew = true)
        {
            applicationModel.Validate();

            PasswordResetPolicy passwordResetPolicy =
                (PasswordResetPolicy)
                this.ObjectActivator.CreateInstanceOf<PasswordResetPolicy>(
                    applicationModel.PasswordResetPolicy.RemoveWhiteSpaces());
            Password password = new Password(applicationModel.Password, passwordResetPolicy);

            Role.Role role =
                this.DatabaseContext.Table<Role.Role>()
                    .FirstOrDefault(x => x.TypeName == applicationModel.RoleId.RemoveWhiteSpaces());

            Validator.CheckReferenceTypeForNull(role, 
                MessageFormatter.InvalidRole(applicationModel.RoleId),
                MethodBase.GetCurrentMethod(), this.GetType());

            AccountStatus accountStatus =
                (AccountStatus)
                this.ObjectActivator.CreateInstanceOf<AccountStatus>(applicationModel.AccountStatus.RemoveWhiteSpaces());

            Domain.User.User user = new Domain.User.User(applicationModel.Name,
                applicationModel.Code, applicationModel.Email, password,
                role, accountStatus);

            if (!isNew)
            {
                user.Id = applicationModel.Id;
            }

            return user;
        }

        [ValidateMethodArguments]
        public UserAm BuildApplicationModelType(Domain.User.User domainEntity)
        {
            UserAm userAm = new UserAm();
            userAm.Name = domainEntity.Name;
            userAm.Code = domainEntity.Code;
            userAm.Email = domainEntity.Email;
            userAm.Password = domainEntity.Password.Value;
            userAm.PasswordResetPolicy =
                domainEntity.Password.PasswordResetPolicy.GetType().Name.InsertSpaceAfterCapitalLetter();
            userAm.RoleId = domainEntity.Role.TypeName.InsertSpaceAfterCapitalLetter();
            userAm.LicenseSpecification = domainEntity.License.TypeName.InsertSpaceAfterCapitalLetter();
            userAm.Id = domainEntity.Id;

            return userAm;
        }

        public IEnumerable<UserAm> BuildApplicationModelTypes(IEnumerable<Domain.User.User> domainEntities)
        {
            List<UserAm> applicationModels = new List<UserAm>();

            foreach (Domain.User.User user in domainEntities)
            {
                applicationModels.Add(this.BuildApplicationModelType(user));
            }

            return applicationModels;
        }
    }
}