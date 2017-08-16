using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AspNet.Identity.MongoDB;
using Castle.Windsor;
using Framework.Application.Api.Web.DependencyInstallers;
using Framework.Application.Api.Web.Models;
using KhanyisaIntel.Kbit.Framework.DependencyInjection;
namespace Framework.Application.Api.Web
{
    public class WebApiApplication : HttpApplication
    {
        private IWindsorContainer _container;

        protected void Application_Start()
        {
            IocContainer iocContainer = new IocContainer();
            iocContainer.RegisterInstaller(new AccountControllerInstaller());
            this._container = iocContainer.WindsorContainer;

            UserStore<ApplicationUser>.Initialize("Mongo");
            //RoleStore<ApplicationUser>.Initialize("Mongo");

            GlobalConfiguration.Configuration.Services.Replace(
                typeof(IHttpControllerActivator),
                new WindsorCompositionRoot(this._container));

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
