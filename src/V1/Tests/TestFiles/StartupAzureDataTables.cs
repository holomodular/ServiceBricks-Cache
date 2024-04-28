using Microsoft.AspNetCore.Hosting;
using ServiceBricks;
using ServiceBricks.Cache;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using ServiceBricks.Cache.AzureDataTables;

namespace ServiceBricks.Xunit
{
    public class StartupAzureDataTables : ServiceBricks.Startup
    {
        public StartupAzureDataTables(IConfiguration configuration) : base(configuration)
        {
        }

        public virtual void ConfigureDevelopmentServices(IServiceCollection services)
        {
            base.CustomConfigureServices(services);
            services.AddSingleton(Configuration);
            services.AddServiceBricks(Configuration);
            services.AddServiceBricksCacheAzureDataTables(Configuration);

            // Remove all background tasks/timers for unit testing
            var logtimer = services.Where(x => x.ImplementationType == typeof(CacheExpirationTimer)).FirstOrDefault();
            if (logtimer != null)
                services.Remove(logtimer);

            // Register TestManager
            services.AddScoped<ITestManager<CacheDataDto>, CacheDataTestManager>();

            services.AddServiceBricksComplete();
        }

        public virtual void Configure(IApplicationBuilder app)
        {
            base.CustomConfigure(app);
            app.StartServiceBricks();
            app.StartServiceBricksCacheAzureDataTables();
        }
    }
}