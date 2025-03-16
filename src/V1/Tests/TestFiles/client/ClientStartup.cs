﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Xunit;

namespace ServiceBricks.Cache.Client.Xunit
{
    public class ClientStartup : ServiceBricks.Startup
    {
        public ClientStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public virtual void ConfigureDevelopmentServices(IServiceCollection services)
        {
            base.CustomConfigureServices(services);
            services.AddSingleton(Configuration);
            services.AddServiceBricks(Configuration);
            services.AddServiceBricksCacheClient(Configuration);
            // Remove all background tasks/timers for unit testing

            // Register TestManagers
            services.AddScoped<ITestManager<CacheDataDto>, CacheDataTestManager>();

            services.AddServiceBricksComplete(Configuration);
        }

        public virtual void Configure(IApplicationBuilder app)
        {
            base.CustomConfigure(app);
            app.StartServiceBricks();
        }
    }
}