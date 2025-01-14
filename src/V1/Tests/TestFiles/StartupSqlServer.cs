using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Cache;
using ServiceBricks.Cache.SqlServer;

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
            services.AddServiceBricksComplete(Configuration);

            // Remove all background tasks/timers for unit testing
            var timer = services.Where(x => x.ImplementationType == typeof(CacheExpirationTimer)).FirstOrDefault();
            if (timer != null)
                services.Remove(timer);

            // Register TestManager
            services.AddScoped<ITestManager<CacheDataDto>, CacheDataTestManager>();
        }

        public virtual void Configure(IApplicationBuilder app)
        {
            base.CustomConfigure(app);
            app.StartServiceBricks();
        }
    }
}