using System.Configuration;
using Architecture.Tests.Infrustructure.MongoDb;
using Architecture.Tests.Infrustructure.Reflection;
using Architecture.Tests.Security.Application.Services.ApplicationFunction;
using Architecture.Tests.Security.Application.Services.Role;
using Architecture.Tests.Security.Domain.ApplicationFunction;
using Architecture.Tests.Security.Domain.Role;
using Architecture.Tests.Security.Domain.User;
using Architecture.Tests.Security.Repository;
using Architecture.Tests.Security.Repository.Database;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Architecture.Tests.DependencyInjection.Installers
{
    public class SecurityInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //Factories
            InstallFactories(container);

            //Repositories
            InstallRepositories(container);

            this.InstallApplicationServices(container);
        }

        private void InstallApplicationServices(IWindsorContainer container)
        {
            container.Register(Component.For<IRoleService>().ImplementedBy<RoleService>().LifestyleTransient());
            container.Register(Component.For<IApplicationFunctionService>().ImplementedBy<ApplicationFunctionService>()
                .LifestyleTransient());
        }

        private static void InstallRepositories(IWindsorContainer container)
        {
            container.Register(Component.For<IDatabaseContext>().ImplementedBy<SecurityDatabaseContext>()
                .DependsOn(Dependency.OnValue<string>(ConfigurationManager.ConnectionStrings["KBITDB"].ConnectionString))
                .LifestyleTransient());

            container.Register(Component.For<IUserRepository>().LifestyleTransient().ImplementedBy<UserRepository>()
                .DependsOn(Dependency.OnComponent<IDatabaseContext, SecurityDatabaseContext>())
                .DependsOn(Dependency.OnComponent<IObjectActivator, ObjectCreator>()));

            container.Register(Component.For<IRoleRepository>().LifestyleTransient().ImplementedBy<RoleRepository>()
                .DependsOn(Dependency.OnComponent<IDatabaseContext, SecurityDatabaseContext>())
                .DependsOn(Dependency.OnComponent<IObjectActivator, ObjectCreator>()));

            container.Register(Component.For<IApplicationFunctionRepository>().ImplementedBy<ApplicationFunctionRepository>()
                .DependsOn(Dependency.OnComponent<IDatabaseContext, SecurityDatabaseContext>())
                .DependsOn(Dependency.OnComponent<IObjectActivator, ObjectCreator>()).DependsOn());
        }

        private static void InstallFactories(IWindsorContainer container)
        {

        }
    }
}