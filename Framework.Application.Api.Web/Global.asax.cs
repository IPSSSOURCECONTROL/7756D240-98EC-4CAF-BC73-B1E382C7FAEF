using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;
using KhanyisaIntel.Kbit.Framework.DependencyInjection;

namespace Framework.Application.Api.Web
{
    public class WebApiApplication : HttpApplication
    {
        private IWindsorContainer _container;

        protected void Application_Start()
        {
            Task task = new Task(() =>
            {
                IocContainer iocContainer = new IocContainer();
                this._container = iocContainer.WindsorContainer;
            });
            task.Start();

            task.Wait();

            GlobalConfiguration.Configuration.Services.Replace(
                typeof(IHttpControllerActivator),
                new WindsorCompositionRoot(this._container));

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


        }

        public override void Dispose()
        {
            this._container.Dispose();
            base.Dispose();
        }
    }
}
