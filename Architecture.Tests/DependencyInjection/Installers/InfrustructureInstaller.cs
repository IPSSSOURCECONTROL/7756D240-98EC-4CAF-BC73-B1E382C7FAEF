using Architecture.Tests.Infrustructure.AOP.Contributors;
using Architecture.Tests.Infrustructure.AOP.Interceptors;
using Architecture.Tests.Infrustructure.Logging;
using Architecture.Tests.Infrustructure.Reflection;
using Architecture.Tests.Infrustructure.Serialization;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Architecture.Tests.DependencyInjection.Installers
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
        }
    }
}