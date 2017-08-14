using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Framework.Application.Api.Web.Startup))]

namespace Framework.Application.Api.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
