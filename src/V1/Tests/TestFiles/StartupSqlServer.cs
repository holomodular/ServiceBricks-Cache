using Microsoft.AspNetCore.Hosting;
using ServiceBricks;
using ServiceBricks.Cache;
using ServiceBricks.Cache.SqlServer;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace ServiceBricks.Xunit
{
    public class StartupSqlServer : ServiceBricks.Startup
    {
        public StartupSqlServer(IConfiguration configuration) : base(configuration)
        {
        }

        public virtual void ConfigureDevelopmentServices(IServiceCollection services)
        {
            base.CustomConfigureServices(services);
            services.AddSingleton(Configuration);
            services.AddServiceBricks(Configuration);
            services.AddServiceBricksCacheSqlServer(Configuration);

            // Remove all background tasks/timers for unit testing
            var timer = services.Where(x => x.ImplementationType == typeof(CacheExpirationTimer)).FirstOrDefault();
            if (timer != null)
                services.Remove(timer);

            // Register TestManager
            services.AddScoped<ITestManager<CacheDataDto>, CacheDataTestManager>();

            services.AddServiceBricksComplete();
        }

        public virtual void Configure(IApplicationBuilder app)
        {
            base.CustomConfigure(app);
            app.StartServiceBricks();
            app.StartServiceBricksCacheSqlServer();
        }
    }
}