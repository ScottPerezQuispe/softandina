using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sistareo.web.Startup))]
namespace Sistareo.web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
