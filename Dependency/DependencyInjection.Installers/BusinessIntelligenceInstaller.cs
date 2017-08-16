using System.Configuration;
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
using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;
using KhanyisaIntel.Kbit.Framework.Infrustructure.MongoDb;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Reflection;

namespace KhanyisaIntel.Kbit.Framework.DependencyInjection.Installers
{
    public class BusinessIntelligenceInstaller: IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            this.InstallWorkflows(container);
            this.InstallRepositories(container);
            this.InstallApplicationServices(container);
            //container.Register(Component.For<CustomerAm>().LifestyleTransient());
            this.InstallFactories(container);
        }

        private void InstallFactories(IWindsorContainer container)
        {
            container.Register(Component.For<IDomainFactory<Business, BusinessAm>>()
                .ImplementedBy<BusinessFactory>().LifestyleTransient());
        }

        private void InstallApplicationServices(IWindsorContainer container)
        {
            container.Register(Component.For<IBusinessService>().ImplementedBy<BusinessService>().LifestyleTransient());
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

            container.Register(Component.For<IProductRepository>().LifestyleTransient().ImplementedBy<ProductRepository>()
                .DependsOn(Dependency.OnComponent<IDatabaseContext, BusinessIntelligenceDatabaseContext>())
                .DependsOn(Dependency.OnComponent<IObjectActivator, ObjectCreator>()));

            container.Register(Component.For<IBusinessRepository>().LifestyleTransient().ImplementedBy<BusinessRepository>()
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
