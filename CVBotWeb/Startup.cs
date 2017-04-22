using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CVBotWeb.Startup))]
namespace CVBotWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
