﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Validation;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Application.Model
{
    public abstract class ApplicationModelBase
    {
        public ApplicationModelBase()
        {
        }

        public string Id { get; set; } = string.Empty;

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
                ValidationUtility.CheckField(asdasd,
                    MessageFormatter.IsARequiredField(property.Name),
                    MethodBase.GetCurrentMethod());
            }
        }
    }
}
