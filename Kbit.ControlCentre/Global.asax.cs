using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AspNet.Identity.MongoDB;
using Kbit.ControlCentre.Models;
using Kbit.ControlCentre.MvcDependency;
using KhanyisaIntel.Kbit.Framework.DependencyInjection;
using System.Web.Http;
using AutoMapper.Attributes;
using KhanyisaIntel.Kbit.Framework.Security.Repository.Interfaces;

namespace Kbit.ControlCentre
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AutoMapper.Mapper.Initialize(config => {
                typeof(MvcApplication).Assembly.MapTypes(config);      //or use typeof(Program).GetTypeInfo().Assembly if using .NET Standard
            });

            IocContainer iocContainer = new IocContainer();
            iocContainer.RegisterInstaller(new ControllerInstaller());

            IdentityExtensions.IdentityExtensions.UserRepository = iocContainer.Resolve<IUserRepository>();

            ContainerFactory.SetContainer(iocContainer.Current());

            if (ConfigurationManager.AppSettings["IS_DEV_ENV"] == "N")
            {
                UserStore<ApplicationUser>.Initialize("Mongo");
            }
            else
            {
                UserStore<ApplicationUser>.Initialize("Mongo");
            }
            ControllerBuilder.Current
                .SetControllerFactory(typeof(WindsorControllerFactory));
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
