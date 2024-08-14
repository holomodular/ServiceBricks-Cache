using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceBricks.Cache
{
    /// <summary>
    /// Extensions for adding the ServiceBricks Cache module to the service collection.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the ServiceBricks Cache module to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksCache(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add the module to the ModuleRegistry
            ModuleRegistry.Instance.RegisterItem(typeof(CacheModule), new CacheModule());

            // AI: Add hosted services for the module
            services.AddHostedService<CacheExpirationTimer>();

            // AI: Add workers for tasks in the module
            services.AddScoped<CacheExpirationTask.Worker>();

            // AI: Configure all options for the module

            // AI: Add API Controllers for each DTO in the module
            services.AddScoped<ICacheDataApiController, CacheDataApiController>();

            // AI: Add any miscellaneous services for the module
            services.AddScoped<ISingleServerProcessService, SingleServerProcessService>();

            // AI: Register business rules for the module

            // AI: Register servicebus subscriptions for the module

            return services;
        }

        /// <summary>
        /// Add the ServiceBricks Cache client to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksCacheClient(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add clients for the module for each DTO
            services.AddScoped<ICacheDataApiClient, CacheDataApiClient>();

            return services;
        }
    }
}