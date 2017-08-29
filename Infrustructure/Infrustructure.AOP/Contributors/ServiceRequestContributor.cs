using System.Linq;
using Castle.Core;
using Castle.Core.Internal;
using Castle.MicroKernel;
using Castle.MicroKernel.ModelBuilder;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Contributors
{
    public class ServiceRequestContributor: IContributeComponentModelConstruction
    {
        public void ProcessModel(IKernel kernel, ComponentModel model)
        {
            var traceableMethods = model.Implementation.GetMethods()
                .Where(m => AttributesUtil.GetAttribute<ServiceRequestMethodAttribute>(m) != null).ToList();


            if (traceableMethods.Any())
            {
                model.Interceptors.AddIfNotInCollection(InterceptorReference.ForType<ServiceRequestInterceptor>());
            }
        }
    }
}
