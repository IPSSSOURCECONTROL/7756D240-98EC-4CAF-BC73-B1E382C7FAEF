using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Architecture.Tests.BusinessIntelligence.Domain.Factories.Customer;
using Architecture.Tests.Infrustructure.AOP.Attributes;
using Architecture.Tests.Infrustructure.AOP.Interceptors;
using Architecture.Tests.Infrustructure.Utilities;
using Architecture.Tests.Infrustructure.Validation;

namespace Architecture.Tests.Infrustructure.Application.Model
{
    public class ApplicationModelBase
    {
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Validates all properties of <see cref="ApplicationModelBase"/> subtypes 
        /// decorated with the <see cref="KbitRequiredAttribute"/>. Returns true 
        /// if the property is valid, false if not.
        /// </summary>
        public bool IsValid { get { return this.ValidateModel(); } }

        public string ValidationReason { get; set; } = string.Empty;

        /// <summary>
        /// Validates all properties of <see cref="ApplicationModelBase"/> subtypes 
        /// decorated with the <see cref="KbitRequiredAttribute"/>. Throws an exception
        /// if the property is invalid.
        /// </summary>
        public void Validate()
        {
            List<PropertyInfo> properties = this.GetType()
                .GetProperties()
                .Where(x => x.CustomAttributes.Any(c => c.AttributeType == typeof(KbitRequiredAttribute))).ToList();

            foreach (PropertyInfo property in properties)
            {
                Validator.CheckField(property.GetValue(this).ToString(),
                    MessageFormatter.IsARequiredField(property.Name),
                    MethodBase.GetCurrentMethod());
            }
        }

        private bool ValidateModel()
        {
            try
            {
                List<PropertyInfo> properties = this.GetType()
                    .GetProperties()
                    .Where(x => x.CustomAttributes.Any(c => c.AttributeType == typeof(KbitRequiredAttribute))).ToList();

                foreach (PropertyInfo property in properties)
                {
                    Validator.CheckField(property.GetValue(this).ToString(),
                        MessageFormatter.IsARequiredField(property.Name),
                        MethodBase.GetCurrentMethod(),
                        typeof(CustomerFactory));
                }
            }
            catch (KbitRequiredFieldValidationException exception)
            {
                this.ValidationReason = exception.Message;
                return false;
            }

            return true;
        }
    }
}
