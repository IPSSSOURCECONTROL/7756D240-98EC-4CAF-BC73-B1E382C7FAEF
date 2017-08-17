using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using KhanyisaIntel.Kbit.Framework.Mvc.Controllers.Business;
using KhanyisaIntel.Kbit.Framework.Mvc.Controllers.Customer;

namespace KhanyisaIntel.Kbit.Framework.DependencyInjection.Installers
{
    public class WebApiControllersInstaller: IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<CustomerController>().LifestyleTransient());
            container.Register(Component.For<BusinessController>().LifestyleTransient());

            //container.Register(
            //    Classes.FromAssembly(container.Resolve<IStackInspector>()
            //    .GetAllStackAssemblies()
            //    .FirstOrDefault(x => x.FullName.Contains("KhanyisaIntel.Kbit.Framework.Mvc.Controllers")))
            //    .BasedOn(typeof(ApiController))
            //    .WithServiceBase().LifestyleTransient());
        }
    }
}
