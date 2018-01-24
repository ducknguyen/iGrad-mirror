using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IGrad.Startup))]
namespace IGrad
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
