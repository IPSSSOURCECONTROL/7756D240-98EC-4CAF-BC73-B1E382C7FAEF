using System.Reflection;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Kbit.ControlCentre.MvcDependency
{
    public class ControllerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes
                    .FromAssembly(Assembly.GetExecutingAssembly())
                    .BasedOn<IController>().LifestylePerWebRequest());
        }
    }
}