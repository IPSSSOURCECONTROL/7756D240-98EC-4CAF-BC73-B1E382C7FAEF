using System;

namespace Architecture.Tests.Infrustructure.AOP.Attributes
{
    /// <summary>
    /// Applies a transaction scope to methods decorated with this attribute.
    /// </summary>
    public class TransactionalAttribute : Attribute
    {
    }
}