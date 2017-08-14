using System.Linq;
using Architecture.Tests.Infrustructure.AOP.Attributes;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.ModelBuilder;

namespace Architecture.Tests.Infrustructure.AOP.Contributors
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
