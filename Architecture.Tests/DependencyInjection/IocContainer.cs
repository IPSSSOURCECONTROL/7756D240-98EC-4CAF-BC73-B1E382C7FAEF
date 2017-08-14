using System;
using Architecture.Tests.DependencyInjection.Installers;
using Architecture.Tests.Infrustructure.AOP.Contributors;
using Architecture.Tests.Infrustructure.DependencyInjection;
using Castle.Windsor;

namespace Architecture.Tests.DependencyInjection
{
    public class IocContainer: IAutoResolver
    {
        private readonly WindsorContainer _windsorContainer;

        public IocContainer()
        {
            this._windsorContainer = new WindsorContainer();
            this.RegisterConstructionConstributors();
            this.ConfigureMappings();
            this.InstallDependencies();
        }

        private void RegisterConstructionConstributors()
        {
            this._windsorContainer.Kernel.ComponentModelBuilder.AddContributor(new KbitRequiredContributor());
            this._windsorContainer.Kernel.ComponentModelBuilder.AddContributor(new CheckIfRepositoryCallContributor());
            this._windsorContainer.Kernel.ComponentModelBuilder.AddContributor(new ValidateMethodArgumentContributor());
            this._windsorContainer.Kernel.ComponentModelBuilder.AddContributor(new ServiceRequestContributor());
            this._windsorContainer.Kernel.ComponentModelBuilder.AddContributor(new TransactionalContributor());
            this._windsorContainer.Kernel.ComponentModelBuilder.AddContributor(new MandatoryInjectionContributor());
        }

        private void ConfigureMappings()
        {
           // MappingConfigurator.ConfigureSystemModelMappings();
        }

        private void InstallDependencies()
        {
            this._windsorContainer.Install(
                new InfrustructureInstaller(),
                                new MongoDbMappingInstaller(),
                new SecurityInstaller(),
                new BusinessIntelligenceInstaller());
        }

        public TDependency Resolve<TDependency>() where TDependency : class
        {
            try
            {
                return this._windsorContainer.Resolve<TDependency>();
            }
            catch (Exception e)
            {
                return default(TDependency);
            }

        }
    }
}
