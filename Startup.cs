using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyPSBlog.Startup))]
namespace MyPSBlog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
