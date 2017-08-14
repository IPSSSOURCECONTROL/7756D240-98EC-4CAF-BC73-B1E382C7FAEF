using System.Configuration;
using Architecture.Tests.BusinessIntelligence.Application.Models;
using Architecture.Tests.BusinessIntelligence.Application.Services.Customer;
using Architecture.Tests.BusinessIntelligence.Domain.Customer;
using Architecture.Tests.BusinessIntelligence.Repository;
using Architecture.Tests.BusinessIntelligence.Repository.Database;
using Architecture.Tests.BusinessIntelligence.Workflows.ProductListing;
using Architecture.Tests.Infrustructure.MongoDb;
using Architecture.Tests.Infrustructure.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Architecture.Tests.DependencyInjection.Installers
{
    public class BusinessIntelligenceInstaller: IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            this.InstallWorkflows(container);
            this.InstallRepositories(container);
            this.InstallApplicationServices(container);
            container.Register(Component.For<CustomerAm>().LifestyleTransient());
        }

        private void InstallApplicationServices(IWindsorContainer container)
        {
            container.Register(Component.For<ICustomerService>().ImplementedBy<CustomerService>().LifestyleTransient());
        }

        private void InstallRepositories(IWindsorContainer container)
        {
            container.Register(Component.For<IDatabaseContext>().ImplementedBy<BusinessIntelligenceDatabaseContext>()
                .DependsOn(Dependency.OnValue<string>(ConfigurationManager.ConnectionStrings["KBITDB"].ConnectionString))
                .LifestyleTransient());

            container.Register(Component.For<ICustomerRepository>().LifestyleTransient().ImplementedBy<CustomerRepository>()
                .DependsOn(Dependency.OnComponent<IDatabaseContext, BusinessIntelligenceDatabaseContext>())
                .DependsOn(Dependency.OnComponent<IObjectActivator, ObjectCreator>()));

            container.Register(Component.For<IProductListingRepository>().ImplementedBy<ProductListingRepository>()
                .DependsOn(Dependency.OnComponent<IDatabaseContext, BusinessIntelligenceDatabaseContext>())
                .DependsOn(Dependency.OnComponent<ProductListingWorkflow, ProductListingWorkflow>()));
        }

        private void InstallWorkflows(IWindsorContainer container)
        {
            container.Register(Component.For<ProductListingWorkflow>()
                .ImplementedBy<ProductListingWorkflow>()
                .DependsOn(Dependency.OnComponent<IObjectActivator, ObjectCreator>()));
        }
    }
}
