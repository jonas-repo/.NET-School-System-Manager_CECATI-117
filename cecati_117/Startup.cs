using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(cecati_117.Startup))]
namespace cecati_117
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
