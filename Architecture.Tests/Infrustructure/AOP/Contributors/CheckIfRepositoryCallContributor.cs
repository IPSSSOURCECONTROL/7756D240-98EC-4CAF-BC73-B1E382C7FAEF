using System.Linq;
using Architecture.Tests.Infrustructure.AOP.Attributes;
using Architecture.Tests.Infrustructure.AOP.Interceptors;
using Castle.Core;
using Castle.Core.Internal;
using Castle.MicroKernel;
using Castle.MicroKernel.ModelBuilder;

namespace Architecture.Tests.Infrustructure.AOP.Contributors
{
    public class CheckIfRepositoryCallContributor : IContributeComponentModelConstruction
    {
        public void ProcessModel(IKernel kernel, ComponentModel model)
        {
            var traceableMethods = model.Implementation.GetMethods()
                .Where(m => AttributesUtil.GetAttribute<CheckIfRepositoryCallAttribute>(m) != null).ToList();


            if (traceableMethods.Any())
            {
                model.Interceptors.AddIfNotInCollection(InterceptorReference.ForType<CheckIfRepositoryCallInterceptor>());
            }
        }
    }
}