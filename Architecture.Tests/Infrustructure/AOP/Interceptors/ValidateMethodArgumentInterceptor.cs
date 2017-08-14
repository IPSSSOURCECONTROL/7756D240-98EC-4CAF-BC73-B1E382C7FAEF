using Architecture.Tests.Infrustructure.AOP.Attributes;
using Architecture.Tests.Infrustructure.Utilities;
using Architecture.Tests.Infrustructure.Validation;
using Castle.Core.Internal;
using Castle.DynamicProxy;

namespace Architecture.Tests.Infrustructure.AOP.Interceptors
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
                Validator.CheckReferenceTypeForNull(invocationArgument, MessageFormatter.EntityCanNotBeNull(),
                    invocation.Method);
            }

            invocation.Proceed();
        }
    }
}
