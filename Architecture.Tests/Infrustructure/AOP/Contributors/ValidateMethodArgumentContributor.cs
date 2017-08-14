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
    public class ValidateMethodArgumentContributor : IContributeComponentModelConstruction
    {
        public void ProcessModel(IKernel kernel, ComponentModel model)
        {
            List<MethodInfo> functions = model.Implementation.GetMethods()
                .Where(m => AttributesUtil.GetAttribute<ValidateMethodArgumentsAttribute>(m) != null).ToList();


            if (functions.Any())
            {
                model.Interceptors.AddIfNotInCollection(InterceptorReference.ForType<ValidateMethodArgumentInterceptor>());
            }
        }
    }
}
