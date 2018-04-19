using Microsoft.Owin;
using Owin;
using Hangfire;
using System;

[assembly: OwinStartupAttribute(typeof(WeatherApp2.Startup))]

namespace WeatherApp2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            GlobalConfiguration.Configuration.UseSqlServerStorage("WeatherContext");
            app.UseHangfireDashboard();
            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}