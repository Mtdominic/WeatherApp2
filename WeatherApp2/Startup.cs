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
            //Each 15 "*/15 * * * *"
            RecurringJob.AddOrUpdate(() => obj.GetAllCities(), Cron.Hourly);
            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}