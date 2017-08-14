using System.Linq;
using Castle.Core;
using Castle.Core.Internal;
using Castle.DynamicProxy;
using Castle.MicroKernel;
using Castle.MicroKernel.ModelBuilder;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Contributors
{
    public class KbitRequiredContributor : IContributeComponentModelConstruction
    {
        public void ProcessModel(IKernel kernel, ComponentModel model)
        {
            var traceableMethods = model.Implementation.GetProperties()
                .Where(m => AttributesUtil.GetAttribute<KbitRequiredAttribute>(m) != null).ToList();


            if (traceableMethods.Any())
            {
                model.Interceptors.AddIfNotInCollection(InterceptorReference.ForType<KbitRequiredInterceptor>());
            }
        }
    }

    public class KbitRequiredInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            KbitRequiredAttribute attribute =
                invocation.Method.GetAttribute<KbitRequiredAttribute>();

            if (attribute == null)
            {
                invocation.Proceed();
                return;
            }

            if (invocation.Arguments != null)
            {

            }
        }
    }
}
