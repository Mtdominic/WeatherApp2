using Microsoft.Owin;
using Owin;
using Hangfire;
using System;
using WeatherApp2.Controllers;

[assembly: OwinStartupAttribute(typeof(WeatherApp2.Startup))]

namespace WeatherApp2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            GlobalConfiguration.Configuration.UseSqlServerStorage("WeatherContext");
            WeatherApiController obj = new WeatherApiController();
            RecurringJob.AddOrUpdate(() => obj.GetAllCities(), "*/5 * * * *");
            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}