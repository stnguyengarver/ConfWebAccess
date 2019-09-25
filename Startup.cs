using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ConfWebAccess.Startup))]
namespace ConfWebAccess
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
