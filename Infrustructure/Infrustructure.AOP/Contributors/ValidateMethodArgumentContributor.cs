using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.Core;
using Castle.Core.Internal;
using Castle.MicroKernel;
using Castle.MicroKernel.ModelBuilder;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Interceptors;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Contributors
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
