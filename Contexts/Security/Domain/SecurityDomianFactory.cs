using System;
using System.Reflection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Validation;
using KhanyisaIntel.Kbit.Framework.Security.Domain.User.PasswordTypes;

namespace KhanyisaIntel.Kbit.Framework.Security.Domain
{
    public class SecurityDomianFactory
    {
        /// <summary>
        /// Creates a valid <see cref="User.User"/> object given valid creation 
        /// parameters. 
        /// Validates all invariants.
        /// </summary>
        /// <param name="createUserParameters">All properties need to create a valid <see cref="User.User"/></param>
        /// <returns>A <see cref="User.User"/></returns>
        public static User.User CreateUser(CreateUserParameters createUserParameters)
        {
            Validator.CheckReferenceTypeForNull(createUserParameters, nameof(createUserParameters),
                MethodBase.GetCurrentMethod(), typeof(SecurityDomianFactory));

            Validator.IsNullEmptyOrWhitespace(createUserParameters.Name, nameof(createUserParameters.Name), 
                MethodBase.GetCurrentMethod(), typeof(SecurityDomianFactory));

            Validator.IsNullEmptyOrWhitespace(createUserParameters.Code, nameof(createUserParameters.Code),
                MethodBase.GetCurrentMethod(), typeof(SecurityDomianFactory));

            Validator.IsNullEmptyOrWhitespace(createUserParameters.Email, nameof(createUserParameters.Email),
                MethodBase.GetCurrentMethod(), typeof(SecurityDomianFactory));

            Validator.IsNullEmptyOrWhitespace(createUserParameters.Password, nameof(createUserParameters.Password),
                MethodBase.GetCurrentMethod(), typeof(SecurityDomianFactory));

            Validator.CheckReferenceTypeForNull(createUserParameters.PasswordResetPolicy, nameof(createUserParameters.PasswordResetPolicy),
                MethodBase.GetCurrentMethod(), typeof(SecurityDomianFactory));

            Validator.CheckReferenceTypeForNull(createUserParameters.Role, nameof(createUserParameters.Role),
                MethodBase.GetCurrentMethod(), typeof(SecurityDomianFactory));

            Validator.CheckReferenceTypeForNull(createUserParameters.AccountStatus, nameof(createUserParameters.AccountStatus),
                MethodBase.GetCurrentMethod(), typeof(SecurityDomianFactory));

            return new User.User( createUserParameters.Name, 
                createUserParameters.Code, createUserParameters.Email, 
                new Password(createUserParameters.Password, createUserParameters.PasswordResetPolicy), 
                createUserParameters.Role, createUserParameters.AccountStatus);
        }

        public static ApplicationFunction.ApplicationFunction CreateApplicationFunction(
            string name, string assemblyInformation)
        {
            Validator.IsNullEmptyOrWhitespace(name, nameof(name), MethodBase.GetCurrentMethod(),
                typeof(SecurityDomianFactory));

            ApplicationFunction.ApplicationFunction applicationFunction = 
                new ApplicationFunction.ApplicationFunction(name);


            applicationFunction.ScanAppDomain(assemblyInformation);

            return applicationFunction;
        }
    }
}
