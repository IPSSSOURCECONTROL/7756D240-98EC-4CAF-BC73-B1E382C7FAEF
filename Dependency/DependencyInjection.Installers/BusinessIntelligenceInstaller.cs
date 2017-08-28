using System.Configuration;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Database;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Workflows.ProductListing;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Stack;

namespace KhanyisaIntel.Kbit.Framework.DependencyInjection.Installers
{
    public class BusinessIntelligenceInstaller: IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            this.InstallWorkflows(container);
            this.InstallRepositories(container);
            this.InstallApplicationServices(container);
            this.InstallFactories(container);
        }

        private void InstallFactories(IWindsorContainer container)
        {
            container.Register(
                Classes.FromAssembly(container.Resolve<IStackInspector>()
                .GetAllStackAssemblies()
                .FirstOrDefault(x=>x.FullName.Contains("KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Factories")))
                .BasedOn(typeof(IDomainFactory<,>))
                .WithServiceAllInterfaces()
                .LifestyleTransient());
        }

        private void InstallApplicationServices(IWindsorContainer container)
        {
            container.Register(
                Classes.FromAssembly(container.Resolve<IStackInspector>()
                .GetAllStackAssemblies()
                .FirstOrDefault(x => x.FullName.Contains("KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services")))
                .BasedOn(typeof(IApplicationService<,>))
                .WithServiceAllInterfaces()
                .LifestyleTransient());
        }

        private void InstallRepositories(IWindsorContainer container)
        {
            container.Register(Component.For<IDatabaseContext>().ImplementedBy<BusinessIntelligenceDatabaseContext>()
                .DependsOn(Dependency.OnValue<string>(ConfigurationManager.ConnectionStrings["KBITDB"].ConnectionString))
                .LifestyleTransient());

            container.Register(
                Classes.FromAssembly(container.Resolve<IStackInspector>()
                .GetAllStackAssemblies()
                .FirstOrDefault(x => x.FullName.Contains("KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository")))
                .BasedOn(typeof(IBasicRepository<>))
                .WithServiceAllInterfaces()
                .LifestyleTransient());

            container.Register(
                Classes.FromAssembly(container.Resolve<IStackInspector>()
                .GetAllStackAssemblies()
                .FirstOrDefault(x => x.FullName.Contains("KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository")))
                .BasedOn(typeof(IRepositoryWorkflowAvailable<,>))
                .WithServiceAllInterfaces()
                .LifestyleTransient());
        }

        private void InstallWorkflows(IWindsorContainer container)
        {
            container.Register(Component.For<ProductListingWorkflow>()
                .ImplementedBy<ProductListingWorkflow>().LifestyleTransient());
        }
    }
}
