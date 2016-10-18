using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_3F.Startup))]
namespace _3F
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
