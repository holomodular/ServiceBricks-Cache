using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Cache;
using ServiceBricks.Cache.Postgres;

namespace ServiceBricks.Xunit
{
    public class StartupPostgres : ServiceBricks.Startup
    {
        public StartupPostgres(IConfiguration configuration) : base(configuration)
        {
        }

        public virtual void ConfigureDevelopmentServices(IServiceCollection services)
        {
            base.CustomConfigureServices(services);
            services.AddSingleton(Configuration);
            services.AddServiceBricks(Configuration);
            services.AddServiceBricksCachePostgres(Configuration);

            // Remove all background tasks/timers for unit testing
            var timer = services.Where(x => x.ImplementationType == typeof(CacheExpirationTimer)).FirstOrDefault();
            if (timer != null)
                services.Remove(timer);

            // Register TestManager
            services.AddScoped<ITestManager<CacheDataDto>, CacheDataTestManagerPostgres>();

            services.AddServiceBricksComplete();
        }

        public virtual void Configure(IApplicationBuilder app)
        {
            base.CustomConfigure(app);
            app.StartServiceBricks();
            app.StartServiceBricksCachePostgres();
        }
    }
}