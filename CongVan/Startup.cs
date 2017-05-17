using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CongVan.Startup))]
namespace CongVan
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
