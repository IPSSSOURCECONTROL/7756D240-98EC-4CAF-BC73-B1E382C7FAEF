using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Framework.Application.Api.Web.Controllers;

namespace Framework.Application.Api.Web.DependencyInstallers
{
    public class AccountControllerInstaller: IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<AccountController>().LifestyleTransient());
        }
    }
}