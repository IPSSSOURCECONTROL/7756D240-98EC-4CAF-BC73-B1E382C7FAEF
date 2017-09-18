using System;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;

namespace Kbit.ControlCentre.MvcDependency
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        private readonly IWindsorContainer container;

        public WindsorControllerFactory()
        {
            this.container = ContainerFactory.Current();
        }

        protected override IController GetControllerInstance(
            RequestContext requestContext,
            Type controllerType)
        {
            return (IController) this.container.Resolve(controllerType);
        }
    }
}