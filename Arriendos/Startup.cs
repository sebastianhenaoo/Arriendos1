using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Arriendos.Startup))]
namespace Arriendos
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
