using System.Linq;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.ModelBuilder;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Contributors
{
    public class MandatoryInjectionContributor : IContributeComponentModelConstruction
    {
        public void ProcessModel(IKernel kernel, ComponentModel model)
        {
            foreach (var property in model.Properties)
            {
                if (property.Property.GetCustomAttributes(inherit: true).Any(x => x is MandatoryInjectionAttribute))
                {
                    property.Dependency.IsOptional = false;
                }
            }
        }
    }
}
