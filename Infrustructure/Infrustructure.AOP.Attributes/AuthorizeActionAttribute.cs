using System;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes
{
    /// <summary>
    /// Checks if a user is authorized to perform a certain action. 
    /// This attribute will work on ApplicationService methods.
    /// </summary>
    public class AuthorizeActionAttribute: Attribute
    {
    }
}
