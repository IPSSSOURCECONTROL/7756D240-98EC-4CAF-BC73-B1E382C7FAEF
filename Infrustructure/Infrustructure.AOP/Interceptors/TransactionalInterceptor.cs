using System.Transactions;
using Castle.Core.Internal;
using Castle.DynamicProxy;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Interceptors
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
