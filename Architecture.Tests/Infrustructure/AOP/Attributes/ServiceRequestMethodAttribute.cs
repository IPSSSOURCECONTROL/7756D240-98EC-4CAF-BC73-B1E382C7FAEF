using System;
using Architecture.Tests.Infrustructure.Application;

namespace Architecture.Tests.Infrustructure.AOP.Attributes
{
    /// <summary>
    /// Provides exception handling and other services to <see cref="IApplicationService{TServiceRequest,TServiceResponse}"/><para/>
    /// type methods. All <see cref="IApplicationService{TServiceRequest,TServiceResponse}"/> methods must be decorated with <para/>
    /// this attribute.
    /// </summary>
    public class ServiceRequestMethodAttribute: Attribute
    {
    }
}
