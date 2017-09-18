using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Castle.MicroKernel;
using Castle.MicroKernel.Handlers;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Diagnostics;
using KhanyisaIntel.Kbit.Framework.DependencyInjection.Installers;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Contributors;
using KhanyisaIntel.Kbit.Framework.Infrustructure.DependencyInjection;

namespace KhanyisaIntel.Kbit.Framework.DependencyInjection
{
    public class IocContainer: IAutoResolver, IWindsorInstallerAvailable
    {
        private readonly WindsorContainer _windsorContainer;
        private static readonly object SyncObject = new object();

        public IocContainer()
        {
            this._windsorContainer = new WindsorContainer();
            this.RegisterConstructionConstributors();
            this.InstallDependencies();
        }

        public  IWindsorContainer Current()
        {
            return this._windsorContainer;
        }

        public IWindsorContainer WindsorContainer { get {return this._windsorContainer;} }

        public TDependency Resolve<TDependency>() where TDependency : class
        {
            try
            {
                return this._windsorContainer.Resolve<TDependency>();
            }
            catch (Exception eeException)
            {
                return default(TDependency);
            }

        }

        public void Dispose()
        {
            this._windsorContainer?.Dispose();
        }

        public void RegisterInstaller(IWindsorInstaller installer)
        {
            this._windsorContainer.Install(installer);
        }

        private void RegisterConstructionConstributors()
        {
            //OwnDatabaseContextContributor
            this._windsorContainer.Kernel.ComponentModelBuilder.AddContributor(new OwnDatabaseContextContributor());
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

            //CheckForPotentiallyMisconfiguredComponents(this._windsorContainer);
        }

        private static void CheckForPotentiallyMisconfiguredComponents(IWindsorContainer container)
        {
            var host = (IDiagnosticsHost)container.Kernel.GetSubSystem(SubSystemConstants.DiagnosticsKey);
            var diagnostics = host.GetDiagnostic<IPotentiallyMisconfiguredComponentsDiagnostic>();

            var handlers = diagnostics.Inspect();

            if (handlers.Any())
            {
                var message = new StringBuilder();
                var inspector = new DependencyInspector(message);

                foreach (IExposeDependencyInfo handler in handlers)
                {
                    handler.ObtainDependencyDetails(inspector);
                }

                throw new MisconfiguredComponentException(message.ToString());
            }
        }
    }

    [Serializable]
    public class MisconfiguredComponentException : Exception
    {
        public MisconfiguredComponentException()
        {
        }

        public MisconfiguredComponentException(string message) : base(message)
        {
        }

        public MisconfiguredComponentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MisconfiguredComponentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
