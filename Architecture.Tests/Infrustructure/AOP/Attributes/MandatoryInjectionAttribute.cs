using System;

namespace Architecture.Tests.Infrustructure.AOP.Attributes
{
    /// <summary>
    /// Provides A.O.P method interception to instruct the IOC container to do property injection 
    /// on a Interface type property decorated with this attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MandatoryInjectionAttribute : Attribute
    {
    }
}
