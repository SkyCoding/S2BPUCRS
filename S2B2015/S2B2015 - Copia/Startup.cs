using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(S2B2015.Startup))]
namespace S2B2015
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
