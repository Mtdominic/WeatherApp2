using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WeatherApp2.Startup))]
namespace WeatherApp2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
