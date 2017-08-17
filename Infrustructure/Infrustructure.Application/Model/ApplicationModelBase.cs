using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Exception;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Validation;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Application.Model
{
    public abstract class ApplicationModelBase
    {
        public string Id { get; set; } = string.Empty;

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
                var asdasd = property.GetValue(this);
                Validator.CheckField(asdasd,
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
                        MessageFormatter.IsARequiredField(property.Name));
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
