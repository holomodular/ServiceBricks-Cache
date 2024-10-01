using ServiceBricks;
using ServiceBricks.Cache.Postgres;

//using ServiceBricks.Logging.InMemory;
using WebApp.Extensions;

namespace WebApp
{
    public class StartupPostgres
    {
        public StartupPostgres(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public virtual IConfiguration Configuration { get; set; }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddServiceBricks(Configuration);
            //services.AddServiceBricksLoggingInMemory(Configuration);
            services.AddServiceBricksCachePostgres(Configuration);
            services.AddCustomWebsite(Configuration);
            services.AddServiceBricksComplete(Configuration);
        }

        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment webHostEnvironment)
        {
            app.StartServiceBricks();
            app.StartCustomWebsite(webHostEnvironment);
            var logger = app.ApplicationServices.GetRequiredService<ILogger<StartupPostgres>>();
            logger.LogInformation("Application Started");
        }
    }
}