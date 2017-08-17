using Castle.Core.Internal;
using Castle.DynamicProxy;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Validation;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Interceptors
{
    public class ValidateMethodArgumentInterceptor: InterceptorBase, IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            ValidateMethodArgumentsAttribute attribute =
                invocation.Method.GetAttribute<ValidateMethodArgumentsAttribute>();

            if (attribute == null)
            {
                invocation.Proceed();
                return;
            }

            if(invocation.Arguments == null)
                invocation.Proceed();

            foreach (object invocationArgument in invocation.Arguments)
            {
                if(invocationArgument != null && invocationArgument.GetType().IsValueType)
                    continue;
                Validator.CheckReferenceTypeForNull(invocationArgument, MessageFormatter.EntityCanNotBeNull(),
                    invocation.Method);
            }

            invocation.Proceed();
        }
    }
}
