using System.Configuration;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;
using KhanyisaIntel.Kbit.Framework.Infrustructure.MongoDb;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Stack;
using KhanyisaIntel.Kbit.Framework.Security.Repository.Database;

namespace KhanyisaIntel.Kbit.Framework.DependencyInjection.Installers
{
    public class SecurityInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            InstallRepositories(container);
            InstallFactories(container);
            this.InstallApplicationServices(container);
        }

        private void InstallApplicationServices(IWindsorContainer container)
        {
            container.Register(
                Classes.FromAssembly(container.Resolve<IStackInspector>()
                .GetAllStackAssemblies()
                .FirstOrDefault(x => x.ManifestModule.Name=="KhanyisaIntel.Kbit.Framework.Security.Application.Services.dll"))
                .BasedOn(typeof(IApplicationService<,>))
                .WithServiceAllInterfaces()
                .LifestyleTransient());
        }

        private static void InstallRepositories(IWindsorContainer container)
        {
            container.Register(Component.For<IDatabaseContext>().ImplementedBy<SecurityDatabaseContext>()
                .DependsOn(Dependency.OnValue<string>(ConfigurationManager.ConnectionStrings["KBITDB"].ConnectionString))
                .LifestyleTransient());

            container.Register(
                Classes.FromAssembly(container.Resolve<IStackInspector>()
                .GetAllStackAssemblies()
                .FirstOrDefault(x => x.FullName.Contains("KhanyisaIntel.Kbit.Framework.Security.Repository")))
                .BasedOn(typeof(IBasicRepository<>))
                .WithServiceAllInterfaces()
                .LifestyleTransient());

            container.Register(
                Classes.FromAssembly(container.Resolve<IStackInspector>()
                .GetAllStackAssemblies()
                .FirstOrDefault(x => x.FullName.Contains("KhanyisaIntel.Kbit.Framework.Security.Repository")))
                .BasedOn(typeof(IRepositoryWorkflowAvailable<,>))
                .WithServiceAllInterfaces()
                .LifestyleTransient());
        }

        private static void InstallFactories(IWindsorContainer container)
        {
            container.Register(
                Classes.FromAssembly(container.Resolve<IStackInspector>()
                .GetAllStackAssemblies()
                .FirstOrDefault(x => x.FullName.Contains("KhanyisaIntel.Kbit.Framework.Security.Domain.Factories")))
                .BasedOn(typeof(IDomainFactory<,>))
                .WithServiceAllInterfaces()
                .LifestyleTransient());
        }
    }
}