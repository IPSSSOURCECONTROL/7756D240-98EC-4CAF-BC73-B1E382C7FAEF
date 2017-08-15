using Castle.Core.Internal;
using Castle.DynamicProxy;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Interceptors
{
    public class CheckIfRepositoryCallInterceptor : InterceptorBase, IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            CheckIfRepositoryCallAttribute attribute =
                invocation.Method.GetAttribute<CheckIfRepositoryCallAttribute>();

            if (attribute == null)
            {
                invocation.Proceed();
                return;
            }

            //Type type = invocation.InvocationTarget.GetType();

            //if (type.GetInterfaces().Any(x => x == typeof(IBasicRepository<>)))
            //{

            //}

            //if (invocation.TargetType != typeof(DatabaseContextWrapper))
            //{
            //    throw new InvalidOperationException("Can only invoke ");
            //}
            //foreach (object invocationArgument in invocation.Arguments)
            //{
            //    Validator.CheckReferenceTypeForNull(invocationArgument, MessageFormatter.EntityCanNotBeNull(),
            //        invocation.Method);
            //}

            invocation.Proceed();
        }
    }
}