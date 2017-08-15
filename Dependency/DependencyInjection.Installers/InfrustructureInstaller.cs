using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Contributors;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Interceptors;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Logging;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Reflection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Serialization;

namespace KhanyisaIntel.Kbit.Framework.DependencyInjection.Installers
{
    public class InfrustructureInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IObjectSerializer>().ImplementedBy<ObjectSerializer>().LifestyleTransient());
            container.Register(Component.For<IObjectActivator>().ImplementedBy<ObjectCreator>());
            container.Register(Component.For<ILoggingType>().ImplementedBy<NLogWrapper>());

            this.InstallAop(container);
        }

        private void InstallAop(IWindsorContainer container)
        {
            container.Register(Component.For<KbitRequiredInterceptor>().LifestyleTransient());
            container.Register(Component.For<CheckIfRepositoryCallInterceptor>().LifestyleTransient());
            container.Register(Component.For<ValidateMethodArgumentInterceptor>().LifestyleTransient());
            container.Register(Component.For<TransactionalInterceptor>().LifestyleTransient());
            container.Register(Component.For<ServiceRequestInterceptor>().LifestyleTransient());
            container.Register(Component.For<AuthorizeActionInterceptor>().LifestyleTransient());
        }
    }
}