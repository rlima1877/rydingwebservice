using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestingGitHub.Startup))]
namespace TestingGitHub
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
