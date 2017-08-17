using System;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes
{
    /// <summary>
    /// Validates the arguments of a method and throws 
    /// an <see cref="ArgumentException"/> if any null 
    /// argument is detected.
    /// </summary>
    public class ValidateMethodArgumentsAttribute: Attribute
    {

    }
}
