using System.Configuration;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Models;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Business;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services.Customer;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Business;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Factories.Business;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Database;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Workflows.ProductListing;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;
using KhanyisaIntel.Kbit.Framework.Infrustructure.MongoDb;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository;

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
            container.Register(Classes.FromAssembly(Assembly.Load("KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Factories"))
                .BasedOn(typeof(IDomainFactory<,>)).WithServiceAllInterfaces());
        }

        private void InstallApplicationServices(IWindsorContainer container)
        {
            container.Register(
                Classes.FromAssembly(Assembly.Load("KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Application.Services"))
                    .BasedOn(typeof(IApplicationService<,>))
                    .WithServiceAllInterfaces());
        }

        private void InstallRepositories(IWindsorContainer container)
        {
            container.Register(Component.For<IDatabaseContext>().ImplementedBy<BusinessIntelligenceDatabaseContext>()
                .DependsOn(Dependency.OnValue<string>(ConfigurationManager.ConnectionStrings["KBITDB"].ConnectionString))
                .LifestyleTransient());

            container.Register(
                Classes.FromAssembly(Assembly.Load("KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository"))
                    .BasedOn(typeof(IBasicRepository<>))
                    .WithServiceAllInterfaces());

            container.Register(
                Classes.FromAssembly(Assembly.Load("KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository"))
                    .BasedOn(typeof(IRepositoryWorkflowAvailable<,>))
                    .WithServiceAllInterfaces());
        }

        private void InstallWorkflows(IWindsorContainer container)
        {
            container.Register(Component.For<ProductListingWorkflow>()
                .ImplementedBy<ProductListingWorkflow>().LifestyleTransient());
        }
    }
}
