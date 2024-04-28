using Microsoft.AspNetCore.Hosting;
using ServiceBricks;
using ServiceBricks.Cache;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using ServiceBricks.Cache.SqlServer;
using ServiceBricks.Cache.Sqlite;
using ServiceBricks.Cache.Postgres;

namespace ServiceBricks.Xunit
{
    public class StartupMigrations : ServiceBricks.Startup
    {
        public StartupMigrations(IConfiguration configuration) : base(configuration)
        {
        }

        public virtual void ConfigureDevelopmentServices(IServiceCollection services)
        {
            base.CustomConfigureServices(services);
            services.AddSingleton(Configuration);
            services.AddServiceBricks(Configuration);

            //**************************
            //UNCOMMENT THE ONE YOU NEED
            //**************************
            services.AddServiceBricksCachePostgres(Configuration);
            //services.AddServiceBricksCacheSqlServer(Configuration);
            //services.AddServiceBricksCacheSqlite(Configuration);

            // Remove all background tasks/timers for unit testing

            services.AddServiceBricksComplete();
        }

        public virtual void Configure(IApplicationBuilder app)
        {
            base.CustomConfigure(app);
            app.StartServiceBricks();

            //**************************
            //UNCOMMENT THE ONE YOU NEED
            //**************************
            app.StartServiceBricksCachePostgres();
            //app.StartServiceBricksCacheSqlServer();
            //app.StartServiceBricksCacheSqlite();
        }
    }
}