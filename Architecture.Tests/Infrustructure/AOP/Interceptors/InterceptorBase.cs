using Architecture.Tests.Infrustructure.AOP.Attributes;
using Architecture.Tests.Infrustructure.Logging;

namespace Architecture.Tests.Infrustructure.AOP.Interceptors
{
    public abstract class InterceptorBase
    {
        [MandatoryInjection]
        public ILoggingType Logger { get; set; }
    }
}