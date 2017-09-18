using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Kbit.ControlCentre.Startup))]
namespace Kbit.ControlCentre
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}
