using System.Transactions;
using Architecture.Tests.Infrustructure.AOP.Attributes;
using Castle.Core.Internal;
using Castle.DynamicProxy;

namespace Architecture.Tests.Infrustructure.AOP.Interceptors
{
    /// <summary>
    /// Intercepts method calls decorated with the <see cref="TransactionalAttribute"/>.
    /// </summary>
    public class TransactionalInterceptor : InterceptorBase, IInterceptor
    {   
        public void Intercept(IInvocation invocation)
        {
            TransactionalAttribute attribute =
                invocation.Method.GetAttribute<TransactionalAttribute>();

            if (attribute == null)
            {
                invocation.Proceed();
                return;
            }

            TransactionOptions transactionOptions = new TransactionOptions();
            transactionOptions.IsolationLevel = IsolationLevel.ReadCommitted;

            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
            {
                invocation.Proceed();
                transactionScope.Complete();
            }
        }
    }
}
