using System.Security.Principal;
using System.Web;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using KhanyisaIntel.Kbit.Framework.Mvc.Controllers.Business;
using KhanyisaIntel.Kbit.Framework.Mvc.Controllers.Customer;
using KhanyisaIntel.Kbit.Framework.Mvc.Controllers.User;

namespace KhanyisaIntel.Kbit.Framework.DependencyInjection.Installers
{
    public class WebApiControllersInstaller: IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<CustomerController>().LifestylePerWebRequest());
            container.Register(Component.For<BusinessController>().LifestylePerWebRequest());
            container.Register(Component.For<UserController>().LifestylePerWebRequest());
            container.Register(Component.For<IPrincipal>()
              .LifeStyle.PerWebRequest
              .UsingFactoryMethod(() => HttpContext.Current.User));
            //container.Register(
            //    Classes.FromAssembly(container.Resolve<IStackInspector>()
            //    .GetAllStackAssemblies()
            //    .FirstOrDefault(x => x.FullName.Contains("KhanyisaIntel.Kbit.Framework.Mvc.Controllers")))
            //    .BasedOn(typeof(ApiController))
            //    .WithServiceDefaultInterfaces().LifestyleTransient());
        }
    }
}
