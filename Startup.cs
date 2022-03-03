using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LaundryV3.Startup))]
namespace LaundryV3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
