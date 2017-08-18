using System.Linq;
using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AUT;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Encryption;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Logging;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Reflection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Serialization;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Stack;

namespace KhanyisaIntel.Kbit.Framework.DependencyInjection.Installers
{
    public class InfrustructureInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IStackInspector>()
                .ImplementedBy<StackInspector>());

            container.Register(Component.For<IObjectSerializer>()
                .ImplementedBy<ObjectSerializer>().LifestyleTransient());
            
            container.Register(Component.For<IObjectActivator>()
                .ImplementedBy<ObjectCreator>().LifestyleTransient());

            container.Register(Component.For<ILoggingType>()
                .ImplementedBy<NLogWrapper>());

            container.Register(Component.For<IAspNetCryptology>()
                .ImplementedBy<AspNetCryptology>());

            this.InstallAop(container);
        }

        private void InstallAop(IWindsorContainer container)
        {
            container.Register(
                Classes.FromAssembly(container.Resolve<IStackInspector>()
                .GetAllStackAssemblies()
                .FirstOrDefault(x => x.FullName.Contains("KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Interceptors")))
                .BasedOn(typeof(IInterceptor))
                .LifestyleTransient());

            //container.Register(Component.For<KbitRequiredInterceptor>().LifestyleTransient());
            //container.Register(Component.For<CheckIfRepositoryCallInterceptor>().LifestyleTransient());
            //container.Register(Component.For<ValidateMethodArgumentInterceptor>().LifestyleTransient());
            //container.Register(Component.For<TransactionalInterceptor>().LifestyleTransient());
            //container.Register(Component.For<ServiceRequestInterceptor>().LifestyleTransient());
            //container.Register(Component.For<AuthorizeActionInterceptor>().LifestyleTransient());
        }
    }
}