using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Logging;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Interceptors
{
    public abstract class InterceptorBase
    {
        [MandatoryInjection]
        public ILoggingType Logger { get; set; }
    }
}