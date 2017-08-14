using Castle.Core.Internal;
using Castle.DynamicProxy;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Exception;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Logging;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Reflection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Interceptors
{
    public class ServiceRequestInterceptor : IInterceptor
    {
        [MandatoryInjection]
        public ILoggingType Logger { get; set; }

        [MandatoryInjection]
        public IObjectActivator ObjectActivator { get; set; }
        public void Intercept(IInvocation invocation)
        {
            ServiceRequestMethodAttribute attribute =
                invocation.Method.GetAttribute<ServiceRequestMethodAttribute>();

            if (attribute == null)
            {
                invocation.Proceed();
                return;
            }

            try
            {
                if (invocation.Arguments[0] == null)
                {
                    invocation.ReturnValue = this.ObjectActivator.CreateInstanceOf(invocation.Method.ReturnType,
                        "Null request recieved.", ServiceResult.Error);
                    return;
                }

                invocation.Proceed();

            }
            catch (KbitRequiredFieldValidationException exception)
            {
                this.Logger.Log(MessageFormatter.FormatException(exception));

                ServiceResult serviceResult = ServiceResult.Error;
                ;
                invocation.ReturnValue = this.ObjectActivator.CreateInstanceOf(invocation.Method.ReturnType,
                    exception.Message, serviceResult);
            }
            catch (System.Exception exception)
            {
                string errorMessage = $"Exception occurred whilst executing action. :";

                this.Logger.Log(MessageFormatter.FormatException(exception));

                ServiceResult serviceResult = ServiceResult.Exception;

                invocation.ReturnValue = this.ObjectActivator.CreateInstanceOf(invocation.Method.ReturnType,
                    errorMessage, serviceResult);
            }

        }
    }
}