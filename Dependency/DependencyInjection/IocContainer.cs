using System;
using Castle.Windsor;
using KhanyisaIntel.Kbit.Framework.DependencyInjection.Installers;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Contributors;
using KhanyisaIntel.Kbit.Framework.Infrustructure.DependencyInjection;

namespace KhanyisaIntel.Kbit.Framework.DependencyInjection
{
    public class IocContainer: IAutoResolver
    {
        private readonly WindsorContainer _windsorContainer;

        public IocContainer()
        {
            this._windsorContainer = new WindsorContainer();
            this.RegisterConstructionConstributors();
            this.InstallDependencies();
        }

        public IWindsorContainer WindsorContainer { get {return this._windsorContainer;} }

        private void RegisterConstructionConstributors()
        {
            this._windsorContainer.Kernel.ComponentModelBuilder.AddContributor(new KbitRequiredContributor());
            this._windsorContainer.Kernel.ComponentModelBuilder.AddContributor(new CheckIfRepositoryCallContributor());
            this._windsorContainer.Kernel.ComponentModelBuilder.AddContributor(new ValidateMethodArgumentContributor());
            this._windsorContainer.Kernel.ComponentModelBuilder.AddContributor(new ServiceRequestContributor());
            this._windsorContainer.Kernel.ComponentModelBuilder.AddContributor(new TransactionalContributor());
            this._windsorContainer.Kernel.ComponentModelBuilder.AddContributor(new MandatoryInjectionContributor());
            this._windsorContainer.Kernel.ComponentModelBuilder.AddContributor(new AuthorizeActionContributor());
        }

        private void InstallDependencies()
        {
            this._windsorContainer.Install(
                new InfrustructureInstaller(),
                new MongoDbMappingInstaller(),
                new SecurityInstaller(),
                new BusinessIntelligenceInstaller(),
                new WebApiControllersInstaller());
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

        public void Dispose()
        {
            this._windsorContainer?.Dispose();
        }
    }
}
