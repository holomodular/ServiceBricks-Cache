﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Cache.Postgres;
using ServiceBricks.Cache.Sqlite;
using ServiceBricks.Cache.SqlServer;

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

            services.AddServiceBricksCachePostgres(Configuration);
            services.AddServiceBricksCacheSqlServer(Configuration);
            services.AddServiceBricksCacheSqlite(Configuration);

            // Remove all background tasks/timers for unit testing

            services.AddServiceBricksComplete(Configuration);
        }

        public virtual void Configure(IApplicationBuilder app)
        {
            base.CustomConfigure(app);
            app.StartServiceBricks();
        }
    }
}