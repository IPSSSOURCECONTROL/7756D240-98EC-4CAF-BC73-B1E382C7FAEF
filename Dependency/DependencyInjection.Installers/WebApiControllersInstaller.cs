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
            
            ////List<string> controllerTypes = Directory.GetFiles(
            ////    ConfigurationManager.AppSettings["APP_BIN"],
            ////    "*.dll", SearchOption.AllDirectories).Distinct().Where(x=>x.Contains("Framework.Mvc.Controllers")).ToList();

            ////foreach (string controllerType in controllerTypes)
            ////{
            ////    Assembly assembly = Assembly.LoadFile(controllerType);

            ////    IEnumerable<Type> apiControllerTypes = assembly.GetTypes()
            ////        .Where(x => x != null && x.BaseType == typeof(ApiController));

            ////    foreach (Type apiControllerType in apiControllerTypes)
            ////    {
            ////        container.Register(Component.For(apiControllerType).LifestyleTransient());
            ////    }
            ////}


            //////foreach (Assembly assembly in controllerAssemblies)
            //////{
            //////    container.Register(Component.For().LifestyleTransient());
            //////}

            container.Register(Component.For<CustomerController>().LifestyleTransient());

            container.Register(Component.For<BusinessController>().LifestyleTransient());
        }
    }
}
