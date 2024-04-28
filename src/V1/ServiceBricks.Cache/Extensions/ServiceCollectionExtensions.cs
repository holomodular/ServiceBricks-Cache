using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceBricks.Cache
{
    /// <summary>
    /// IServiceCollection extensions for Cache.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceBricksCache(this IServiceCollection services, IConfiguration configuration)
        {
            // Add to module registry for automapper
            ModuleRegistry.Instance.RegisterItem(typeof(CacheModule), new CacheModule());

            // Background Tasks
            services.AddHostedService<CacheExpirationTimer>();
            services.AddScoped<CacheExpirationTask.Worker>();

            // API Controllers
            services.AddScoped<ICacheDataApiController, CacheDataApiController>();

            // Register Services
            services.AddScoped<ISingleServerProcessService, SingleServerProcessService>();

            services.AddScoped<ICacheDataApiClient, CacheDataApiClient>();

            return services;
        }

        public static IServiceCollection AddServiceBricksCacheClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICacheDataApiClient, CacheDataApiClient>();

            return services;
        }
    }
}