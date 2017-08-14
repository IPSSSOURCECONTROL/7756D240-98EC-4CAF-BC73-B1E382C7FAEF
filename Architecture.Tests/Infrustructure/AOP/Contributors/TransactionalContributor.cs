using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Architecture.Tests.Infrustructure.AOP.Attributes;
using Architecture.Tests.Infrustructure.AOP.Interceptors;
using Castle.Core;
using Castle.Core.Internal;
using Castle.MicroKernel;
using Castle.MicroKernel.ModelBuilder;

namespace Architecture.Tests.Infrustructure.AOP.Contributors
{
    public class TransactionalContributor : IContributeComponentModelConstruction
    {
        public void ProcessModel(IKernel kernel, ComponentModel model)
        {
            List<MethodInfo> traceableMethods = model.Implementation.GetMethods()
                .Where(m => AttributesUtil.GetAttribute<TransactionalAttribute>(m) != null).ToList();


            if (traceableMethods.Any())
            {
                model.Interceptors.AddIfNotInCollection(InterceptorReference.ForType<TransactionalInterceptor>());
            }
        }
    }
}
