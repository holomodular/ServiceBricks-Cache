﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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