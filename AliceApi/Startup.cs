using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AliceApi.Startup))]
namespace AliceApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
